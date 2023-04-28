using SimpleJSON;
using System.Collections;
using UnityEngine;
using UnityEngine.Networking;

//自动生成于:2023/4/7 15:42:53
public partial class CloundMusicDown :MonoBehaviour
{
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
    /*
     * 半岛铁盒
     * https://dl.stream.qqmusic.qq.com/C4000019GXTz1OM6Wu.m4a?guid=2691270072&vkey=5C53A7C950DD9F65CDF3B374CA3550A287813B028892EE797560330A6109CF0A6DF8275F1A39037230A6B66D614EEF76AA2E06389D5B9927&uin=3181983989&fromtag=120032
     * 晴天
     * https://dl.stream.qqmusic.qq.com/C400002202B43Cq4V4.m4a?guid=2691270072&vkey=48D5B8E8BD4112C970919B0740B53C5F34E929829224DD845FCC1428DDA4D49AEF14442F244CB6F415710C3539776C990B1211B774E04F94&uin=3181983989&fromtag=120032
     */
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
                if( CloundMusicInterface.Instance.LyricsPortrayData != null )
                {
                    CloundMusicInterface.Instance.LyricsPortrayData.UpdateLyricsInfo( null );
                }
                PlayOverCompelent( );
                totalTime = string.Empty;
                return;
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
        m_Btn_PlayList.onClick.AddListener( ( ) =>
        {
            CloundMusicInterface.Instance.MusicPlayList.SetActive( !CloundMusicInterface.Instance.MusicPlayList.gameObject.activeInHierarchy );
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
        CurrentPlayIndex = CloundMusicInterface.Instance.MusicPlayList.AddToPlayList( data );
        //HttpRequest.Instance.CreateCloudRequet( $"https://api-unm.imsyy.top/match?id={data.ID}&server=qq,pyncmd,kugou" , 10 , ( data ) =>{},(data)=>{});
        StartCoroutine( RequestPlayMusic( data.ID ) );
        m_Txt_SongsName.text = data.SongName;
        m_Txt_ArtistsName.text = data.Artists.Name;
    }

    /// <summary>
    /// 请求歌曲
    /// </summary>
    /// <param name="id"></param>
    /// <returns></returns>
    private IEnumerator RequestPlayMusic( int id )
    {
        Debug.Log( id );
        string url = HttpRequest.Instance.RequestUrl + $"/song/url?id={id}";

        UnityWebRequest request = UnityWebRequest.Get( url );
        yield return request.SendWebRequest( );
        if( request.result == UnityWebRequest.Result.Success )
        {
            //PlaySongsInfo.SongsData data = CloudMusicAnalysin.AnalysinPlaySongData( request.downloadHandler.text );
            //StartCoroutine( DownloadImage( data.album.blurPicUrl ) );
            JSONNode data = JSON.Parse( request.downloadHandler.text );
            Debug.Log( data["code"] );
            string geturl = data["data"][0]["url"];
            StartCoroutine( DownloadMusic(geturl ) );
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
        Debug.Log( url );
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

    /// <summary>
    /// 播放完成
    /// </summary>
    private void PlayOverCompelent( )
    {
        Debug.Log( "当前索引为" );
    }
}
