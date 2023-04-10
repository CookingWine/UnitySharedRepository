using System.Collections;
using UnityEngine;
using UnityEngine.Networking;

public partial class MusicLogin :MonoBehaviour
{
    private void Awake( )
    {
        InitBindComponent( gameObject );
    }

    private void Start( )
    {
        InitInfo( );

        InitButtonFuncion( );
    }

    /// <summary>
    /// 绘制二维码
    /// </summary>
    /// <param name="data"></param>
    private void DrawQRcode( string data )
    {
        m_Img_QRCode.sprite = null;
        Debug.Log( data );
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
                DrawQRcode( null );
            }
        } );

        m_Btn_Login.onClick.AddListener( ( ) =>
        {
            if( VerificationAuthCode( m_TInput_AuthCode.text ) )
            {
                CloudMain.Instance.LoadAsset.LoadPrefabAsset( "CloundMusicInterface" , CloudMain.Instance.MainCanvas );
                Object.Destroy( gameObject );
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

    private IEnumerator SendPhoneCode( )
    {
        string url = "http://cloud-music.pl-fe.cn/comment/sent?phone=15139659175";

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
        string url = "http://music.163.com/captcha/verify?phone=15139659175&captcha=" + code;

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
}
