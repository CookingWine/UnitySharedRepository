using UnityEngine;
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

    }
}
