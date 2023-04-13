using UnityEngine;

public class CloudMain :MonoBehaviour
{
    public static CloudMain Instance { get; private set; }

    [SerializeField]
    [Header( "是否在编辑器下启动bundle加载模式" )]
    private bool m_EnableEidtorBundleModel;

    [SerializeField]
    [Header( "当前Game使用的帧率" )]
    [Range(0,240)]
    private int m_GameFrameRate = 60;

    /// <summary>
    /// 是否可以自动登录
    /// </summary>
    private bool m_IsAutonLogin;

    public Transform MainCanvas;

    public GameLoadAsset LoadAsset { get; private set; }
    private void Awake( )
    {
        Instance = this;
        LoadAsset = new GameLoadAsset( );
        Application.targetFrameRate = m_GameFrameRate;
        LoadAsset.EditorEnableBunle( m_EnableEidtorBundleModel );
    }
    private void Start( )
    {
        if( !m_IsAutonLogin )
        {
            LoadAsset.LoadPrefabAsset( ExcelConfigData.CloudMusicLogin, MainCanvas );
        }
    }
}
