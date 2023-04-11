using UnityEngine;
using UnityEngine.UI;

//自动生成于:2023/4/11 9:56:33
public partial class PlayListItem :RecyclingListViewItem
{
    public int SongsID { get { return m_SongsInfo.ID; } }

    public int CurrentIndex { get; private set; }

    private SearchSongsDataInfo.SongsInfo m_SongsInfo;
    protected override void OnAwake( )
    {
        InitBindComponent( gameObject );
    }


    public void SetSongsInfo( SearchSongsDataInfo.SongsInfo data , int index )
    {
        m_SongsInfo = data;
        CurrentIndex = index;
    }

    private void OnEnable( )
    {
        UpdateInfo( );
    }
    private void UpdateInfo( )
    {
        m_Txt_SongsName.text = m_SongsInfo.SongName;
        m_Txt_ArtistsName.text = m_SongsInfo.Artists.Name;
    }
}
