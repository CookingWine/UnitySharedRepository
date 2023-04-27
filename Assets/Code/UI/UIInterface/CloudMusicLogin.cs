using SimpleJSON;
using System;
using System.Text;
using UnityEngine;
using UnityEngine.Networking;
using CloudMusic.API;
//自动生成于:2023/4/13 10:30:18
public partial class CloudMusicLogin :MonoBehaviour
{
    private readonly string m_DefalutPath = "C:\\Users\\Admin\\.UserLonginData.json";
    private static CloudMusicLogin m_Instance;
    public static CloudMusicLogin Instance
    {
        get
        {
            if( m_Instance == null )
            {
                Debug.LogError( "登录界面已经被卸载，或者还没有创建" );
                return null;
            }
            return m_Instance;
        }
    }
    private string m_QRCodeKey = string.Empty;
    public string UserLoginData { get; private set; }
    private void Awake( )
    {
        m_Instance = this;
        InitBindComponent( gameObject );
    }

    private void Start( )
    {
        if( FileExtend.FileExists( m_DefalutPath , out string data ) )
        {
            UserLoginData = data;
            Invoke( nameof( AutoLogin ) , 2.0f );
        }
        else
        {
            string url = HttpRequest.Instance.RequestUrl + "/" + CloudMusicAPI.CloudMusicWebRequest.LoginQRkeyRequest( );
            HttpRequest.Instance.CreateCloudRequet( url , 5 , GenKeySuccessCompenlet ,
                ( message ) =>
                {
                    Debug.LogError( message );
                } );
        }
    }

    private void GenKeySuccessCompenlet( DownloadHandler data )
    {
        JSONNode json = JSON.Parse( data.text );
        string key = json["data"]["unikey"];
        m_QRCodeKey = key;
        string url = HttpRequest.Instance.RequestUrl + "/" + CloudMusicAPI.CloudMusicWebRequest.LoginQrCreateRequest( key );
        HttpRequest.Instance.CreateCloudRequet( url , 5 , GenQRCodeSuccess , ( message ) =>
        {
            Debug.LogError( message );
        } );
    }
    private void GenQRCodeSuccess( DownloadHandler data )
    {
        string str = Encoding.UTF8.GetString( data.data );
        JSONNode json = JSON.Parse( str );
        string jsondata = json["data"]["qrimg"];
        string da = jsondata.Replace( "data:image/png;base64," , "" );
        byte[] texture = Convert.FromBase64String( da );
        Texture2D texture2D = new Texture2D( 512 , 512 );
        texture2D.LoadImage( texture );
        m_RImg_QRCode.texture = texture2D;
        InvokeRepeating( nameof( CheckQRCode ) , 5.0f , 5.0f );
    }

    private void CheckQRCode( )
    {
        string url = HttpRequest.Instance.RequestUrl + "/" + CloudMusicAPI.CloudMusicWebRequest.LoginQrCheckRequest( m_QRCodeKey );
        HttpRequest.Instance.CreateCloudRequet( url , 3 , ( data ) =>
        {
            JSONNode json = JSON.Parse( data.text );
            int code = json["code"];
            string message = json["message"];
            switch( code )
            {
                case 800:
                    Debug.LogError( message );
                    CancelInvoke( );
                    break;
                case 801:
                    Debug.LogWarning( message );
                    break;
                case 802:
                    Debug.LogWarning( message );
                    break;
                case 803:
                    string cookies = json["cookie"];
                    CloudMusicAPI.Cookie = cookies;
                    string requet = HttpRequest.Instance.RequestUrl + "/" + CloudMusicAPI.CloudMusicWebRequest.LoginStatus( );
                    HttpRequest.Instance.CreateCloudRequet( requet , 5 , ( data ) =>
                    {
                        UserLoginData = data.text;
                        FileExtend.CreationFileWirteData( m_DefalutPath , UserLoginData );
                        CloudMain.Instance.LoadAsset.LoadPrefabAsset( ExcelConfigData.CloundMusicInterface , CloudMain.Instance.MainCanvas );
                    } ,
                    ( message ) =>
                    {

                    } );
                    CancelInvoke( );
                    break;
            }
        } ,
           ( message ) =>
           {
               Debug.LogError( message );
           } );
    }

    private void Update( )
    {
        if( Input.GetKeyDown( KeyCode.Space ) )
        {
            Debug.Log( "退出登录" );
            string url = HttpRequest.Instance.RequestUrl + "/" + CloudMusicAPI.CloudMusicWebRequest.LogOut( );

            HttpRequest.Instance.CreateCloudRequet( url , 5 , ( data ) =>
            {
                Debug.Log( $"退出登录后的结果->{data.text}" );
            } , ( message ) =>
            {
                Debug.LogError( "退出失败" + message );
            } );
        }
    }

    private void AutoLogin( )
    {
        CloudMain.Instance.LoadAsset.LoadPrefabAsset( ExcelConfigData.CloundMusicInterface , CloudMain.Instance.MainCanvas );
    }
    public void Unload( )
    {
        m_Instance = null;
        UserLoginData = string.Empty;
        m_QRCodeKey = string.Empty;
        GameObject.Destroy( transform.gameObject );
    }
}
