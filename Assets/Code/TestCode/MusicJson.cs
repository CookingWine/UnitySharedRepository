using SimpleJSON;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// 别名信息
/// </summary>
public struct AliasInfo
{

    public string GetInfo( )
    {
        return "null";
    }
}

/// <summary>
/// 搜索歌曲的信息
/// </summary>
public class SearchSongsDataInfo
{
    /// <summary>
    /// 歌曲数据
    /// </summary>
    public struct SongsData
    {
        /// <summary>
        /// 歌曲数据
        /// </summary>
        public List<SongsInfo> Songs;

        /// <summary>
        /// 歌曲总数量
        /// </summary>
        public int SongCount;
    }

    /// <summary>
    /// 歌曲信息
    /// </summary>
    public struct SongsInfo
    {
        /// <summary>
        /// 歌曲ID
        /// </summary>
        public int ID;
        /// <summary>
        /// 歌曲名
        /// </summary>
        public string SongName;

        /// <summary>
        /// 歌手信息
        /// </summary>
        public ArtistsInfo Artists;

        /// <summary>
        /// 专辑信息
        /// </summary>
        public AlbumInfo Album;

        /// <summary>
        /// 持续时间
        /// </summary>
        public long Duration;

        /// <summary>
        /// 版权标识
        /// </summary>
        public long CopyrightId;

        /// <summary>
        /// 状态
        /// </summary>
        public int Status;

        /// <summary>
        /// 别名信息(该值一般为空,不用管)
        /// </summary>
        public AliasInfo[] Alias;

        public long RType;

        public long Ftype;

        /// <summary>
        /// mv id
        /// </summary>
        public long MVID;

        /// <summary>
        /// 费用
        /// </summary>
        public long fee;

        public string RUrl;

        /// <summary>
        /// 标记
        /// </summary>
        public long Mark;
    }

    /// <summary>
    /// 艺术家信息
    /// </summary>
    public struct ArtistsInfo
    {
        /// <summary>
        /// 歌手ID
        /// </summary>
        public int ID;

        /// <summary>
        /// 歌手名
        /// </summary>
        public string Name;

        public string PicUrl;

        /// <summary>
        /// 别名信息(该值一般为空,不用管)
        /// </summary>
        public AliasInfo[] Alias;

        /// <summary>
        /// 专辑数
        /// </summary>
        public int AlbumSize;

        public long PicId;

        /// <summary>
        /// 粉丝团
        /// </summary>
        public string FansGroup;

        public string img1v1Url;

        public long img1v1;

        public string trans;
    }

    /// <summary>
    /// 专辑信息
    /// </summary>
    public struct AlbumInfo
    {
        /// <summary>
        /// 专辑ID
        /// </summary>
        public int ID;

        /// <summary>
        /// 专辑名
        /// </summary>
        public string Name;

        /// <summary>
        /// 歌手
        /// </summary>
        public ArtistsInfo Artist;

        /// <summary>
        /// 发布时间
        /// </summary>
        public long publishTime;

        /// <summary>
        /// 规格
        /// </summary>
        public long Size;

        /// <summary>
        /// 版权标识
        /// </summary>
        public long CopyrightId;

        /// <summary>
        /// 状态
        /// </summary>
        public string status;

        public long picId;

        /// <summary>
        /// 标记
        /// </summary>
        public int mark;
    }
}

/// <summary>
/// 播放歌曲信息
/// </summary>
public class PlaySongsInfo
{
    /// <summary>
    /// 播放音乐请求的url
    /// <para>完整路径为url+id+.mp3(VIP歌曲暂时不能使用这个url进行播放)</para>
    /// </summary>
    private const string Request = "http://music.163.com/song/media/outer/url?id=";

    /// <summary>
    /// 获取请求播放mp3的url
    /// </summary>
    /// <param name="data"></param>
    /// <returns></returns>
    public string GetRequestMP3URL( SongsData data )
    {
        return Request + data.id + ".mp3";
    }

    /// <summary>
    /// 歌曲数据
    /// </summary>
    public struct SongsData
    {
        /// <summary>
        /// 歌曲名
        /// </summary>
        public string name;

        /// <summary>
        /// 歌曲ID
        /// </summary>
        public long id;

        /// <summary>
        /// 位置
        /// </summary>
        public int position;

        /// <summary>
        /// 别名
        /// </summary>
        public AliasInfo alias;

        /// <summary>
        /// 状态
        /// </summary>
        public int status;

        /// <summary>
        /// 费用
        /// </summary>
        public int fee;

        /// <summary>
        /// 有版权的
        /// </summary>
        public int copyrightId;

        /// <summary>
        /// 唱片
        /// </summary>
        public string disc;

        public int no;

        /// <summary>
        /// 艺术家信息
        /// </summary>
        public ArtistsInfo[] artists;

        /// <summary>
        /// 专辑信息
        /// </summary>
        public AlbumInfo album;

        /// <summary>
        /// 星号标记
        /// </summary>
        public bool starred;

        /// <summary>
        /// 流行
        /// </summary>
        public int popularity;

        /// <summary>
        /// 得分
        /// </summary>
        public int score;

        /// <summary>
        /// 主演次数
        /// </summary>
        public int starredNum;

        /// <summary>
        /// 持续时间
        /// </summary>
        public long duration;

        /// <summary>
        /// 播放次数
        /// </summary>
        public int playedNum;

        /// <summary>
        /// 播放天数
        /// </summary>
        public int dayPlays;

        /// <summary>
        /// 听到时间
        /// </summary>
        public int hearTime;

        /// <summary>
        /// 歌曲数据
        /// </summary>
        public MusicInfo sqMusic;

        /// <summary>
        /// 出版者
        /// </summary>
        public string hrMusic;

        /// <summary>
        /// 手机铃声
        /// </summary>
        public string ringtone;

        /// <summary>
        /// 个性化多彩回铃音业务
        /// </summary>
        public string crbt;

        /// <summary>
        /// 试听
        /// </summary>
        public string audition;

        /// <summary>
        /// 临摹
        /// </summary>
        public string copyFrom;

        public string commentThreadId;

        public string rtUrl;

        public string ftype;

        /// <summary>
        /// 这个值应该为空
        /// </summary>
        public string rtUrls;

        /// <summary>
        /// 版权
        /// </summary>
        public int copyright;

        /// <summary>
        /// 反转名字
        /// </summary>
        public string transName;

        public string sign;

        public int mark;

        public int originCoverType;

        public string originSongSimpleData;

        public int single;

        public string noCopyrightRcmd;

        public int rtype;

        public string rurl;

        public MusicInfo bMusic;

        public string mp3Url;

        public int mvid;

        public MusicInfo hMusic;

        public MusicInfo mMusic;

        public MusicInfo lMusic;
    }

    /// <summary>
    /// 艺术家信息
    /// </summary>
    public struct ArtistsInfo
    {
        public string name;

        public long id;

        public int picId;

        public int img1v1Id;

        public string briefDesc;

        public string picUrl;

        public string img1v1Url;

        public int albumSize;

        public AliasInfo alias;

        public string trans;

        public int musicSize;

        public int topicPerson;
    }

    /// <summary>
    /// 专辑信息
    /// </summary>
    public struct AlbumInfo
    {
        public string name;

        public long id;

        public string type;

        public int size;

        public long picId;

        public string blurPicUrl;

        public string companyId;

        public long pic;

        public string picUrl;

        public long publishTime;

        public string description;

        public string tags;

        public string company;

        public string briefDesc;

        public ArtistsInfo artist;

        public string[] songs;

        public AliasInfo alias;

        public int status;

        public int copyrightId;

        public string commentThreadId;

        public ArtistsInfo[] artists;

        public string subType;

        public string transName;

        public bool onSale;

        public int mark;

        public int gapless;

        public int dolbyMark;

        public long picId_str;

    }

    /// <summary>
    /// 音乐信息
    /// </summary>
    public struct MusicInfo
    {
        public string name;

        public string id;

        public int size;

        public string extension;

        public long sr;

        public int dfsId;

        public long bitrate;

        public long playTime;

        public long volumeDelta;
    }
}

/// <summary>
/// 网易云音乐解析
/// </summary>
public class CloudMusicAnalysin
{
    /// <summary>
    /// 搜索到歌曲的数据
    /// </summary>
    /// <param name="data"></param>
    /// <param name="totalCount">每次拉取歌曲的数量</param>
    /// <returns></returns>
    public SearchSongsDataInfo.SongsData AnalysinSongsData( string data , int totalCount = 1 )
    {
        SearchSongsDataInfo.SongsData songs = new SearchSongsDataInfo.SongsData( );
        JSONNode json = JSON.Parse( data );
        //取得歌曲总数量
        songs.SongCount = json["result"]["songCount"];
        songs.Songs = new List<SearchSongsDataInfo.SongsInfo>( );
        //整体的歌曲数据
        SearchSongsDataInfo.SongsInfo temp = new SearchSongsDataInfo.SongsInfo( );

        if( totalCount > songs.SongCount )
        {
            throw new System.Exception( "请求的歌曲数量大于总数量" );
        }

        //获取全部歌曲数据
        for( int i = 0 ; i < totalCount ; i++ )
        {
            var jsonData = json["result"]["songs"][i];
            //取得歌手json
            var artistsData = jsonData["artists"][0];
            //取得专辑json
            var album = jsonData["album"];

            //取得歌曲ID，播放歌曲时需要用到
            temp.ID = jsonData["id"];
            //取得歌曲名
            temp.SongName = jsonData["name"];
            //获取歌手信息
            temp.Artists = GetArtistsInfo( artistsData );
            //获取专辑信息
            temp.Album = GetAlbumInfos( album );
            //取得歌曲总时长
            temp.Duration = jsonData["duration"];
            //版权标识
            temp.CopyrightId = jsonData["copyrightId"];
            temp.Status = jsonData["status"];

            //一般情况下，这个值为空
            temp.Alias = null;

            temp.RType = jsonData["rtype"];
            temp.Ftype = jsonData["ftype"];
            temp.MVID = jsonData["mvid"];
            temp.fee = jsonData["fee"];
            temp.RUrl = jsonData["rUrl"];
            temp.Mark = jsonData["mark"];
            songs.Songs.Add( temp );
        }
        return songs;
    }

    /// <summary>
    /// 播放歌曲的数据
    /// </summary>
    /// <param name="data"></param>
    /// <returns></returns>
    public PlaySongsInfo.SongsData AnalysinPlaySongData( string data )
    {
        PlaySongsInfo.SongsData songs = new PlaySongsInfo.SongsData( );
        JSONNode json = JSON.Parse( data );
        var jsonData = json["songs"][0];
        songs.name = jsonData["name"];
        songs.id = jsonData["id"];
        songs.position = jsonData["position"];
        songs.alias = new AliasInfo( );
        songs.status = jsonData["status"];
        songs.fee = jsonData["fee"];
        songs.copyrightId = jsonData["copyrightId"];
        songs.disc = jsonData["disc"];
        songs.no = jsonData["no"];
        songs.artists = GetArtistsData( jsonData );
        songs.album = GetAlbumData( jsonData["album"] );
        songs.starred = jsonData["starred"];
        songs.popularity = jsonData["popularity"];
        songs.score = jsonData["score"];
        songs.starredNum = jsonData["starredNum"];
        songs.duration = jsonData["duration"];
        songs.playedNum = jsonData["playedNum"];
        songs.dayPlays = jsonData["dayPlays"];
        songs.hearTime = jsonData["hearTime"];
        songs.sqMusic = GetMusicInfo( jsonData["sqMusic"] );
        songs.hrMusic = jsonData["hrMusic"];
        songs.ringtone = jsonData["ringtone"];
        songs.crbt = jsonData["crbt"];
        songs.audition = jsonData["audition"];
        songs.copyFrom = jsonData["copyFrom"];
        songs.commentThreadId = jsonData["commentThreadId"];
        songs.rtUrl = jsonData["rtUrl"];
        songs.ftype = jsonData["ftype"];
        songs.rtUrls = null;
        songs.copyright = jsonData["copyright"];
        songs.transName = jsonData["transName"];
        songs.sign = jsonData["transName"];
        songs.mark = jsonData["mark"];
        songs.originCoverType = jsonData["originCoverType"];
        songs.originSongSimpleData = jsonData["originSongSimpleData"];
        songs.single = jsonData["single"];
        songs.noCopyrightRcmd = jsonData["noCopyrightRcmd"];
        songs.rtype = jsonData["rtype"];
        songs.rurl = jsonData["rurl"];
        songs.bMusic = GetMusicInfo( jsonData["bMusic"] );
        songs.mp3Url = jsonData["mp3Url"];
        songs.mvid = jsonData["mvid"];
        songs.hMusic = GetMusicInfo( jsonData["hMusic"] );
        songs.mMusic = GetMusicInfo( jsonData["mMusic"] );
        songs.lMusic = GetMusicInfo( jsonData["lMusic"] );
        return songs;
    }

    /// <summary>
    /// 获取歌手信息
    /// </summary>
    /// <param name="data"></param>
    /// <returns></returns>
    private SearchSongsDataInfo.ArtistsInfo GetArtistsInfo( JSONNode data )
    {
        SearchSongsDataInfo.ArtistsInfo temp = new SearchSongsDataInfo.ArtistsInfo( );
        temp.ID = data["id"];
        temp.Name = data["name"];
        temp.Alias = null;
        temp.AlbumSize = data["albumSize"];
        temp.PicId = data["picId"];
        temp.FansGroup = data["fansGroup"];
        temp.img1v1Url = data["img1v1Url"];
        temp.img1v1 = data["img1v1"];
        temp.trans = data["trans"];
        return temp;
    }

    /// <summary>
    /// 获取专辑数据
    /// </summary>
    /// <param name="data"></param>
    /// <returns></returns>
    private SearchSongsDataInfo.AlbumInfo GetAlbumInfos( JSONNode data )
    {
        SearchSongsDataInfo.AlbumInfo temp = new SearchSongsDataInfo.AlbumInfo( );
        temp.ID = data["id"];
        temp.Name = data["name"];
        var json = data["artist"];
        temp.Artist.ID = json["id"];
        temp.Artist.Name = json["name"];
        temp.Artist.PicUrl = json["picUrl"];
        temp.Artist.Alias = null;
        temp.Artist.AlbumSize = json["albumSize"];
        temp.Artist.PicId = json["picId"];
        temp.Artist.FansGroup = json["fansGroup"];
        temp.Artist.img1v1Url = json["img1v1Url"];
        temp.Artist.img1v1 = json["img1v1"];
        temp.Artist.trans = json["trans"];
        //发布时间
        temp.publishTime = data["publishTime"];
        temp.Size = data["size"];
        temp.CopyrightId = data["copyrightId"];
        temp.status = data["status"];
        temp.picId = data["picId"];
        temp.mark = data["mark"];
        return temp;
    }

    private PlaySongsInfo.MusicInfo GetMusicInfo( JSONNode data )
    {
        PlaySongsInfo.MusicInfo temp = new PlaySongsInfo.MusicInfo
        {
            name = data["name"] ,
            id = data["id"] ,
            size = data["size"] ,
            extension = data["extension"] ,
            sr = data["sr"] ,
            dfsId = data["dfsId"] ,
            bitrate = data["bitrate"] ,
            playTime = data["playTime"] ,
            volumeDelta = data["volumeDelta"]
        };

        return temp;
    }

    private PlaySongsInfo.ArtistsInfo[] GetArtistsData( JSONNode data )
    {
        PlaySongsInfo.ArtistsInfo[] temp = new PlaySongsInfo.ArtistsInfo[data["artists"].Count];
        for( int i = 0 ; i < temp.Length ; i++ )
        {
            var json = data["artists"][i];

            temp[i] = GetArtistsPlayData( json );
        }

        return temp;
    }

    private PlaySongsInfo.ArtistsInfo GetArtistsPlayData( JSONNode json )
    {
        PlaySongsInfo.ArtistsInfo temp = new PlaySongsInfo.ArtistsInfo( )
        {
            name = json["name"] ,
            id = json["id"] ,
            picId = json["picId"] ,
            img1v1Id = json["img1v1Id"] ,
            briefDesc = json["briefDesc"] ,
            picUrl = json["picUrl"] ,
            img1v1Url = json["img1v1Url"] ,
            albumSize = json["albumSize"] ,
            alias = new AliasInfo( ) ,
            trans = json["trans"] ,
            musicSize = json["musicSize"] ,
            topicPerson = json["topicPerson"] ,
        };
        return temp;
    }

    private PlaySongsInfo.AlbumInfo GetAlbumData( JSONNode data )
    {
        PlaySongsInfo.AlbumInfo temp = new PlaySongsInfo.AlbumInfo( )
        {
            name = data["name"] ,
            id = data["id"] ,
            type = data["type"] ,
            size = data["size"] ,
            picId = data["picId"] ,
            blurPicUrl = data["blurPicUrl"] ,
            companyId = data["companyId"] ,
            pic = data["pic"] ,
            picUrl = data["picUrl"] ,
            publishTime = data["publishTime"] ,
            description = data["description"] ,
            tags = data["tags"] ,
            company = data["company"] ,
            briefDesc = data["briefDesc"] ,
            artist = GetArtistsPlayData( data ) ,
            songs = null ,
            alias = new AliasInfo( ) ,
            status = data["status"] ,
            copyrightId = data["copyrightId"] ,
            commentThreadId = data["commentThreadId"] ,
            artists = GetArtistsData( data ) ,
            subType = data["subType"] ,
            transName = data["transName"] ,
            onSale = data["onSale"] ,
            mark = data["mark"] ,
            gapless = data["gapless"] ,
            dolbyMark = data["dolbyMark"] ,
            picId_str = data["picId_str"] ,
        };

        return temp;
    }
}

/// <summary>
/// 网易云音乐api
/// </summary>
public class CloudMusicAPI
{
    private const string URL = "http://music.163.com/api/";

    /// <summary>
    /// 获取歌曲信息URL
    /// </summary>
    /// <param name="songsName">歌曲名</param>
    /// <param name="total">请求的总数量(不代表总歌曲的数量,仅仅代表当前请求会请求多少个歌曲)</param>
    /// <returns>url</returns>
    public static string GetSongsInfo( string songsName , int total = 10 )
    {
        return URL + $"search/get/web?csrf_token=&type=1&offset=0&total=true&limit={total}&s={songsName}";
    }

    /// <summary>
    /// 获取播放mp3的url
    /// </summary>
    /// <param name="data"></param>
    /// <returns></returns>
    public static string GetRequestMP3URL( PlaySongsInfo.SongsData data )
    {
        if( data.copyrightId > 1 )
        {
            Debug.LogError( "无法播放VIP歌曲" );
            return null;
        }
        Debug.Log( $"当前的id为{data.id}\n歌手为{data.artists[0].name}" );
        return $"http://music.163.com/song/media/outer/url?id={data.id}.mp3";
    }

    /// <summary>
    /// 发送验证码
    /// </summary>
    /// <returns></returns>
    public static string SendVerificationCode( string phone )
    {
        return "http://netease-cloud-music-api-beta-lyart.vercel.app/captcha/sent?phone=" + phone;
        //return "captcha/sent?phone=" + phone;
    }

    /// <summary>
    /// 验证码登录
    /// </summary>
    /// <param name="phone">手机号</param>
    /// <returns></returns>
    public static string VerificationCodeLogin( string phone , string code )
    {
        return $"http://netease-cloud-music-api-beta-lyart.vercel.app/captcha/verify?phone={phone}&captcha={code}";
    }
}


public class CloudMusicPlayerAPI
{
    /// <summary>
    /// 音质
    /// </summary>
    public enum EToneQuality
    {
        /// <summary>
        /// 标准
        /// </summary>
        standard,
        /// <summary>
        /// 较高
        /// </summary>
        higher,
        /// <summary>
        /// 极高
        /// </summary>
        exhigh,
        /// <summary>
        /// 无损
        /// </summary>
        lossless,
        /// <summary>
        /// Hi-Res
        /// </summary>
        hires,
    }

    public static string GetPlayMusicUrl( int id )
    {
        return $"http://music.163.com/song/media/outer/url?id={id}.mp3";
    }
}