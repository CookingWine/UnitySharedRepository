using SimpleJSON;
using System.Collections.Generic;

namespace CloudMusic.API
{
    /// <summary>
    /// 别名信息
    /// </summary>
    public struct AliasInfo
    {

    }

    /// <summary>
    /// 搜索歌曲的数据
    /// </summary>
    public class SearchSongsData
    {
        /// <summary>
        /// 获取搜索歌曲的url
        /// </summary>
        /// <param name="songsName">歌曲名</param>
        /// <param name="count">请求个数</param>
        /// <param name="offset">从第几个开始搜索</param>
        /// <returns></returns>
        public static string GetSearchSongsUrl( string songsName , int count , int offset = 0 )
        {
            return $"http://music.163.com/api/search/get/web?csrf_token=&type=1&offset={offset}&total=true&limit={count}&s={songsName}";
        }
        /// <summary>
        /// 歌曲数据
        /// </summary>
        public struct SongsData
        {
            /// <summary>
            /// 歌曲数据
            /// </summary>
            public List<SongsInfo> Songs { get; private set; }

            /// <summary>
            /// 歌曲总数量
            /// </summary>
            public int SongCount { get; private set; }

            /// <summary>
            /// 获取歌曲数据
            /// </summary>
            /// <param name="data">json</param>
            /// <param name="requrtCount">请求的个数</param>
            public void GetSongsData( string data , int requrtCount )
            {
                Songs = new List<SongsInfo>( );
                JSONNode json = JSON.Parse( data );
                SongCount = json["result"]["songCount"];
                for( int i = 0 ; i < requrtCount ; i++ )
                {
                    Songs.Add( new SongsInfo( ).GetSongsInfo( json["result"]["songs"][i] ) );
                }
            }
        }

        /// <summary>
        /// 歌曲信息
        /// </summary>
        public struct SongsInfo
        {
            /// <summary>
            /// 歌曲ID
            /// </summary>
            public int ID { get; private set; }
            /// <summary>
            /// 歌曲名
            /// </summary>
            public string SongName { get; private set; }

            /// <summary>
            /// 歌手信息
            /// </summary>
            public ArtistsInfo Artists { get; private set; }

            /// <summary>
            /// 专辑信息
            /// </summary>
            public AlbumInfo Album { get; private set; }

            /// <summary>
            /// 持续时间
            /// </summary>
            public long Duration { get; private set; }

            /// <summary>
            /// 版权标识
            /// </summary>
            public long CopyrightId { get; private set; }

            /// <summary>
            /// 状态
            /// </summary>
            public int Status { get; private set; }

            /// <summary>
            /// 别名信息(该值一般为空,不用管)
            /// </summary>
            public AliasInfo[] Alias { get; private set; }

            public long RType { get; private set; }

            public long Ftype { get; private set; }

            /// <summary>
            /// mv id
            /// </summary>
            public long MVID { get; private set; }

            /// <summary>
            /// 费用
            /// </summary>
            public long Fee { get; private set; }

            public string RUrl { get; private set; }

            /// <summary>
            /// 标记
            /// </summary>
            public long Mark { get; private set; }

            public SongsInfo GetSongsInfo( JSONNode data )
            {
                //取得歌手json
                var artistsData = data["artists"][0];
                //取得专辑json
                var album = data["album"];
                SongsInfo temp = new SongsInfo
                {
                    //取得歌曲ID，播放歌曲时需要用到
                    ID = data["id"] ,
                    //取得歌曲名
                    SongName = data["name"] ,
                    Duration = data["duration"] ,
                    //版权标识
                    CopyrightId = data["copyrightId"] ,
                    Status = data["status"] ,
                    //一般情况下，这个值为空
                    Alias = null ,
                    RType = data["rtype"] ,
                    Ftype = data["ftype"] ,
                    MVID = data["mvid"] ,
                    Fee = data["fee"] ,
                    RUrl = data["rUrl"] ,
                    Mark = data["mark"] ,
                    Artists = new ArtistsInfo( ) ,
                    Album = new AlbumInfo( ) ,
                };
                temp.Artists = temp.Artists.GetArtistsInfo( artistsData );
                temp.Album = temp.Album.GetAlbumInfo( album );
                return temp;
            }

            public void DebuLogInfo( )
            {
                UnityEngine.Debug.Log( $"歌曲ID->{ID}\n歌曲名为{SongName}\n歌手->{Artists.Name}" );
            }
        }

        /// <summary>
        /// 艺术家信息
        /// </summary>
        public struct ArtistsInfo
        {
            /// <summary>
            /// 歌手ID
            /// </summary>
            public int ID { get; private set; }

            /// <summary>
            /// 歌手名
            /// </summary>
            public string Name { get; private set; }

            public string PicUrl { get; private set; }

            /// <summary>
            /// 别名信息(该值一般为空,不用管)
            /// </summary>
            public AliasInfo[] Alias { get; private set; }

            /// <summary>
            /// 专辑数
            /// </summary>
            public int AlbumSize { get; private set; }

            public long PicId { get; private set; }

            /// <summary>
            /// 粉丝团
            /// </summary>
            public string FansGroup { get; private set; }

            public string Img1v1Url { get; private set; }

            public long Img1v1 { get; private set; }

            public string Trans { get; private set; }

            public ArtistsInfo GetArtistsInfo( JSONNode data )
            {
                ArtistsInfo temp = new ArtistsInfo
                {
                    ID = data["id"] ,
                    Name = data["name"] ,
                    Alias = null ,
                    AlbumSize = data["albumSize"] ,
                    PicId = data["picId"] ,
                    FansGroup = data["fansGroup"] ,
                    Img1v1Url = data["img1v1Url"] ,
                    Img1v1 = data["img1v1"] ,
                    Trans = data["trans"]
                };
                return temp;
            }
        }

        /// <summary>
        /// 专辑信息
        /// </summary>
        public struct AlbumInfo
        {
            /// <summary>
            /// 专辑ID
            /// </summary>
            public int ID { get; private set; }

            /// <summary>
            /// 专辑名
            /// </summary>
            public string Name { get; private set; }

            /// <summary>
            /// 歌手
            /// </summary>
            public ArtistsInfo Artist { get; private set; }

            /// <summary>
            /// 发布时间
            /// </summary>
            public long PublishTime { get; private set; }

            /// <summary>
            /// 规格
            /// </summary>
            public long Size { get; private set; }

            /// <summary>
            /// 版权标识
            /// </summary>
            public long CopyrightId { get; private set; }

            /// <summary>
            /// 状态
            /// </summary>
            public string Status { get; private set; }

            public long PicId { get; private set; }

            /// <summary>
            /// 标记
            /// </summary>
            public int Mark { get; private set; }

            public AlbumInfo GetAlbumInfo( JSONNode data )
            {
                AlbumInfo temp = new AlbumInfo
                {
                    ID = data["id"] ,
                    Name = data["name"] ,
                    //发布时间
                    PublishTime = data["publishTime"] ,
                    Size = data["size"] ,
                    CopyrightId = data["copyrightId"] ,
                    Status = data["status"] ,
                    PicId = data["picId"] ,
                    Mark = data["mark"] ,
                    Artist = new ArtistsInfo( )
                };
                temp.Artist = temp.Artist.GetArtistsInfo( data["artist"] );
                return temp;
            }
        }
    }
}