using SimpleJSON;
using System;
using UnityEngine;

namespace CloudMusic.API
{
    /// <summary>
    /// 网易云音乐json数据
    /// </summary>
    public class CloudMusicRequetJsonData
    {
        /// <summary>
        /// 登录请求
        /// </summary>
        public class CloudMusicLoginRequet
        {
            /// <summary>
            /// 获取生成二维码的主键
            /// </summary>
            /// <param name="json">生成二维码返回的json</param>
            /// <returns>unikey</returns>
            public static string GenerateQrCodeKey( string json )
            {
                return JSON.Parse( json )["data"]["unikey"];
            }

            /// <summary>
            /// 获取二维码的信息
            /// </summary>
            /// <param name="json">请求二维码返回的json</param>
            /// <returns>二维码的信息</returns>
            public static string GenerateQrCodeInfo( string json )
            {
                string data = JSON.Parse( json )["data"]["qrimg"];
                return data.Replace( "data:image/png;base64," , "" );
            }

            /// <summary>
            /// 获取二维码
            /// </summary>
            /// <param name="json">请求二维码返回的json</param>
            /// <param name="widthOrHeight">二维码的宽高</param>
            /// <returns>二维码</returns>
            public static Texture2D GenerateQrCode( string json , int widthOrHeight )
            {
                string data = JSON.Parse( json )["data"]["qrimg"];
                byte[] bytes = Convert.FromBase64String( data.Replace( "data:image/png;base64," , "" ) );
                Texture2D texture2D = new Texture2D( widthOrHeight , widthOrHeight );
                texture2D.LoadImage( bytes );
                return texture2D;
            }
        }
    }
}