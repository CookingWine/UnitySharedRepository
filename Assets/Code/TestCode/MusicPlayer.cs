using SimpleJSON;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Networking;

public class MusicPlayer :MonoBehaviour
{
    public string MusicName;

    public int idss;

    public AudioSource audioSource;

    private CloudMusicAnalysin m_MusicAnalysin;

    public string Phone;

    public string PhoneCode;

    private void Awake( )
    {
        m_MusicAnalysin = new CloudMusicAnalysin( );
    }
    private void Update( )
    {
        if( Input.GetKeyDown( KeyCode.Space ) )
        {
            Debug.Log( "发送验证码" );
            StartCoroutine( RequestCloudMusic( CloudMusicAPI.SendVerificationCode( Phone ) ) );
        }

        if( Input.GetKeyDown( KeyCode.A ) )
        {
            Debug.Log( "验证码登录" );
            StartCoroutine( RequestCloudMusic( CloudMusicAPI.VerificationCodeLogin( Phone , PhoneCode ) ) );
        }

    }

    public IEnumerator RequestCloudMusic( string url )
    {
        UnityWebRequest request = new UnityWebRequest( url , UnityWebRequest.kHttpVerbGET )
        {
            timeout = 5
        };
        DownloadHandlerBuffer data = new DownloadHandlerBuffer( );
        request.downloadHandler = data;
        yield return request.SendWebRequest( );
        if( request.result == UnityWebRequest.Result.Success )
        {
            Debug.Log( $"请求成功数据为:\n{request.downloadHandler.text}" );
        }
        else
        {
            Debug.LogError( $"请求失败数据为\n{request.error}" );
        }
    }


    private IEnumerator RequestMusicInfo( string key )
    {
        string url = CloudMusicAPI.GetSongsInfo( key , 20 );
        using( UnityWebRequest request = new UnityWebRequest( url ) )
        {
            request.timeout = 5;

            DownloadHandlerBuffer data = new DownloadHandlerBuffer( );
            request.downloadHandler = data;
            yield return request.SendWebRequest( );
            if( request.result == UnityWebRequest.Result.Success )
            {
                AnalysinSongsData( request.downloadHandler.text , 20 );
                Debug.Log( "请求成功" );
            }
            else
            {
                Debug.LogError( request.error + "请求失败" );
            }
        }
    }

    /// <summary>
    /// 解析歌曲信息
    /// </summary>
    /// <param name="jsonData"></param>
    private void AnalysinSongsData( string jsonData , int count )
    {
        SearchSongsDataInfo.SongsData temp = m_MusicAnalysin.AnalysinSongsData( jsonData , count );
        for( int i = 0 ; i < temp.Songs.Count ; i++ )
        {
            Debug.Log( $"id->{temp.Songs[i].ID}\n歌手->{temp.Songs[i].Artists.Name}" );
        }
    }

    //1987823521

    // 解析歌曲信息
    private SongInfo ParseSongInfo( string responseJson )
    {
        Debug.Log( "解析歌曲信息为->" + responseJson );
        SongInfo songInfo = new SongInfo( );
        // 解析 JSON 数据
        JSONNode json = JSON.Parse( responseJson );
        songInfo.name = json["songs"][0]["name"];
        Debug.Log( $"当前歌曲名为{songInfo.name}" );
        songInfo.artist = json["songs"][0]["artists"][0]["name"];
        Debug.Log( $"当前歌手名为{songInfo.artist}" );
        songInfo.url = $"http://music.163.com/song/media/outer/url?id={json["songs"][0]["id"]}.mp3";
        Debug.Log( $"获取的url为{songInfo.url}" );
        return songInfo;
    }
    // 歌曲信息
    private struct SongInfo
    {
        public string name; // 歌曲名
        public string artist; // 歌手名
        public string url; // 歌曲播放链接
    }
    // 播放指定ID的歌曲
    public void PlaySong( int songId )
    {
        StartCoroutine( GetSongInfo( songId ) );
    }

    // 获取歌曲信息
    private IEnumerator GetSongInfo( int songId )
    {
        // 构造 API 请求 URL
        string url = $"http://music.163.com/api/song/detail/?id={songId}&ids=[{songId}]";
        // 发送 API 请求并获取响应
        using( UnityWebRequest request = UnityWebRequest.Get( url ) )
        {
            yield return request.SendWebRequest( );
            if( request.result == UnityWebRequest.Result.Success )
            {
                PlaySongsInfo.SongsData temp = m_MusicAnalysin.AnalysinPlaySongData( request.downloadHandler.text );


                //// 解析响应
                //SongInfo songInfo = ParseSongInfo( request.downloadHandler.text );
                // 显示歌曲信息
                // 播放歌曲
                PlayMusic( CloudMusicAPI.GetRequestMP3URL( temp ) );
            }
            else
            {
                Debug.LogErrorFormat( "GetSongInfo error: {0}" , request.error );
            }
        }
    }
    // 播放音乐
    private void PlayMusic( string musicUrl )
    {
        if( string.IsNullOrEmpty( musicUrl ) )
        {
            throw new System.Exception( "url为空" );
        }
        StartCoroutine( DownloadMusic( musicUrl ) );
    }

    // 下载音乐
    private IEnumerator DownloadMusic( string musicUrl )
    {

        using( UnityWebRequest request = UnityWebRequestMultimedia.GetAudioClip( musicUrl , AudioType.MPEG ) )
        {
            yield return request.SendWebRequest( );
            if( request.result == UnityWebRequest.Result.Success )
            {
                // 播放音乐
                AudioClip clip = DownloadHandlerAudioClip.GetContent( request );
                audioSource.clip = clip;
                audioSource.Play( );
            }
            else
            {
                Debug.LogErrorFormat( "DownloadMusic error: {0}" , request.error );
            }
        }
    }

}
