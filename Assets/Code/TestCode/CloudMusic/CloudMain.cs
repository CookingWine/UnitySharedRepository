using UnityEngine;

public class CloudMain :MonoBehaviour
{
    [SerializeField]
    [Header( "是否在编辑器下启动bundle加载模式" )]
    private bool m_EnableEidtorBundleModel;

    [SerializeField]
    [Header( "当前Game使用的帧率" )]
    [Range(0,240)]
    private int m_GameFrameRate = 60;

    /// <summary>
    /// 数据
    /// </summary>
    private const string UserConfigJsonData = @"C:\Users\Admin\.CloudMusic\UserData\user.json";

    /// <summary>
    /// 是否可以自动登录
    /// </summary>
    private bool m_IsAutonLogin;

    public Transform MainCanvas;

    public GameLoadAsset LoadAsset { get; private set; }
    private void Awake( )
    {
        LoadAsset = new GameLoadAsset( );
        Application.targetFrameRate = m_GameFrameRate;
        LoadAsset.EditorEnableBunle( m_EnableEidtorBundleModel );
        if( UserConfigJsonData.FileExists( out string data ) )
        {
            if( !data.IsNullOrEmpty( ) )
            {
                Debug.Log( "判断是否符合自动登录" );
                m_IsAutonLogin = true;

            }
            else
            {
                m_IsAutonLogin = false;
            }
        }
        else
        {
            FileExtend.CreationFile( UserConfigJsonData );
            m_IsAutonLogin = false;
        }
    }
    private void Start( )
    {
        if( !m_IsAutonLogin )
        {
            Debug.Log( "等待加载登录界面" );
            LoadAsset.LoadPrefabAsset( "MusicLogin" , MainCanvas );
        }
    }
}
