using UnityEngine;

//自动生成于:2023/4/11 9:59:03
public partial class CloundMusicPlayList :MonoBehaviour
{
    private SearchSongsDataInfo.SongsData m_PlayList = new SearchSongsDataInfo.SongsData( );
    private RecyclingListView m_ListView;
    private void Awake( )
    {
        InitBindComponent( gameObject );
        m_PlayList.Songs = new System.Collections.Generic.List<SearchSongsDataInfo.SongsInfo>( );
        m_ListView = m_Trans_PlayList.GetComponent<RecyclingListView>( );
        m_ListView.ItemCallback = PopulateItem;
        m_Btn_ClearAll.onClick.AddListener( ( ) =>
        {
            m_PlayList.Songs.Clear( );
            m_ListView.Clear( );
        } );
    }
    private void OnEnable( )
    {
        m_Txt_CurrentNumer.text = $"总{m_PlayList.Songs.Count}首";
    }

    public int GetIndex( SearchSongsDataInfo.SongsInfo data )
    {
        for( int i = 0 ; i < m_PlayList.Songs.Count ; i++ )
        {
            if( m_PlayList.Songs[i].ID == data.ID )
            {
                return i;
            }
        }
        return -1;
    }

    public int AddToPlayList( SearchSongsDataInfo.SongsInfo data )
    {
        if( !m_PlayList.Songs.Contains( data ) )
        {
            m_PlayList.Songs.Add( data );
            m_ListView.RowCount = m_PlayList.Songs.Count;
            return m_PlayList.Songs.Count - 1;
        }
        else
        {
            int index = 0;
            for( int i = 0 ; i < m_PlayList.Songs.Count ; i++ )
            {
                if( m_PlayList.Songs[i].ID == data.ID )
                {
                    index = i;
                    break;
                }
            }
            return index;
        }
    }
    private void PopulateItem( RecyclingListViewItem item , int rowIndex )
    {
        var date = item as PlayListItem;
        date.SetSongsInfo( m_PlayList.Songs[rowIndex] , rowIndex );

    }
}
