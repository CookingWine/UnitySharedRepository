using System.Collections;
using UnityEngine;
using UnityEngine.Networking;
using UnityEngine.UI;

//自动生成于:2023/4/7 14:54:32
public partial class CloundMusicTop :MonoBehaviour
{
    private void Awake( )
    {
        InitBindComponent( gameObject );
        InitializationData( );
    }

    private void InitializationData( )
    {
        m_Txt_MusicTitle.text = "Clound music";
        m_Btn_SearchSongs.onClick.AddListener( ClickSearchSongsBtnCallback );
        Debug.Log( "初始化数据完毕" );
    }
    /// <summary>
    /// 设置头像
    /// </summary>
    /// <param name="icon"></param>
    public void SetUserIcon( Sprite icon )
    {
        m_Img_UserIcon.sprite = icon;
    }

    /// <summary>
    /// 点击搜索按钮
    /// </summary>
    private void ClickSearchSongsBtnCallback( )
    {
        if( m_Input_SearchSongs.text.IsNullOrEmpty( ) )
        {
            return;
        }
        StartCoroutine( RequestMusicCount( m_Input_SearchSongs.text ) );
    }

    private IEnumerator RequestMusicCount( string key )
    {
        string url = CloudMusicRequestUrl.GetSongsInfo( key , 10 );
        using UnityWebRequest request = new UnityWebRequest( url );
        request.timeout = 5;
        DownloadHandlerBuffer data = new DownloadHandlerBuffer( );
        request.downloadHandler = data;
        yield return request.SendWebRequest( );
        if( request.result == UnityWebRequest.Result.Success )
        {
            SearchSongsDataInfo.SongsData music = CloudMusicAnalysin.AnalysinSongsData( request.downloadHandler.text , 10 );
            //Debug.Log( request.downloadHandler.text );
            CloundMusicInterface.Instance.SerachSongsInfoData.SetActiveInHierarchy( true ).AddData( music );
        }
        else
        {
            Debug.LogError( "请求失败" );
        }
    }
}
