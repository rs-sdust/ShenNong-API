using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Web;

namespace WebApi.Models
{
    public class User
    {
        public int id { get; set; }
        public string name { get; set; }
        public string mobile { get; set; }
        //public string password { get; set; }
        public string im_pwd { get; set; }
        public int role { get; set; }
        public int farm { get; set; }
        public string icon { get; set; }
        //public DateTime tokendate { get; set; }

    }
   
    public class UserResponse<T>
    {
        public string msg { get; set; }
        public T data { get; set; }
    }
    public class Userinfo
    {
        public string userid { get; set; }
        public string password { get; set; }
        public string nick { get; set; }
        public string mobile { get; set;}
        public string email { get; set; }
        public string icon_url { get; set; }
        
    }
}