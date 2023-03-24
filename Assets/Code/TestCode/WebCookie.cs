using System;
using System.Collections;
using UnityEngine;
using UnityEngine.Networking;

public class WebCookie :MonoBehaviour
{
    CloudMusic.API.SearchSongsData.SongsData searchSongs;

    public string SongsName;

    public int Length;
    private void Start( )
    {
        searchSongs = new CloudMusic.API.SearchSongsData.SongsData( );
    }

    private void Update( )
    {
        if( Input.GetKeyDown( KeyCode.Space ) )
        {
            long time = ConvertDateTimeToUtc_13( );
            Debug.Log( time );
            StartCoroutine( RequestMusicInfo( ) );
        }

    }

    private IEnumerator RequestMusicInfo( )
    {
        string url = string.Empty;
        UnityWebRequest request = new UnityWebRequest( url )
        {
            timeout = 5
        };

        DownloadHandlerBuffer data = new DownloadHandlerBuffer( );
        request.downloadHandler = data;
        yield return request.SendWebRequest( );
        if( request.result == UnityWebRequest.Result.Success )
        {
            Debug.Log( $"请求到的数据为->{request.downloadHandler.text}" );
        }
        else
        {
            Debug.LogError( request.error + "请求失败" );
        }

    }

    public string GetMD5( string key )
    {
        return key.MD5EncryptString( );
    }


    private IEnumerator RequestMusicInfo( string key , int count )
    {
        string url = CloudMusic.API.SearchSongsData.GetSearchSongsUrl( key , count );
        UnityWebRequest request = new UnityWebRequest( url )
        {
            timeout = 5
        };

        DownloadHandlerBuffer data = new DownloadHandlerBuffer( );
        request.downloadHandler = data;
        yield return request.SendWebRequest( );
        if( request.result == UnityWebRequest.Result.Success )
        {
            searchSongs.GetSongsData( request.downloadHandler.text , count );
            Debug.Log( $"请求成功,歌曲总数量为{searchSongs.SongCount}，已经请求到的数据长度为{searchSongs.Songs.Count}" );
            for( int i = 0 ; i < searchSongs.Songs.Count ; i++ )
            {
                searchSongs.Songs[i].DebuLogInfo( );
            }
        }
        else
        {
            Debug.LogError( request.error + "请求失败" );
        }
    }

    /// <summary>
    /// 时间转化为13位时间戳
    /// </summary>
    /// <param name="_time">获取的时间</param>
    /// <returns></returns>
    public static long ConvertDateTimeToUtc_13( )
    {
        TimeSpan timeSpan = DateTime.Now.ToUniversalTime( ) - new DateTime( 1970 , 1 , 1 , 0 , 0 , 0 , 0 );
        return Convert.ToInt64( timeSpan.TotalMilliseconds );
    }
}