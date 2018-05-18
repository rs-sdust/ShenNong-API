using SunGolden.DBUtils;
using System;
using System.Collections.Generic;
using System.Data.Common;
using System.Linq;
using System.Web;
using System.Web.Http;

namespace WebApi.Models
{
    /// <summary>
    /// 地块类
    /// </summary>
    public class Field
    {
        /// <summary>
        /// 地块主键
        /// </summary>
        public int id { get; set; }
        /// <summary>
        /// 农场编号
        /// </summary>
        public int farm { get; set; }
        /// <summary>
        /// 地块名称
        /// </summary>
        public string name { get; set; }
        /// <summary>
        /// 地块边界
        /// </summary>
        public string geom { get; set; }
        /// <summary>
        /// 地块面积
        /// </summary>
        public float area { get; set; }
        /// <summary>
        /// 创建日期
        /// </summary>
        public DateTime createdate { get; set; }
        /// <summary>
        /// 当前作物
        /// </summary>
        public int currentcrop { get; set; }
        /// <summary>
        /// 播种时间
        /// </summary>
        public DateTime sowdate { get; set; }
        /// <summary>
        /// 物候期
        /// </summary>
        public int phenophase { get; set; }
        /// <summary>
        /// 地块缩略图
        /// </summary>
        public string thumb { get; set; }
    } 
    ///// <summary>
    ///// 地块列表类
    ///// </summary>
    //public class GetFields
    //{
    //    /// <summary>
    //    /// 
    //    /// </summary>
    //    public string name { get; set; }
    //    public float area { get; set; }
    //    public int currentcrop { get; set; }
    //    public string thumb { get; set; }
    //} 
    /// <summary>
    /// 作物类型类
    /// </summary>
    public class CropTypes
    {
        /// <summary>
        /// 作物类型主键
        /// </summary>
        public int id { get; set; }
        /// <summary>
        /// 作物类型名称
        /// </summary>
        public string crop_type { get; set; }
    }
    /// <summary>
    /// 物候期类
    /// </summary>
    public class Phenophase
    {
        /// <summary>
        /// 物候期主键
        /// </summary>
        public int id { get; set; }
        /// <summary>
        /// 作物类型编号
        /// </summary>
        public int crop_type { get; set; }
        /// <summary>
        /// 物候类型编号
        /// </summary>
        public int phen_type { get; set; }
        /// <summary>
        /// 物候日期
        /// </summary>
        public int time { get; set; }
        /// <summary>
        /// 物候详情
        /// </summary>
        public string phen_detail { get; set; }

    }
}