using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Networking;

//自动生成于:2023/4/7 15:42:53
public partial class CloundMusicDown :MonoBehaviour
{
    /// <summary>
    /// 播放列表
    /// </summary>
    private readonly List<SearchSongsDataInfo.SongsInfo> m_PlayMusicList = new List<SearchSongsDataInfo.SongsInfo>( );

    public Sprite SongsIcon { get; private set; }
    public int CurrentPlayIndex { get; private set; }

    /// <summary>
    /// 总时间
    /// </summary>
    private string m_TotalMusicTimer;

    private float m_TotalPlayMusicTimer;

    public bool IsPlay { get; set; }


    private void Awake( )
    {
        InitBindComponent( gameObject );
        InitializationData( );
    }

    private void Update( )
    {
        if( m_Source_CloundMusic.clip == null )
        {
            return;
        }
        else
        {
            IsPlay = true;
            string[] timer = m_Source_CloundMusic.GetSoundCurrentTimeMsec( ).Split( ':' );
            string totalTime;
            if( timer[0].ToInt( ) > 0 )
            {
                totalTime = m_Source_CloundMusic.GetSoundCurrentTime( );
            }
            else
            {
                totalTime = $"{timer[1]}:{timer[2]}";
            }
            m_Txt_CurrentTimer.text = totalTime;
            m_Img_ProgressBar.fillAmount = m_Source_CloundMusic.time / m_TotalPlayMusicTimer;
            if( totalTime == m_TotalMusicTimer )
            {
                IsPlay = false;
                m_TotalMusicTimer = "00:00";
                if( CloundMusicInterface.Instance.LyricsPortrayData != null )
                {
                    CloundMusicInterface.Instance.LyricsPortrayData.UpdateLyricsInfo( null );
                }
                PlayOverCompelent( );
            }
        }
    }

    /// <summary>
    /// 初始化数据
    /// </summary>
    private void InitializationData( )
    {
        m_Img_SongsIcon.SetColorAlpha( 0 );
        m_TotalMusicTimer = "00:00";
        m_Slider_Volume.value = 0.5f;
        m_Source_CloundMusic.volume = 0.5f;
        m_Btn_PlayOrPauser.onClick.AddListener( ( ) =>
        {
            if( m_Source_CloundMusic.clip != null )
            {
                if( m_Source_CloundMusic.isPlaying )
                {
                    m_Source_CloundMusic.Pause( );
                }
                else
                {
                    m_Source_CloundMusic.Play( );
                }
            }
        } );
        m_Btn_Volume.onClick.AddListener( ( ) =>
        {
            m_Slider_Volume.SetActive( !m_Slider_Volume.gameObject.activeInHierarchy );
        } );
        m_Btn_SongsIcon.onClick.AddListener( ( ) =>
        {
            CloundMusicInterface.Instance.LyricsPortrayData
            .UpdateLyricsInfo( SongsIcon ).SetActive( !CloundMusicInterface.Instance.LyricsPortrayData.gameObject.activeInHierarchy );
        } );
        m_Img_ProgressBar.fillAmount = 0;
        m_Slider_Volume.onValueChanged.AddListener( OnChangeVolumeCompelet );
    }
    /// <summary>
    /// 播放
    /// </summary>
    /// <param name="data"></param>
    public void PlayCloundMusic( SearchSongsDataInfo.SongsInfo data )
    {
        if( !m_PlayMusicList.Contains( data ) )
        {
            m_PlayMusicList.Add( data );
            CurrentPlayIndex = m_PlayMusicList.Count - 1;
        }
        else
        {
            CurrentPlayIndex = m_PlayMusicList.IndexOf( data );
        }
        StartCoroutine( RequestPlayMusic( data.ID ) );
        m_Txt_SongsName.text = data.SongName;
        m_Txt_ArtistsName.text = data.Artists.Name;
    }

    /// <summary>
    /// 添加到播放列表
    /// </summary>
    public void AddMusicToPlayList( SearchSongsDataInfo.SongsInfo music )
    {
        if( m_PlayMusicList.Contains( music ) )
        {
            Debug.LogError( "当前列表内有该歌曲" );
            return;
        }
        m_PlayMusicList.Add( music );
    }

    /// <summary>
    /// 请求歌曲
    /// </summary>
    /// <param name="id"></param>
    /// <returns></returns>
    private IEnumerator RequestPlayMusic( int id )
    {
        string url = $"http://music.163.com/api/song/detail/?id={id}&ids=[{id}]";
        UnityWebRequest request = UnityWebRequest.Get( url );
        yield return request.SendWebRequest( );
        if( request.result == UnityWebRequest.Result.Success )
        {
            PlaySongsInfo.SongsData data = CloudMusicAnalysin.AnalysinPlaySongData( request.downloadHandler.text );
            StartCoroutine( DownloadImage( data.album.blurPicUrl ) );

            StartCoroutine( DownloadMusic( CloudMusicAPI.GetRequestMP3URL( data ) ) );
        }
        else
        {
            Debug.LogError( "请求失败" + request.method );
        }
    }

    /// <summary>
    /// 下载
    /// </summary>
    /// <param name="url"></param>
    /// <param name="type"></param>
    /// <returns></returns>
    private IEnumerator DownloadMusic( string url , AudioType type = AudioType.MPEG )
    {
        UnityWebRequest request = UnityWebRequestMultimedia.GetAudioClip( url , type );
        yield return request.SendWebRequest( );
        if( request.result == UnityWebRequest.Result.Success )
        {
            AudioClip clip = DownloadHandlerAudioClip.GetContent( request );
            m_TotalPlayMusicTimer = clip.length;
            string[] timer = clip.GetAudioClipTotalTime( ).Split( ':' );
            string totalTimer;
            if( timer[0].ToInt( ) > 0 )
            {
                totalTimer = clip.GetAudioClipTotalTime( );
            }
            else
            {
                totalTimer = $"{timer[1]}:{timer[2]}";
            }
            m_TotalMusicTimer = totalTimer;
            m_Txt_TotalTimer.text = totalTimer;
            m_Source_CloundMusic.clip = clip;
            m_Source_CloundMusic.Play( );
        }
        else
        {
            Debug.LogError( "下载mp3失败" + request.method );
        }
    }

    /// <summary>
    /// 下载图片
    /// </summary>
    /// <param name="url"></param>
    /// <returns></returns>
    private IEnumerator DownloadImage( string url )
    {
        UnityWebRequest request = UnityWebRequestTexture.GetTexture( url );
        yield return request.SendWebRequest( );
        if( request.result == UnityWebRequest.Result.Success )
        {
            Texture2D texture = ( (DownloadHandlerTexture)request.downloadHandler ).texture;
            m_Img_SongsIcon.sprite = texture.Texture2DToSprite( );
            m_Img_SongsIcon.SetColorAlpha( 1 );
            SongsIcon = m_Img_SongsIcon.sprite;
        }
        else
        {
            Debug.LogError( "下载失败" + request.error );
        }
    }

    private void OnChangeVolumeCompelet( float value )
    {
        m_Source_CloundMusic.volume = value;
    }

    private void PlayOverCompelent( )
    {

    }
}