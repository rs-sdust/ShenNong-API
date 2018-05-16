using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace WebApi.Models
{
    public class Farm
    {
        public int id { get; set; }
        public string name { get; set; }
        public string address { get; set; }
        //public string thumb { get; set; }
    }
    public class Farmwork
    {
        public int field { get; set; }
        public int type { get; set; }
        public int creator { get; set; } 
        public int operater { get; set; } 
        public string material { get; set; }
        public int labor_num { get; set; }
        public float labor_cost { get; set; }
        public int machine { get; set; } 
        public DateTime createdate { get; set; }
        public DateTime  plandate { get; set; }
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
    public class Market
    {
        public int id { get; set; }
        public int crop_type { get; set; }
        public float crop_price { get; set; }
        public DateTime time { get; set; }
    }
    public class RSI_type
    {
        public int id { get; set; }
        public string  rsi_type { get; set; }
    }
    public class Field_rsi
    {
        public int id { get; set; }
        public int farm { get; set; }
        public int field { get; set; }
        public int type { get; set; } 
        public DateTime date { get; set; }
        public int grade { get; set; } 
        public float value { get; set; }
    }
    public class Field_live
    {
        public int id { get; set; }
        public int field { get; set; }
        public int growth { get; set; } 
        public int moisture { get; set; }
        public string disease { get; set; }
        public string pest { get; set; }
        public int collector { get; set; }
        public DateTime collect_date { get; set; }
        public string gps { get; set; }
        public string picture { get; set; }
    }
}