using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace CloudMusic
{
    /// <summary>
    /// api配置列表
    /// </summary>
    public static class CloudMusicApiConfig
    {
        /// <summary>
        /// 搜索
        /// </summary>
        public class Search :CloudMusicRequestData
        {
            public override string Url => "http://music.163.com/api/search/get";

            public override string Method { get; set; } = "POST";
        }
    }
}