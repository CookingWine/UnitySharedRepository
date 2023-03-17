using System.Collections;
using UnityEngine;
using UnityEngine.Networking;

public class MusicPlayer :MonoBehaviour
{
    public string MusicName;

    public int idss;

    public AudioClip audioClip;

    public AudioSource audioSource;

    private void Update( )
    {
        if( Input.GetKeyDown( KeyCode.Space ) )
        {
            Debug.Log( "开始搜索" );
            StartCoroutine( RequestMusicInfo( MusicName ) );
        }
        if( Input.GetKeyDown( KeyCode.A ) )
        {
            Debug.Log( "开始播放" );
            PlaySong( idss );
        }
    }


    private IEnumerator RequestMusicInfo( string key )
    {
        string url = $"http://music.163.com/api/search/get/web?csrf_token=&type=1&offset=0&total=true&limit=10&s={key}";

        using( UnityWebRequest request = new UnityWebRequest( url ) )
        {
            request.timeout = 5;

            DownloadHandlerBuffer data = new DownloadHandlerBuffer( );
            request.downloadHandler = data;
            yield return request.SendWebRequest( );
            if( request.result == UnityWebRequest.Result.Success )
            {
                Debug.Log( request.downloadHandler.text );
                Debug.Log( "请求成功" );
            }
            else
            {
                Debug.LogError( request.error + "请求失败" );
            }
        }
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
                // 解析响应
                SongInfo songInfo = ParseSongInfo( request.downloadHandler.text );
                // 显示歌曲信息
                // 播放歌曲
                PlayMusic( songInfo.url );
            }
            else
            {
                Debug.LogErrorFormat( "GetSongInfo error: {0}" , request.error );
            }
        }
    }

    // 解析歌曲信息
    private SongInfo ParseSongInfo( string responseJson )
    {
        SongInfo songInfo = new SongInfo( );
        // 解析 JSON 数据
        SimpleJSON.JSONNode json = SimpleJSON.JSON.Parse( responseJson );
        songInfo.name = json["songs"][0]["name"];
        Debug.Log( $"当前歌曲名为{songInfo.name}" );
        songInfo.artist = json["songs"][0]["artists"][0]["name"];
        Debug.Log( $"当前歌手名为{songInfo.artist}" );
        songInfo.url = $"http://music.163.com/song/media/outer/url?id={json["songs"][0]["id"]}.mp3";
        return songInfo;
    }

    // 播放音乐
    private void PlayMusic( string musicUrl )
    {
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
