namespace CloudMusic.API
{
    public class CloudMusicAPI
    {
        /// <summary>请求的服务器地址</summary>
        public static string RequestUrl { get { return "http://43.138.56.175:3000"; } }

        /// <summary>
        /// 二维码key生成
        /// </summary>
        [HttpRequestBody( "login/qr/key/" )]
        public class CloudLoginQrKeyRequest :HttpRequestProvider
        {
            public override string GetUrl( )
            {
                return $"timestamp={TimerExtend.GetTimeStamp( )}";
            }
        }

        /// <summary>
        /// 二维码生成接口
        /// </summary>
        [HttpRequestBody( "login/qr/create/" )]
        public class CloudLoginQrCreateRequest :HttpRequestProvider
        {
            public string Key { get; set; } = string.Empty;
            public override string GetUrl( )
            {
                return $"key={Key}&qrimg=true&timestamp={TimerExtend.GetTimeStamp( )}";
            }
        }

        /// <summary>
        /// 二维码检查接口
        /// </summary>
        [HttpRequestBody( "login/qr/check/" )]
        public class CloudLoginQrCheckRequest :HttpRequestProvider
        {
            public string Key { get; set; } = string.Empty;
            public override string GetUrl( )
            {
                return $"key={Key}&timestamp={TimerExtend.GetTimeStamp( )}";
            }
        }

        /// <summary>
        /// 二维码检查接口
        /// </summary>
        [HttpRequestBody( "login/status/" )]
        public class CloudLoginStatusRequest :HttpRequestProvider
        {
            public string Cookie { get; set; } = string.Empty;

        }

        /// <summary>
        /// 登录后获取用户信息
        /// </summary>
        [HttpRequestBody( "user/account" )]
        public class CloudUserInfoRequest :HttpRequestProvider
        {

        }

        [HttpRequestBody( "cloudsearch?" )]
        public class CloudSerachSongsRequest :HttpRequestProvider
        {
            public int Type { get; set; } = 1;
            public int Offect { get; set; } = 0;

            public int Limit { get; set; } = 30;

            public string SongsName { get; set; } = string.Empty;

            public override string GetUrl( )
            {
                return $"keywords={SongsName}";
            }

        }

    }
}