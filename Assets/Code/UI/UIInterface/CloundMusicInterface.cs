using UnityEngine;

//自动生成于:2023/4/7 14:55:02
public partial class CloundMusicInterface :MonoBehaviour
{
    private static CloundMusicInterface m_Instance;
    public static CloundMusicInterface Instance
    {
        get
        {
            if( m_Instance == null )
            {
                Debug.LogError( "The main screen is not loaded yet" );
            }
            return m_Instance;
        }
    }

    #region string
    /// <summary>
    /// 顶部UI的名字
    /// </summary>
    private readonly string CloundMusicTop = "CloundMusicTop";

    /// <summary>
    /// 底部UI
    /// </summary>
    private readonly string CloundMusicDown = "CloundMusicDown";

    /// <summary>
    /// 搜索界面
    /// </summary>
    private readonly string SerachSongsInfo = "SerachSongsInfo";

    private readonly string LyricsPortrayInfo = "LyricsPortray";
    #endregion

    #region 

    public CloundMusicTop CloundMusicTopData { get; private set; }

    public CloundMusicDown CloundMusicDownData { get; private set; }

    public SerachSongsInfo SerachSongsInfoData { get; private set; }

    public LyricsPortray LyricsPortrayData { get; private set; }

    #endregion

    private void Awake( )
    {
        m_Instance = this;
        InitBindComponent( gameObject );
        InitializationLoadData( );
    }

    private void InitializationLoadData( )
    {
        //加载顶部的UI
        CloundMusicTopData = CloudMain.Instance.LoadAsset.LoadPrefabAsset( CloundMusicTop , m_Trans_TopInterface ).GetComponent<CloundMusicTop>( );

        //加载底部UI
        CloundMusicDownData = CloudMain.Instance.LoadAsset.LoadPrefabAsset( CloundMusicDown , m_Trans_DownInterface ).GetComponent<CloundMusicDown>( );

        //加载搜索界面
        SerachSongsInfoData = CloudMain.Instance.LoadAsset.LoadPrefabAsset( SerachSongsInfo , m_Trans_RigthInterface ).GetComponent<SerachSongsInfo>( );
        SerachSongsInfoData.SetActiveInHierarchy( false );
        LyricsPortrayData = CloudMain.Instance.LoadAsset.LoadPrefabAsset( LyricsPortrayInfo , m_Trans_FullScreen ).GetComponent<LyricsPortray>( );
        LyricsPortrayData.SetActive( false );
    }
}
