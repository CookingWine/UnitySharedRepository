using UnityEngine;

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
            if( m_TInput_PhoneNumber.text.IsNullOrEmpty( ) )
            {
                Debug.Log( "手机号为空" );
            }
            if( m_TInput_AuthCode.text.IsNullOrEmpty( ) )
            {
                return;
            }
            Debug.Log( "内容为" + m_TInput_AuthCode.text );
            if( !VerificationAuthCode( m_TInput_AuthCode.text ) )
            {
                return;
            }
            Debug.Log( "初始化个人信息,构建主界面" );
            CloudMain.Instance.LoadAsset.LoadPrefabAsset( "MainInterface" , CloudMain.Instance.MainCanvas );
            Object.Destroy( gameObject );
        } );

        m_Btn_SpeedCode.onClick.AddListener( ( ) =>
        {
            if( m_TInput_PhoneNumber.text.IsNullOrEmpty( ) )
            {
                return;
            }
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
        return !code.IsNullOrEmpty( );
    }

    /// <summary>
    /// 发送验证码
    /// </summary>
    private void SpeedCode( )
    {
        Debug.Log( "发送验证码" );
    }
}
