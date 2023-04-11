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

    #region 

    public CloundMusicTop CloundMusicTopData { get; private set; }

    public CloundMusicDown CloundMusicDownData { get; private set; }

    public SerachSongsInfo SerachSongsInfoData { get; private set; }

    public LyricsPortray LyricsPortrayData { get; private set; }

    public CloundMusicPlayList MusicPlayList { get; private set; }

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
        CloundMusicTopData = CloudMain.Instance.LoadAsset.LoadPrefabAsset( ExcelConfigData.CloundMusicTop , m_Trans_TopInterface ).GetComponent<CloundMusicTop>( );

        //加载底部UI
        CloundMusicDownData = CloudMain.Instance.LoadAsset.LoadPrefabAsset( ExcelConfigData.CloundMusicDown , m_Trans_DownInterface ).GetComponent<CloundMusicDown>( );

        //加载搜索界面
        SerachSongsInfoData = CloudMain.Instance.LoadAsset.LoadPrefabAsset( ExcelConfigData.SerachSongsInfo , m_Trans_RigthInterface ).GetComponent<SerachSongsInfo>( );
        SerachSongsInfoData.SetActiveInHierarchy( false );

        LyricsPortrayData = CloudMain.Instance.LoadAsset.LoadPrefabAsset( ExcelConfigData.LyricsPortray , m_Trans_FullScreen ).GetComponent<LyricsPortray>( );
        LyricsPortrayData.SetActive( false );

        MusicPlayList = CloudMain.Instance.LoadAsset.LoadPrefabAsset( ExcelConfigData.CloundMusicPlayList , m_Trans_PoPInterface ).GetComponent<CloundMusicPlayList>( );
        MusicPlayList.SetActive( false );
    }
}
