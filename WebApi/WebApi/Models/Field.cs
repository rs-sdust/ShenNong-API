using SunGolden.DBUtils;
using System;
using System.Collections.Generic;
using System.Data.Common;
using System.Linq;
using System.Web;
using System.Web.Http;

namespace WebApi.Models
{
    public class Field
    {
        public int id { get; set; }
        public int farm { get; set; }
        public string name { get; set; }
        public string geom { get; set; }
        public float area { get; set; }
        public DateTime createdate { get; set; }
        public int currentcrop { get; set; }
        public DateTime sowdate { get; set; }
        public int phenophase { get; set; }
    }  
    public class CropTypes
    {
        public int id { get; set; }
        public string crop_type { get; set; }
    }

    //public class Field_plan
    //{
    //    public int id { get; set; }
    //    public int field { get; set; }
    //    public int type { get; set; } 
    //    public string name { get; set; }
    //    public string geom { get; set; }
    //    public string currentcrop { get; set; }
    //    public string farmwork_type { get; set; }
    //    public string operater { get; set; }
    //    public string intput_product { get; set; }
    //    public string actusldate { get; set; }

    //}
}