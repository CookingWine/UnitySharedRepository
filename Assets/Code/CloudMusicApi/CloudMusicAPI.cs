namespace CloudMusic.API
{
    public class CloudMusicAPI
    {
        /// <summary>请求的服务器地址</summary>
        public static string RequestUrl { get { return "http://findwind.cn:3000"; } }

        public static string Cookie { get; set; } = string.Empty;


        /// <summary>
        /// 请求接口
        /// </summary>
        public class CloudMusicWebRequest
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

            /// <summary>
            /// 获取用户绑定信息
            /// </summary>
            /// <param name="id">用户id</param>
            /// <returns>登录后调用此接口 , 可以获取用户绑定信息</returns>
            public static string Binding( string id )
            {
                return "user/binding?uid=" + id;
            }

            /// <summary>
            /// 调用此接口 ,传入手机号码, 可发送验证码
            /// </summary>
            /// <param name="phone">手机号码</param>
            /// <returns></returns>
            public static string SendVerificationCode( string phone )
            {
                return "captcha/sent?phone=" + phone;
            }

            /// <summary>
            /// 调用此接口 ,传入手机号码和验证码, 可校验验证码是否正确
            /// </summary>
            /// <param name="phone">手机号码</param>
            /// <param name="code">验证码</param>
            /// <returns></returns>
            public static string VerificationVerificationCode( string phone , string code )
            {
                return $"/captcha/verify?phone={phone}&captcha={code}";
            }

            /// <summary>
            /// 私人 FM( 需要登录 )
            /// </summary>
            /// <returns></returns>
            public static string PersonslFM( )
            {
                return "personal_fm";
            }

            /// <summary>
            /// 获取每日推荐歌单
            /// </summary>
            /// <returns></returns>
            public static string Resource( )
            {
                return "recommend/resource";
            }

            /// <summary>
            /// 获取推荐MV
            /// </summary>
            /// <returns>调用此接口 , 可获取推荐 mv</returns>
            public static string PersonalizedMV( )
            {
                return "personalized/mv";
            }
            /// <summary>
            /// mv 地址
            /// </summary>
            /// <param name="id">mv id</param>
            /// <returns>调用此接口 , 传入 mv id,可获取 mv 播放地址</returns>
            public static string DetailMVData( string id )
            {
                return $"mv/url?id={id}&r=1920";
            }


        }
    }
}