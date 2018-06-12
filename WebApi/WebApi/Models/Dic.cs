using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace WebApi.Models
{
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
    /// 病害类型类
    /// </summary>
    public class DiseaseTypes
    {
        /// <summary>
        /// 病害类型主键
        /// </summary>
        public int id { get; set; }
        /// <summary>
        /// 病害类型名称
        /// </summary>
        public string disease_type { get; set; }
    }
    /// <summary>
    /// 长势类型类
    /// </summary>
    public class GrowthTypes
    {
        /// <summary>
        /// 长势类型主键
        /// </summary>
        public int id { get; set; }
        /// <summary>
        /// 长势类型名称
        /// </summary>
        public string growth_type { get; set; }
    }
    /// <summary>
    /// 土壤湿度类型类
    /// </summary>
    public class MoistureTypes
    {
        /// <summary>
        /// 土壤湿度类型主键
        /// </summary>
        public int id { get; set; }
        /// <summary>
        /// 土壤湿度类型名称
        /// </summary>
        public string moisture_type { get; set; }
    }
    /// <summary>
    /// 资讯类型类
    /// </summary>
    public class NewsTypes
    {
        /// <summary>
        /// 资讯类型主键
        /// </summary>
        public int id { get; set; }
        /// <summary>
        /// 资讯类型名称
        /// </summary>
        public string news_type { get; set; }
    }
    /// <summary>
    /// 虫害类型类
    /// </summary>
    public class PestTypes
    {
        /// <summary>
        /// 虫害类型主键
        /// </summary>
        public int id { get; set; }
        /// <summary>
        /// 虫害类型名称
        /// </summary>
        public string pest_type { get; set; }
    }
     /// <summary>
    /// 产品等级字典类
    /// </summary>
    public class RsiGrade
    {
        /// <summary>
        /// 产品等级主键
        /// </summary>
        public int id { get; set; }
        /// <summary>
        /// 产品等级
        /// </summary>
        public int grade { get; set; }
        /// <summary>
        /// 农情类型
        /// </summary>
        public int rsi_type { get; set; }
        /// <summary>
        /// 产品等级名称
        /// </summary>
        public string name { get; set; }
    }
    /// <summary>
    /// 农情类型字典类
    /// </summary>
    public class RsiTypes
    {
        /// <summary>
        /// 农情类型主键
        /// </summary>
        public int id { get; set; }
        /// <summary>
        /// 农情类型名称
        /// </summary>
        public string rsi_type { get; set; }
        
    }
    
}