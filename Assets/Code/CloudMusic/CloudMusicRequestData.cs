using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace CloudMusic
{
    /// <summary>
    /// 请求参数
    /// </summary>
    public abstract class CloudMusicRequestData
    {
        /// <summary>
        /// 接口地址
        /// </summary>
        public abstract string Url { get; }

        /// <summary>
        /// 请求方式
        /// </summary>
        public virtual string Method { get; set; } = "GET";

        /// <summary>
        /// 请求参数
        /// </summary>
        public object FormData { get; set; }
    }
}