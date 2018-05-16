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
        public string thumb { get; set; }
    } 
    public class GetFields
    {
        public string name { get; set; }
        public float area { get; set; }
        public int currentcrop { get; set; }
        public string thumb { get; set; }
    } 
    public class CropTypes
    {
        public int id { get; set; }
        public string crop_type { get; set; }
    }

    public class Phenophase
    {
        public int id { get; set; }
        public int crop_type { get; set; }
        public int phen_type { get; set; }
        public int time { get; set; }
        public string phen_detail { get; set; }

    }
}