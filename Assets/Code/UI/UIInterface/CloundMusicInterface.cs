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

    #region Const
    /// <summary>
    /// 顶部UI的名字
    /// </summary>
    private const string CloundMusicTop = "CloundMusicTop";
    #endregion

    #region 
    public CloundMusicDown CloundMusicDownData { get; private set; }
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
        CloundMusicDownData = CloudMain.Instance.LoadAsset.LoadPrefabAsset( CloundMusicTop , m_Trans_TopInterface ).GetComponent<CloundMusicDown>( );

    }
}
