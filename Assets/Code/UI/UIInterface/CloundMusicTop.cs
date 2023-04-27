using CloudMusic.API;
using System.Collections;
using UnityEngine;
using UnityEngine.Networking;

//自动生成于:2023/4/7 14:54:32
public partial class CloundMusicTop :MonoBehaviour
{
    private CloudUserInfo m_CloudUserInfo;
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

    public void UpdateUserInfo( string info )
    {
        m_CloudUserInfo = new CloudUserInfo( info );
        m_Txt_UserName.text = m_CloudUserInfo.Nickname;
        HttpRequest.Instance.CreateTextureRequet( m_CloudUserInfo.AvatarUrl , ( data ) =>
        {
            m_Img_UserIcon.sprite = data.Texture2DToSprite( );
        } , ( data ) =>
        {
            Debug.LogError( data );
        } );
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
            string requet = HttpRequest.Instance.RequestUrl + "/" + CloudMusicAPI.CloudMusicWebRequest.PersonslFM( );
            HttpRequest.Instance.CreateCloudRequet( requet , 5 , ( data ) =>
            {
                Debug.Log( data.text );
                FileExtend.CreationFileWirteData( "D:\\test.txt" , data.text );
            } ,
            ( message ) =>
            {
                
            } );
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
            CloundMusicInterface.Instance.SerachSongsInfoData.SetActiveInHierarchy( true ).AddData( music );
        }
        else
        {
            Debug.LogError( "请求失败" );
        }
    }
}

/// <summary>
/// 用户信息
/// </summary>
public class CloudUserInfo
{
    ///<summary>id</summary>
    public int UserId { get; }
    ///<summary>名字</summary>
    public string Nickname { get; }
    ///<summary>头像url</summary>
    public string AvatarUrl { get; }
    ///<summary>背景url</summary>
    public string BackgroundImgId { get; }

    ///<summary>签名</summary>
    public string Signature { get; }

    ///<summary>创建时间</summary>
    public long CreateTime { get; }

    ///<summary>生日</summary>
    public long Birthday { get; }

    ///<summary>性别;0:未知</summary>
    public int Gender { get; }

    ///<summary>省份</summary>
    public long Province { get; }

    ///<summary>市</summary>
    public long City { get; }

    ///<summary>最后一次登录时间</summary>
    public long LastLoginTime { get; }

    ///<summary>最后一次登录IP</summary>
    public string LastLoginIP { get; }
    /// <summary>
    /// 用户信息
    /// </summary>
    public CloudUserInfo( string info )
    {
        SimpleJSON.JSONNode data = SimpleJSON.JSON.Parse( info );
        SimpleJSON.JSONNode profile = data["data"]["profile"];
        UserId = profile["userId"];
        Nickname = profile["nickname"];
        AvatarUrl = profile["avatarUrl"];
        BackgroundImgId = profile["backgroundImgId"];
        Signature = profile["signature"];
        CreateTime = profile["createTime"];
        Birthday = profile["birthday"];
        Gender = profile["gender"];
        Province = profile["province"];
        City = profile["city"];
        LastLoginTime = profile["lastLoginTime"];
        LastLoginIP = profile["lastLoginIP"];
    }
}
