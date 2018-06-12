using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace WebApi.Models
{
    /// <summary>
    /// 新闻资讯类型
    /// </summary>
    public class News
    {
        /// <summary>
        /// 主键
        /// </summary>
        public int id { get; set; }
        /// <summary>
        /// 资讯类型
        /// </summary>
        public int news_type { get; set; }
        /// <summary>
        /// 资讯简述
        /// </summary>
        public string summary { get; set; }
        /// <summary>
        /// 资讯日期
        /// </summary>
        public DateTime news_date { get; set; }
        /// <summary>
        /// 资讯链接
        /// </summary>
        public string url { get; set; }
        /// <summary>
        /// 缩略图
        /// </summary>
        public string thumb { get; set; }
    }
}