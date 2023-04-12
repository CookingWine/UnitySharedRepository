using SimpleJSON;
using System;
using System.Collections;
using System.Text;
using UnityEngine;
using UnityEngine.Networking;

public partial class MusicLogin :MonoBehaviour
{
    public string Cod;
    private void Awake( )
    {
        InitBindComponent( gameObject );
    }

    private void Start( )
    {
        InitInfo( );
        InitButtonFuncion( );
    }
    private void SuccessCompelet( DownloadHandler data )
    {
        FileExtend.CreationFileWirteData( @"D:\Test\test1.txt" , data.text );
    }

    private void FailedCompelet( string data )
    {

    }
    private void Update( )
    {
        if( Input.GetKeyUp( KeyCode.Space ) )
        {
            Debug.Log( "登录" );
            StartCoroutine( DDDD( ) );
        }
    }


    /// <summary>
    /// 初始化按钮事件
    /// </summary>
    private void InitButtonFuncion( )
    {
        m_Btn_SwitchoverLogin.onClick.AddListener( ( ) =>
        {
            m_Trans_AuthCodeLogin.SetActive( !m_Trans_AuthCodeLogin.gameObject.activeInHierarchy );
            m_Trans_QRCodeLogin.SetActive( !m_Trans_QRCodeLogin.gameObject.activeInHierarchy );
            if( m_Trans_QRCodeLogin.gameObject.activeInHierarchy )
            {
                Debug.Log( "开始绘制二维码界面" );
                StartCoroutine( CenKey( ) );
            }
        } );

        m_Btn_Login.onClick.AddListener( ( ) =>
        {
            if( VerificationAuthCode( m_TInput_AuthCode.text ) )
            {
                CloudMain.Instance.LoadAsset.LoadPrefabAsset( "CloundMusicInterface" , CloudMain.Instance.MainCanvas );
                UnityEngine.Object.Destroy( gameObject );
            }
            else
            {
                Debug.LogError( "不正确" );
            }
        } );

        m_Btn_SpeedCode.onClick.AddListener( ( ) =>
        {
            //if( !m_TInput_PhoneNumber.text.IsValidMobilePhoneNumber( ) )
            //{
            //    Debug.Log( "手机号不正确" );
            //    return;
            //}
            //if( m_TInput_PhoneNumber.text.IsNullOrEmpty( ) )
            //{
            //    return;
            //}
            SpeedCode( );
        } );
    }

    private void InitInfo( )
    {
        m_TTxt_LoginBtnTxt.text = "开始emo";
    }
    /// <summary>
    /// 验证，验证码是否正确
    /// </summary>
    /// <param name="code"></param>
    /// <returns></returns>
    private bool VerificationAuthCode( string code )
    {
        //StartCoroutine( PhoneCode( code ) );
        //if( !state )
        //{
        //    return false;
        //}
        return true;
    }

    /// <summary>
    /// 发送验证码
    /// </summary>
    private void SpeedCode( )
    {
        Debug.Log( "发送验证码" );
        StartCoroutine( SendPhoneCode( ) );


    }

    /// <summary>
    /// 生成二维码
    /// </summary>
    /// <returns></returns>
    private IEnumerator CenKey( )
    {
        string url = GetURL( ) + "/login/qr/key";
        using UnityWebRequest request = new UnityWebRequest( url );
        request.timeout = 5;
        DownloadHandlerBuffer data = new DownloadHandlerBuffer( );
        request.downloadHandler = data;
        yield return request.SendWebRequest( );
        if( request.result == UnityWebRequest.Result.Success )
        {
            string str = request.downloadHandler.text;
            Debug.Log( "生成key完成->" + str );
            JSONNode json = JSON.Parse( str );
            string da = json["data"]["unikey"];
            Debug.Log( $"key 值为->{da}" );
            StartCoroutine( SenInfo( da ) );
        }
        else
        {
            Debug.LogError( request.error );
        }
    }
    private string m_key;
    private IEnumerator SenInfo( string key )
    {
        m_key = key;
        string url = GetURL( ) + "/login/qr/create?key=" + key + "&qrimg=true&timestamp=" + TimeUtility.GetTimeStamp( );
        Debug.Log( $"完整的url--》{url}" );
        using UnityWebRequest request = new UnityWebRequest( url );
        request.timeout = 5;
        DownloadHandlerBuffer data = new DownloadHandlerBuffer( );
        request.downloadHandler = data;
        yield return request.SendWebRequest( );
        if( request.result == UnityWebRequest.Result.Success )
        {
            string str = Encoding.UTF8.GetString( request.downloadHandler.data );
            Debug.LogWarning( str );
            JSONNode json = JSON.Parse( str );
            string jsondata = json["data"]["qrimg"];
            string da = jsondata.Replace( "data:image/png;base64," , "" );
            byte[] texture = Convert.FromBase64String( da );
            Texture2D texture2D = new Texture2D( 512 , 512 );
            texture2D.LoadImage( texture );
            m_RImg_QRCode.texture = texture2D;
        }
        else
        {
            Debug.LogError( request.error );
        }
    }

    private IEnumerator DDDD( )
    {
        string url = GetURL( ) + "/login/qr/check?key=" + m_key;
        using UnityWebRequest request = new UnityWebRequest( url );
        request.timeout = 5;
        DownloadHandlerBuffer data = new DownloadHandlerBuffer( );
        request.downloadHandler = data;
        yield return request.SendWebRequest( );
        if( request.result == UnityWebRequest.Result.Success )
        {
            //包含cookie不包含用户信息
            Debug.Log( "二维码请求成功->" + request.downloadHandler.text );
            StartCoroutine( GetState( ) );
        }
        else
        {
            Debug.LogError( request.error );
        }
    }

    private IEnumerator SendPhoneCode( )
    {
        string url = "http://music.163.com/api/captcha/sent?phone=15139659175";

        using UnityWebRequest request = new UnityWebRequest( url );
        request.timeout = 5;
        DownloadHandlerBuffer data = new DownloadHandlerBuffer( );
        request.downloadHandler = data;
        yield return request.SendWebRequest( );
        if( request.result == UnityWebRequest.Result.Success )
        {
            Debug.Log( $"请求成功->{request.downloadHandler.text}" );
        }
        else
        {
            Debug.LogError( "请求失败" );
        }
    }

    private IEnumerator PhoneCode( string code )
    {
        string url = "http://43.138.56.175:3000/captcha/verify?phone=15139659175&captcha=" + code;

        using UnityWebRequest request = new UnityWebRequest( url );
        request.timeout = 5;
        DownloadHandlerBuffer data = new DownloadHandlerBuffer( );
        request.downloadHandler = data;
        yield return request.SendWebRequest( );
        if( request.result == UnityWebRequest.Result.Success )
        {
            Debug.Log( $"请求成功->{request.downloadHandler.text}" );
            //StartCoroutine( GetState( ) );
        }
        else
        {
            Debug.LogError( "请求失败" );
        }
    }

    public string GetURL( )
    {
        return "http://43.138.56.175:3000";
    }
    private IEnumerator GetState( )
    {
        //登录后获取用户信息
        string url = "http://43.138.56.175:3000/user/account";
        using UnityWebRequest request = new UnityWebRequest( url );
        request.timeout = 5;
        DownloadHandlerBuffer data = new DownloadHandlerBuffer( );
        request.downloadHandler = data;
        yield return request.SendWebRequest( );
        if( request.result == UnityWebRequest.Result.Success )
        {
            Debug.Log( request.downloadHandler.text );
        }
        else
        {
            Debug.LogError( request.error );
        }
    }

    private IEnumerator QRCode( )
    {
        string url = "http://43.138.56.175:3000/captcha/sent?phone=15139659175";

        using UnityWebRequest request = new UnityWebRequest( url );
        request.timeout = 5;
        DownloadHandlerBuffer data = new DownloadHandlerBuffer( );
        request.downloadHandler = data;
        yield return request.SendWebRequest( );
        if( request.result == UnityWebRequest.Result.Success )
        {
            Debug.Log( $"数据为->{request.downloadHandler.text}" );
        }
        else
        {
            Debug.LogError( "请求失败" );
        }
    }
}

/// <summary>
/// 为真时获取10位时间戳,为假时获取13位时间戳
/// </summary>
public class TimeUtility
{
    public static long GetTimeStamp( bool bflag = false )
    {
        TimeSpan ts = DateTime.UtcNow - new DateTime( 1970 , 1 , 1 , 0 , 0 , 0 , 0 );
        long ret;
        if( bflag )
            ret = Convert.ToInt64( ts.TotalSeconds );
        else
            ret = Convert.ToInt64( ts.TotalMilliseconds );
        return ret;
    }

    public static string GetTimeFormat( int time )
    {
        int minute = (int)( time / 60 );
        int seconds = time % 60;
        return string.Format( "{0:D2}:{1:D2}" , minute , seconds );
    }

}