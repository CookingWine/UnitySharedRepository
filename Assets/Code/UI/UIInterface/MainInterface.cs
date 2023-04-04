using CloudMusic.API;
using System;
using System.Collections;
using System.Collections.Generic;
using System.IO;
using System.Net;
using UnityEngine;
using UnityEngine.Networking;

public partial class MainInterface :MonoBehaviour
{
    private CloudMusicAnalysin m_MusicAnalysin;
    private SearchSongsDataInfo.SongsData m_songsData;
    private int m_CurrentIndex;
    private void Awake( )
    {
        m_MusicAnalysin = new CloudMusicAnalysin( );
        InitBindComponent( gameObject );
        InitInterfaceInfo( );
    }

    private void InitInterfaceInfo( )
    {
        m_TTxt_CloudMusicTitle.text = "Cloud emo music";
        m_Btn_Search.onClick.AddListener( ( ) =>
        {
            if( m_TInput_SearchSongs.text.IsNullOrEmpty( ) )
            {
                Debug.LogError( "搜索值为空" );
                return;
            }
            Debug.Log( $"当前搜索为{m_TInput_SearchSongs.text}" );
            StartCoroutine( RequestMusicInfo( m_TInput_SearchSongs.text ) );
        } );
        m_Btn_PlayerMusic.onClick.AddListener( ( ) =>
        {
            StartCoroutine( RequestPlayMusic( m_Source_DownLyric , m_songsData.Songs[m_CurrentIndex].ID ) );
        } );
    }
    /// <summary>
    /// 搜索歌曲
    /// </summary>
    /// <param name="key"></param>
    /// <returns></returns>
    private IEnumerator RequestMusicInfo( string key )
    {
        string url = CloudMusicAPI.GetSongsInfo( key , 20 );
        using UnityWebRequest request = new UnityWebRequest( url );
        //十秒的等待
        request.timeout = 10;
        DownloadHandlerBuffer data = new DownloadHandlerBuffer( );
        request.downloadHandler = data;
        yield return request.SendWebRequest( );
        if( request.result == UnityWebRequest.Result.Success )
        {
            m_songsData = m_MusicAnalysin.AnalysinSongsData( request.downloadHandler.text , 20 );
            if( m_songsData.Songs.Count > 0 )
            {
                UpdateSongsInfo( 0 );
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
            PlaySongsInfo.SongsData temp = m_MusicAnalysin.AnalysinPlaySongData( request.downloadHandler.text );
            StartCoroutine( SearchLyric( id ) );
            StartCoroutine( DownloadMusic( CloudMusicAPI.GetRequestMP3URL( temp ) , audio ) );
        }
    }

    /// <summary>
    /// 下载music
    /// </summary>
    /// <param name="musicUrl"></param>
    /// <param name="type"></param>
    /// <returns></returns>
    private IEnumerator DownloadMusic( string musicUrl , AudioSource audio , AudioType type = AudioType.MPEG )
    {
        UnityWebRequest request = UnityWebRequestMultimedia.GetAudioClip( musicUrl , type );
        yield return request.SendWebRequest( );
        if( request.result == UnityWebRequest.Result.Success )
        {
            audio.clip = DownloadHandlerAudioClip.GetContent( request );
            audio.Play( );
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
            lyricdata = m_MusicAnalysin.AnlysinLyricData( request.downloadHandler.text );
        }
        else
        {
            Debug.Log( "请求失败1488340401" );
        }
    }


    private void UpdateSongsInfo( int index )
    {
        if( index >= m_songsData.Songs.Count )
        {
            index = m_songsData.Songs.Count - 1;
        }
        else if( index <= 0 )
        {
            index = 0;
        }
        m_TTxt_SongsDataSongsName.text = m_songsData.Songs[index].SongName;
        m_TTxt_SongsDataLonghair.text = m_songsData.Songs[index].Artists.Name;
        m_CurrentIndex = index;
    }
}
