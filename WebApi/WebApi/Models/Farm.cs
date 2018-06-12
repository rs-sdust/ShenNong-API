using Newtonsoft.Json.Linq;
using System;


namespace WebApi.Models
{
    /// <summary>
    /// 农场类
    /// </summary>
    public class Farm
    {
        /// <summary>
        /// 农场主键
        /// </summary>
        public int id { get; set; }
        /// <summary>
        /// 农场名称
        /// </summary>
        public string name { get; set; }
        /// <summary>
        /// 农场地址
        /// </summary>
        public string address { get; set; }
        //public string thumb { get; set; }
    }
    /// <summary>
    /// 农情信息类
    /// </summary>
    public class Farmwork
    {
        /// <summary>
        /// 地块编号
        /// </summary>
        public int field { get; set; }
        /// <summary>
        /// 农情类型编号
        /// </summary>
        public int type { get; set; }
        /// <summary>
        /// 创建人
        /// </summary>
        public int creator { get; set; } 
        /// <summary>
        /// 操作人
        /// </summary>
        public int operater { get; set; }
        /// <summary>
        /// 投入品（类型，数量）
        /// </summary>
        public string material { get; set; }
        /// <summary>
        /// 投入人工
        /// </summary>
        public int labor_num { get; set; }
        /// <summary>
        /// 人工费用
        /// </summary>
        public float labor_cost { get; set; }
        /// <summary>
        /// 设备（类型，数量）
        /// </summary>
        public int machine { get; set; }
        /// <summary>
        /// 创建日期
        /// </summary>
        public DateTime createdate { get; set; }
        /// <summary>
        /// 计划完成日期
        /// </summary>
        public DateTime  plandate { get; set; }
        /// <summary>
        /// 完成日期
        /// </summary>
        public DateTime  actualdate { get; set; }
    }
    //public class Storage
    //{
    //    public int id { get; set; }
    //    public int farm { get; set; }
    //    public int crop_type { get; set; }
    //    public float storage { get; set; }
    //}
    //public class Storage_flow
    //{
    //    public int id { get; set; }
    //    public int farm { get; set; }
    //    public int direction { get; set; }
    //    public int crop_type { get; set; }
    //    public float amount { get; set; }
    //    public float unit_price { get; set; }
    //    public DateTime time { get; set; }
    //}
    /// <summary>
    /// 市场行情类
    /// </summary>
    public class Market
    {
        /// <summary>
        /// 市场行情主键
        /// </summary>
        public int id { get; set; }
        /// <summary>
        /// 作物类型编号
        /// </summary>
        public int crop_type { get; set; }
        /// <summary>
        /// 作物价格
        /// </summary>
        public float crop_price { get; set; }
        /// <summary>
        /// 日期
        /// </summary>
        public DateTime time { get; set; }
    }
   
    /// <summary>
    /// 遥感农情信息类
    /// </summary>
    public class Field_rsi
    {
        /// <summary>
        /// 农情主键
        /// </summary>
        public int id { get; set; }
        /// <summary>
        /// 农场编号
        /// </summary>
        public int farm { get; set; }
        /// <summary>
        /// 地块编号
        /// </summary>
        public int field { get; set; }
        /// <summary>
        /// 农情类型编号
        /// </summary>
        public int type { get; set; }
        /// <summary>
        /// 日期
        /// </summary>
        public DateTime date { get; set; }
        /// <summary>
        /// 产品等级
        /// </summary>
        public int grade { get; set; }
        /// <summary>
        /// 产品数值
        /// </summary>
        public float value { get; set; }
    }
    /// <summary>
    /// 地块实况信息类
    /// </summary>
    public class Field_live
    {
        /// <summary>
        /// 地块实况主键
        /// </summary>
        public int id { get; set; }
        /// <summary>
        /// 地块编号
        /// </summary>
        public int field { get; set; }
        /// <summary>
        /// 作物长势编号
        /// </summary>
        public int growth { get; set; } 
        /// <summary>
        /// 土壤湿度编号
        /// </summary>
        public int moisture { get; set; }
        /// <summary>
        /// 病害类型
        /// </summary>
        public int disease { get; set; }
        /// <summary>
        /// 虫害类型
        /// </summary>
        public int pest { get; set; }
        /// <summary>
        /// 采集人
        /// </summary>
        public int collector { get; set; }
        /// <summary>
        /// 采集时间
        /// </summary>
        public DateTime collect_date { get; set; }
        /// <summary>
        /// 地理位置
        /// </summary>
        public string gps { get; set; }
        /// <summary>
        /// 图片链接
        /// </summary>
        public string picture { get; set; }
    }
  
}