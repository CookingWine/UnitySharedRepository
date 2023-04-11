using System;
using UnityEngine;

//自动生成于:2023/4/10 17:02:38
public partial class SerachSongsDataItem :MonoBehaviour
{
    private SearchSongsDataInfo.SongsInfo m_SongsData;
    public int SongsID
    {
        get
        {
            return m_SongsData.ID;
        }
    }
    private void Awake( )
    {
        InitBindComponent( gameObject );
        InitButtonData( );
    }

    private void InitButtonData( )
    {
        m_Btn_PlayMusic.onClick.AddListener( ( ) =>
        {
            if( m_SongsData.SongName.IsNullOrEmpty( ) )
            {
                return;
            }
            CloundMusicInterface.Instance.CloundMusicDownData.PlayCloundMusic( m_SongsData );
        } );

        m_Btn_AddMusicToList.onClick.AddListener( ( ) =>
        {
            if( m_SongsData.SongName.IsNullOrEmpty( ) )
            {
                return;
            }
            CloundMusicInterface.Instance.MusicPlayList.AddToPlayList( m_SongsData );
        } );
    }

    public void SetEnable( bool enable )
    {
        gameObject.SetActive( enable );
        SetShowSongsData( );
    }

    public SerachSongsDataItem UpdateData( SearchSongsDataInfo.SongsInfo data )
    {
        m_SongsData = data;
        SetShowSongsData( );
        return this;
    }
    private void SetShowSongsData( )
    {
        if( m_SongsData.SongName.IsNullOrEmpty( ) )
        {
            return;
        }
        m_Txt_MusicName.text = m_SongsData.SongName;
        m_Txt_ArtistsName.text = m_SongsData.Artists.Name;
        m_Txt_AlbumInfo.text = m_SongsData.Album.Name;
        m_Txt_PlayTotalTime.text = GetPlayTotalTime( m_SongsData.Duration );
        m_Img_VipMusic.SetActive( m_SongsData.fee == 1 );
    }

    private string GetPlayTotalTime( long time )
    {
        TimeSpan timeSpan = TimeSpan.FromMilliseconds( time );
        string formattedTime = string.Format( "{0:D2}:{1:D2}" ,
                                timeSpan.Minutes + timeSpan.Hours * 60 ,
                                timeSpan.Seconds );
        return formattedTime;
    }
}
