using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Networking;

public partial class MainInterface :MonoBehaviour
{
    private static MainInterface m_Instance;
    public static MainInterface Instance
    {
        get
        {
            if( m_Instance == null )
            {
                Debug.LogError( "异常错误" );
            }
            return m_Instance;
        }
    }

    /// <summary>
    /// 音乐数据解析
    /// </summary>
    private CloudMusicAnalysin m_MusicAnalysin;
    /// <summary>
    /// 请求的歌曲数据
    /// </summary>
    private SearchSongsDataInfo.SongsData m_songsData;
    /// <summary>
    /// 显示请求歌曲的滚动列表
    /// </summary>
    private RecyclingListView m_ScrollList;
    /// <summary>
    /// 歌词界面
    /// </summary>
    private GameObject m_LyricsInterface;
    /// <summary>
    /// 歌曲的总时间
    /// </summary>
    private float m_SoudnTotalCount;
    /// <summary>
    /// 播放列表
    /// </summary>
    private readonly List<SearchSongsDataInfo.SongsInfo> m_PlayList = new List<SearchSongsDataInfo.SongsInfo>( );
    public AudioSource Source_DownLyric { get { return m_Source_DownLyric; } }

    public bool IsPlay { get { return m_Source_DownLyric.isPlaying; } }

    public Sprite SongsIcon
    {
        get;
        private set;
    }

    public int SongsID { get; private set; }
    private void Awake( )
    {
        m_Instance = this;
        m_MusicAnalysin = new CloudMusicAnalysin( );
        InitBindComponent( gameObject );
        InitInterfaceInfo( );
    }
    private void Update( )
    {
        string[] timer = m_Source_DownLyric.GetSoundCurrentTimeMsec( ).Split( ':' );
        string totalTimer;
        if( timer[0].ToInt( ) > 0 )
        {
            totalTimer = m_Source_DownLyric.GetSoundCurrentTimeMsec( );
        }
        else
        {
            totalTimer = $"{timer[1]}:{timer[2]}";
        }
        m_TTxt_CurrentPlayTime.text = totalTimer;
        m_Img_SongsProgressBar.fillAmount= m_Source_DownLyric.time / m_SoudnTotalCount;
        if( totalTimer == m_TotalTime )
        {
            MusicPlayOverCompeletn( );
        }
    }

    private void InitInterfaceInfo( )
    {
        #region Txt
        m_TTxt_CloudMusicTitle.text = "Cloud emo music";
        m_TTxt_CurrentPlayTime.text = "00:00";
        m_TTxt_TotalPlayTime.text = "00:00";
        #endregion

        #region Button
        m_Btn_Search.onClick.AddListener( ( ) =>
        {
            if( m_TInput_SearchSongs.text.IsNullOrEmpty( ) )
            {
                Debug.LogError( "搜索值为空" );
                return;
            }
            StartCoroutine( RequestMusicInfo( m_TInput_SearchSongs.text ) );
        } );
        m_Btn_PlayOrPause.onClick.AddListener( ( ) =>
        {
            if( m_Source_DownLyric.clip != null )
            {
                if( m_Source_DownLyric.isPlaying )
                {
                    m_Source_DownLyric.Pause( );
                }
                else
                {
                    m_Source_DownLyric.Play( );
                }
            }
        } );

        m_Btn_OpenPlayList.onClick.AddListener( ( ) =>
        {
            foreach( var item in m_PlayList )
            {
                Debug.Log( $"歌曲ID为{item.ID}歌曲名字为{item.SongName}" );
            }
        } );
        m_Btn_SongsIcon.onClick.AddListener( ( ) =>
        {
            if( m_LyricsInterface == null )
            {
                m_LyricsInterface = CloudMain.Instance.LoadAsset.LoadPrefabAsset( "LyricsInterface" , transform );
                m_LyricsInterface.SetActive( true );
            }
            else
            {
                m_LyricsInterface.SetActive( !m_LyricsInterface.activeInHierarchy );
            }
        } );
        #endregion

        m_ScrollList = m_Trans_CentreObjectBG.GetComponent<RecyclingListView>( );
        #region event
        m_ScrollList.ItemCallback = PopulateItem;
        #endregion
    }
    /// <summary>
    /// 播放歌曲
    /// </summary>
    /// <param name="id"></param>
    public void PlayMusic( int id )
    {
        SongsID = id;
        StartCoroutine( RequestPlayMusic( m_Source_DownLyric , id ) );
    }

    /// <summary>
    /// 添加播放列表
    /// </summary>
    /// <param name="id"></param>
    public void AddPlayList( SearchSongsDataInfo.SongsInfo data )
    {
        m_PlayList.Add( data );
    }
    /// <summary>
    /// 搜索歌曲
    /// </summary>
    /// <param name="key"></param>
    /// <returns></returns>
    private IEnumerator RequestMusicInfo( string key )
    {
        string url = CloudMusicAPI.GetSongsInfo( key , 100 );
        using UnityWebRequest request = new UnityWebRequest( url );
        //十秒的等待
        request.timeout = 10;
        DownloadHandlerBuffer data = new DownloadHandlerBuffer( );
        request.downloadHandler = data;
        yield return request.SendWebRequest( );
        if( request.result == UnityWebRequest.Result.Success )
        {
            m_songsData = CloudMusicAnalysin.AnalysinSongsData( request.downloadHandler.text , 100 );
            if( m_songsData.Songs.Count > 0 )
            {
                m_ScrollList.Clear( );
                m_ScrollList.RowCount = m_songsData.Songs.Count;
            }
        }
        else
        {
            Debug.LogError( "请求失败" );
        }

    }

    /// <summary>
    /// 播放歌曲
    /// </summary>
    /// <param name="audio"></param>
    /// <param name="id"></param>
    /// <returns></returns>
    private IEnumerator RequestPlayMusic( AudioSource audio , int id )
    {
        string url = $"http://music.163.com/api/song/detail/?id={id}&ids=[{id}]";
        UnityWebRequest request = UnityWebRequest.Get( url );
        yield return request.SendWebRequest( );
        if( request.result == UnityWebRequest.Result.Success )
        {
            PlaySongsInfo.SongsData temp = CloudMusicAnalysin.AnalysinPlaySongData( request.downloadHandler.text );
            StartCoroutine( SearchLyric( id ) );
            StartCoroutine( DownloadImage( temp.album ) );
            StartCoroutine( DownloadMusic( CloudMusicAPI.GetRequestMP3URL( temp ) , audio , temp ) );
        }
    }
    private string m_TotalTime;
    /// <summary>
    /// 下载music
    /// </summary>
    /// <param name="musicUrl"></param>
    /// <param name="type"></param>
    /// <returns></returns>
    private IEnumerator DownloadMusic( string musicUrl , AudioSource audio , PlaySongsInfo.SongsData songsData , AudioType type = AudioType.MPEG )
    {
        UnityWebRequest request = UnityWebRequestMultimedia.GetAudioClip( musicUrl , type );
        yield return request.SendWebRequest( );
        if( request.result == UnityWebRequest.Result.Success )
        {
            audio.clip = DownloadHandlerAudioClip.GetContent( request );
            UpdateShowSongsInfo( songsData );
            m_SoudnTotalCount = audio.clip.length;
            string[] timer = audio.clip.GetAudioClipTotalTime( ).Split( ':' );
            string totalTimer;
            if( timer[0].ToInt( ) > 0 )
            {
                totalTimer = audio.clip.GetAudioClipTotalTime( );
            }
            else
            {
                totalTimer = $"{timer[1]}:{timer[2]}";
            }
            m_TotalTime = totalTimer;
            m_TTxt_TotalPlayTime.text = totalTimer;
            audio.Play( );
            m_IsPlayOver = false;
        }
    }

    /// <summary>
    /// 搜索歌词
    /// </summary>
    /// <param name="id"></param>
    /// <returns></returns>
    private IEnumerator SearchLyric( int id )
    {
        using UnityWebRequest request = new UnityWebRequest( $"http://music.163.com/api/song/media?id={id}" );
        request.timeout = 10;
        DownloadHandlerBuffer data = new DownloadHandlerBuffer( );
        request.downloadHandler = data;
        yield return request.SendWebRequest( );
        if( request.result == UnityWebRequest.Result.Success )
        {
            LyricData.SongsLyricData lyricdata = new LyricData.SongsLyricData( );
            lyricdata = CloudMusicAnalysin.AnlysinLyricData( request.downloadHandler.text );
            foreach( var item in lyricdata.ArtistsLyric )
            {
                Debug.Log( $"{item.Key}:{item.Value}" );
            }
        }
        else
        {
            Debug.Log( "请求失败" );
        }
    }

    /// <summary>
    /// 下载图片
    /// </summary>
    /// <param name="data"></param>
    /// <returns></returns>
    private IEnumerator DownloadImage( PlaySongsInfo.AlbumInfo data )
    {
        UnityWebRequest request = UnityWebRequestTexture.GetTexture( data.blurPicUrl );
        yield return request.SendWebRequest( );
        if( request.result == UnityWebRequest.Result.Success )
        {
            Texture2D texture = ( (DownloadHandlerTexture)request.downloadHandler ).texture;
            m_Img_SongsIcon.sprite = texture.Texture2DToSprite( );
            SongsIcon = m_Img_SongsIcon.sprite;
        }
        else
        {
            Debug.LogError( "下载图片错误" );
        }
    }

    private void UpdateShowSongsInfo( PlaySongsInfo.SongsData data )
    {
        m_TTxt_SongsName.text = data.name;
        m_TTxt_Longhair.text = data.artists[0].name;
    }

    private void PopulateItem( RecyclingListViewItem item , int rowIndex )
    {
        var data = item as SongsData;
        data.SetSongsDataInfo( m_songsData.Songs[rowIndex] );
    }

    private bool m_IsPlayOver;
    /// <summary>
    /// 播放完成回调
    /// </summary>
    private void MusicPlayOverCompeletn( )
    {
        if( m_IsPlayOver )
        {
            return;
        }
        m_IsPlayOver = true;
        if( m_PlayList.Count != 0 )
        {
            PlayMusic( m_PlayList[0].ID );
            m_PlayList.RemoveAt( 0 );
        }
    }
}
