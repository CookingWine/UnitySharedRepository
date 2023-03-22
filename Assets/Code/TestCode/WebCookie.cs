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
            StartCoroutine( RequestMusicInfo( SongsName , Length ) );
        }

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
}