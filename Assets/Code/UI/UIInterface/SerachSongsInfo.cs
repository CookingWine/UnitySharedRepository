using System.Collections.Generic;
using UnityEngine;

//自动生成于:2023/4/10 17:04:53
public partial class SerachSongsInfo :MonoBehaviour
{
    private readonly List<SearchSongsDataInfo.SongsData> m_SongsInfo = new List<SearchSongsDataInfo.SongsData>( );

    private readonly SerachSongsDataItem[] m_SongsItem = new SerachSongsDataItem[10];

    private int m_CurrentPagination;

    private void Awake( )
    {
        InitBindComponent( gameObject );
        LoadSongsItemData( );
        InitBtnData( );
    }
    private void LoadSongsItemData( )
    {
        for( int i = 0 ; i < 10 ; i++ )
        {
            SerachSongsDataItem item = CloudMain.Instance.LoadAsset.LoadPrefabAsset( "SerachSongsDataItemData" , m_Trans_MiddleSongData ).GetComponent<SerachSongsDataItem>( );
            m_SongsItem[i] = item;
        }
    }
    private void InitBtnData( )
    {
        m_Btn_CloseSerach.onClick.AddListener( ( ) =>
        {
            SetActiveInHierarchy( false );
            for( int i = 0 ; i < m_SongsItem.Length ; i++ )
            {
                m_SongsItem[i] = null;
            }
        } );

    }

    public SerachSongsInfo SetActiveInHierarchy( bool enable )
    {
        gameObject.SetActive( enable );
        m_SongsInfo.Clear( );
        m_CurrentPagination = 0;
        return this;
    }

    public SerachSongsInfo AddData( SearchSongsDataInfo.SongsData data )
    {
        m_SongsInfo.Add( data );
        for( int i = 0 ; i < data.Songs.Count ; i++ )
        {
            m_SongsItem[i].UpdateData( data.Songs[i] );
        }
        m_CurrentPagination = 0;
        m_Txt_Pagination.text = ( m_CurrentPagination + 1 ).ToString( );
        return this;
    }

    public SerachSongsInfo Clear( )
    {
        m_SongsInfo.Clear( );
        return this;
    }
}
