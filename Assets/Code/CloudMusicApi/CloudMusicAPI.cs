namespace CloudMusic.API
{
    public class CloudMusicAPI
    {
        /// <summary>请求的服务器地址</summary>
        public static string RequestUrl { get { return "http://43.138.56.175:3000"; } }

        public static string Cookie { get; set; } = string.Empty;


        /// <summary>
        /// 用户请求
        /// </summary>
        public class CloudMusicUserRequest
        {
            /// <summary>
            /// 二维码key生成
            /// <para>调用此接口可生成一个 key</para>
            /// </summary>
            public static string LoginQRkeyRequest( )
            {
                return $"login/qr/key/timestamp={TimerExtend.GetTimeStamp( )}";
            }

/// <summary>
            /// 二维码生成接口
            /// <para>调用此接口传入上一个接口生成的 key 可生成二维码图片的 base64 和二维码信息,可使用 base64 展示图片,或者使用二维码信息内容自行使用第三方二维码生成库渲染二维码</para>
            /// </summary>
            public static string LoginQrCreateRequest( string key )
            {
                return $"login/qr/create?key={key}&qrimg=true&timestamp={TimerExtend.GetTimeStamp( )}";
            }
            /// <summary>
            /// 二维码检查接口
            /// <para>轮询此接口可获取二维码扫码状态,800 为二维码过期,801 为等待扫码,802 为待确认,803 为授权登录成功(803 状态码下会返回 cookies)</para>
            /// </summary>
            public static string LoginQrCheckRequest( string key )
            {
                return $"login/qr/check?key={key}&timestamp={TimerExtend.GetTimeStamp( )}";
            }

            /// <summary>
            /// 退出登录
            /// <para>调用此接口,可退出登录</para>
            /// </summary>
            public static string LogOut( )
            {
                return "logout";
            }

            /// <summary>
            /// 登录状态
            /// <para>调用此接口,可获取登录状态</para>
            /// </summary>
            public static string LoginStatus( )
            {
                return "login/status";
            }

            /// <summary>
            /// 获取用户详情
            /// <para>登录后调用此接口 , 传入用户 id, 可以获取用户详情</para>
            /// </summary>
            /// <param name="id">用户 id</param>
            /// <returns></returns>
            public static string Userinfo( int id )
            {
                return $"user/detail?uid={id}";
            }

            /// <summary>
            /// 获取账号信息
            /// <para>登录后调用此接口 ,可获取用户账号信息</para>
            /// </summary>
            public static string AccountInformation( )
            {
                return "user/account";
            }

            /// <summary>
            /// 获取用户信息 , 歌单，收藏，mv, dj 数量
            /// <para>登录后调用此接口,可以获取用户信息</para>
            /// </summary>
            /// <returns></returns>
            public static string Subcount( )
            {
                return "user/subcount";
            }

            /// <summary>
            /// 获取用户等级信息
            /// <para>说明 : 登录后调用此接口,可以获取用户等级信息,包含当前登录天数,听歌次数,下一等级需要的登录天数和听歌次数,当前等级进度</para>
            /// </summary>
            /// <returns></returns>
            public static string UserLevel( )
            {
                return "user/level";
            }
        }
    }
}