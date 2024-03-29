using UnityEngine;
using UnityEngine.UI;

//自动生成于:2023/4/6 15:02:10
public partial class SongsData :RecyclingListViewItem
{
    /// <summary>
    /// 歌曲ID
    /// </summary>
    public int SongsID { get; private set; }

    /// <summary>
    /// 歌名
    /// </summary>
    public string SongsName { get; private set; }

    /// <summary>
    /// 艺术家
    /// </summary>
    public string Artist { get; private set; }

    /// <summary>
    /// 专辑
    /// </summary>
    public string Album { get; private set; }

    public SearchSongsDataInfo.SongsInfo SongsDataInfo { get; private set; }

    protected override void OnAwake( )
    {
        InitBindComponent( gameObject );
        m_Txt_SongsDataLonghair.text = Artist;
        m_Txt_SongsDataSongsName.text = SongsName;
        m_Txt_Album.text = Album;
        m_Btn_PlayerMusic.onClick.AddListener( ( ) =>
        {
            MainInterface.Instance.PlayMusic( SongsID );
        } );
        m_Btn_AddPlayList.onClick.AddListener( ( ) =>
        {
            MainInterface.Instance.AddPlayList( SongsDataInfo );
        } );
    }

    public void SetSongsDataInfo( SearchSongsDataInfo.SongsInfo data )
    {
        SongsDataInfo = data;
        SongsID = data.ID;
        SongsName = data.SongName;
        Artist = data.Artists.Name;
        Album = data.Album.Name;
        if( m_Txt_SongsDataLonghair != null )
        {
            m_Txt_SongsDataLonghair.text = Artist;
        }
        if( m_Txt_SongsDataSongsName != null )
        {
            m_Txt_SongsDataSongsName.text = SongsName;
        }
        if( m_Txt_Album != null )
        {
            m_Txt_Album.text = data.Album.Name;
        }
    }

}
