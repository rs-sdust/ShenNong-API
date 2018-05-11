using Newtonsoft.Json;
using SunGolden.DBUtils;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Common;
using System.Net.Http;
using System.Web;
using System.Web.Http;
using WebApi.Auth;
using WebApi.Models;

namespace WebApi.Controllers
{
    public class FieldsController : ApiController
    {
        //获取地块列表
        [HttpGet]
        [AuthFilterOutside]
        public object GetFields(int farm)
        { 
            //获取请求
            var request = HttpContext.Current.Request;
            ////查询数据库
            string str ="select * from tb_field where farm = @farm";
            PostgreSQL.OpenCon();
            var para = new DbParameter[1];
            para[0] = PostgreSQL.NewParameter("@farm", farm);
            //var qField1 = PostgreSQL.ExecuteTQuery<Field>(str, null, para);
            var qField = PostgreSQL.ExecuteTListQuery<Field>(str, null, para);
            PostgreSQL.CloseCon();
            //响应
            UserResponse<List<Field>> respons = new UserResponse< List < Field >> ();
            HttpResponseMessage response = new HttpResponseMessage();
            respons.msg = "0";
            respons.data = qField;
            string token = request.Headers["Token"];
            //添加响应内容
            var resultObj = JsonConvert.SerializeObject(respons, Formatting.Indented);
            response.Headers.Add("Token",token );
            response.Content = new StringContent(resultObj);
            return response;
        }
       
        //添加地块
        [HttpPost]
        [AuthFilterOutside]
        public object AddField(Field field)
        {
            string token = null;
            //获取请求
            var request = HttpContext.Current.Request;
            //声明响应
            UserResponse<Field> respons = new UserResponse<Field>();
            HttpResponseMessage response = new HttpResponseMessage();
            //查询数据库
            PostgreSQL.OpenCon();
            string str_select = "select * from tb_field where \"name\" = @name;";
            var para1 = new DbParameter[1];
            para1[0] = PostgreSQL.NewParameter("@name", field.name );   
            var qField = PostgreSQL.ExecuteTQuery<Field>(str_select, null, para1);
            PostgreSQL.CloseCon();
            //判断地块名是否存在
            if (qField != null)
            {
                respons.msg = "1";
                respons.data = qField;
                token = request.Headers["Token"];
            }
            else
            {
                PostgreSQL.OpenCon();
                string str_insert = "insert into tb_field(farm,\"name\",geom,area,createdate,currentcrop,sowdate,phenophase) values(@farm,@name,@geom,@area,@createdate,@currentcrop,@sowdate,@phenophase);";
                var trans = PostgreSQL.BeginTransaction();
                try
                {
                    var para = new DbParameter[8];
                    para[0] = PostgreSQL.NewParameter("@farm", field.farm);
                    para[1] = PostgreSQL.NewParameter("@name", field.name);
                    para[2] = PostgreSQL.NewParameter("@geom", field.geom);
                    para[3] = PostgreSQL.NewParameter("@area", field.area);
                    para[4] = PostgreSQL.NewParameter("@createdate", field.createdate);
                    para[5] = PostgreSQL.NewParameter("@currentcrop", field.currentcrop);
                    para[6] = PostgreSQL.NewParameter("@sowdate", field.sowdate);
                    para[7] = PostgreSQL.NewParameter("@phenophase", field.phenophase);
                    var num = PostgreSQL.ExecuteNoneQuery(str_insert, trans, para);
                    PostgreSQL.CommitTransaction(trans);
                }
                catch (Exception ex)
                {
                    PostgreSQL.RollbackTransaction(trans);
                }
                PostgreSQL.CloseCon();
            }
            PostgreSQL.CloseCon();
            //响应内容
            respons.msg = "0";
            respons.data = qField;
            token = request.Headers["Token"];
            //添加响应头
            var resultObj = JsonConvert.SerializeObject(respons, Formatting.Indented);
            response.Headers.Add("Token", token);
            response.Content = new StringContent(resultObj);

            return response;
        }
        //更新地块信息
        [HttpPost]
        [AuthFilterOutside]
        public object UpdateField(Field field)
        {
            string token = null;
            //获取请求
            var request = HttpContext.Current.Request;
            //声明响应
            UserResponse<Field> respons = new UserResponse<Field>();
            HttpResponseMessage response = new HttpResponseMessage();

            //查询数据库
            PostgreSQL.OpenCon();
            string str_select = "select * from tb_field where name = @name;";
            var para = new DbParameter[1];
            para[0] = PostgreSQL.NewParameter("@name", field.name);
            var qField = PostgreSQL.ExecuteTQuery<Field>(str_select, null, para);
            PostgreSQL.CloseCon();
            if (qField == null)
            {
                respons.msg = "1";
                respons.data = null;
                token = request.Headers["Token"];
            }
            else
            {
                PostgreSQL.OpenCon();
                string str_update = "update tb_field set \"name\" = @name, area= @area,sowdate = @sowdate ,currentcrop = @currentcrop where \"name\" = @name";
                var trans = PostgreSQL.BeginTransaction();
                try
                {
                    var para1 = new DbParameter[4];
                    para1[0] = PostgreSQL.NewParameter("@name", field.name);
                    para1[1] = PostgreSQL.NewParameter("@area", field.area);
                    para1[2] = PostgreSQL.NewParameter("@sowdate", field.sowdate);
                    para1[3] = PostgreSQL.NewParameter("@currentcrop", field.currentcrop);
                    var num = PostgreSQL.ExecuteNoneQuery(str_update, trans, para1);
                    PostgreSQL.CommitTransaction(trans);
                }
                catch (Exception ex)
                {
                    PostgreSQL.RollbackTransaction(trans);
                }
                PostgreSQL.CloseCon();
                //查询数据库
                PostgreSQL.OpenCon();
                string str = "select * from tb_field where name = @name;";
                var para2 = new DbParameter[1];
                para2[0] = PostgreSQL.NewParameter("@name", field.name);
                var qField1 = PostgreSQL.ExecuteTQuery<Field>(str, null, para2);
                PostgreSQL.CloseCon();
                //响应内容
                respons.msg = "0";
                respons.data = qField1;
                token = request.Headers["Token"];
               
                //添加响应头
                var resultObj = JsonConvert.SerializeObject(respons, Formatting.Indented);
                response.Headers.Add("Token", token);
                response.Content = new StringContent(resultObj);
            }
                return response;
        }
        //获取地块信息详情
        //[HttpGet]
        //public object GetFieldDetail( )
        //{

        //}

        //获取作物类型
        [HttpGet]
        [AuthFilterOutside]
        public object GetCrops()
        {
            string token = null;
            //获取请求
            var request = HttpContext.Current.Request;

            //查询数据库
            string str_select = "select * from dic_crop_type";
            PostgreSQL.OpenCon();
          
            var para = new DbParameter[0];
            var qCrop = PostgreSQL.ExecuteTListQuery<CropTypes>(str_select, null, para);
            PostgreSQL.CloseCon();
            //响应
            UserResponse<List<CropTypes>> respons = new UserResponse<List<CropTypes>>();
            HttpResponseMessage response = new HttpResponseMessage();
            respons.msg = "0";
            respons.data = qCrop;
            token = request.Headers["Token"];
            //添加响应头
            var resultObj = JsonConvert.SerializeObject(respons, Formatting.Indented);
            response.Headers.Add("Token", token);
            response.Content = new StringContent(resultObj);
            return response;
        } 
       //创建农场
        [HttpPost]
        [AuthFilterOutside]
        public object CreatFarm(Farm farm)
        {
            string token =null;
            //获取请求
            var request = HttpContext.Current.Request;
            token= request.Headers["Token"];
            var role =RoleType(token);

            //响应
            UserResponse<Farm> respons = new UserResponse<Farm>();
            HttpResponseMessage response = new HttpResponseMessage();
           
            //如果用户是农场主，则创建农场
            if (role== "1")//"农场主")
            {
                PostgreSQL.OpenCon();
                var para = new DbParameter[1];
                string str_select = "select * from tb_farm where name=@name";
                para[0] = PostgreSQL.NewParameter("@name", farm.name);
                var qfarm = PostgreSQL.ExecuteTQuery<Farm>(str_select, null, para);
                PostgreSQL.CloseCon();
                if(qfarm !=null)
                {
                    respons.msg = "1";
                    respons.data = qfarm;
                    token = request.Headers["Token"];
                }
                else
                {
                    PostgreSQL.OpenCon(); 
                    var trans = PostgreSQL.BeginTransaction();
                    try
                    {
                        string str_insert = "insert into tb_farm(\"name\",address,thumb) values(@name,@address,@thumb);";
                        var para1 = new DbParameter[3];
                        para1[0] = PostgreSQL.NewParameter("@name", farm.name);
                        para1[1] = PostgreSQL.NewParameter("@address", farm.address);
                        para1[2] = PostgreSQL.NewParameter("@thumb", farm.thumb);
                        var num = PostgreSQL.ExecuteNoneQuery(str_insert, trans, para1);
                        PostgreSQL.CommitTransaction(trans);
                    }
                    catch (Exception ex)
                    {
                        PostgreSQL.RollbackTransaction(trans);
                    }
                   
                    var para2 = new DbParameter[1];
                    para2[0] = PostgreSQL.NewParameter("@name", farm.name);
                    var qfarm1 = PostgreSQL.ExecuteTQuery<Farm>(str_select, null, para2);
                    PostgreSQL.CloseCon();
                    //响应内容
                    respons.msg = "0";
                    respons.data = qfarm1;
                    token = request.Headers["Token"];
                   
                }
               
            }
            else
            {
                respons.msg = "1";
                respons.data = null;
                token = request.Headers["Token"];
            }
            //添加响应头
            var resultObj = JsonConvert.SerializeObject(respons, Formatting.Indented);
            response.Headers.Add("Token", token);
            response.Content = new StringContent(resultObj);
            return response;    
            
        }
       //加入农场
        [HttpPost]
        [AuthFilterOutside]
        public object JoinFarm(User user)
        {
            string token = null;
            //获取请求
            var request = HttpContext.Current.Request;
            token = request.Headers["Token"];
            var role = RoleType(token);

            //响应
            UserResponse<User> respons = new UserResponse<User>();
            HttpResponseMessage response = new HttpResponseMessage();

            if (role == "1")// "管理员")
            {
                //查询数据库，获取用户农场字段是否为空。
                PostgreSQL.OpenCon();
                string str = "select * from  tb_user where mobile=@mobile;";  //SQL查询语句       
                                                                                 //var trans = PostgreSQL.BeginTransaction();
                var para = new DbParameter[1];
                para[0] = PostgreSQL.NewParameter("@mobile", user.mobile);
                var qfarm = PostgreSQL.ExecuteTableQuery(str, null, para);
                //PostgreSQL.CloseCon();
                var de = qfarm.Rows[0]["farm"].ToString ();
                if (qfarm !=null && de != "")
                {
                    respons.msg = "1";
                    respons.data= PostgreSQL.ExecuteTQuery<User>(str, null, para); 
                    token = request.Headers["Token"];
                    PostgreSQL.CloseCon();
                }
                else
                {
                    PostgreSQL.OpenCon();
                    var trans = PostgreSQL.BeginTransaction();
                    try
                    {
                        string str_update = "update tb_user set farm= @farm where mobile= @mobile;";
                        var para1 = new DbParameter[2];
                        para1[0] = PostgreSQL.NewParameter("@farm", user.farm);
                        para1[1] = PostgreSQL.NewParameter("@mobile", user.mobile);
                        var num = PostgreSQL.ExecuteNoneQuery(str_update, trans, para1);
                        PostgreSQL.CommitTransaction(trans);
                    }
                    catch (Exception ex)
                    {
                        PostgreSQL.RollbackTransaction(trans);
                    }
                    var para2 = new DbParameter[1];
                    para2[0] = PostgreSQL.NewParameter("@mobile", user.mobile);
                    var qfarm1 = PostgreSQL.ExecuteTQuery<User>(str, null, para2);
                    respons.msg = "0";
                    respons.data = qfarm1;
                    token = request.Headers["Token"]; 
                    PostgreSQL.CloseCon();
                }
            }
            else
            {
                respons.msg = "1";
                respons.data = null;
                token = request.Headers["Token"];
            }
            //添加响应头信息
            var resultObj = JsonConvert.SerializeObject(respons, Formatting.Indented);
            response.Headers.Add("Token", token);
            response.Content = new StringContent(resultObj);
            return response;

        }
        //获取农场简报
        [HttpGet]
        [AuthFilterOutside]
        public object GetFarmbrief(string name)
        {
            string token =null;

            //获取请求
            var request = HttpContext.Current.Request;
            //响应
            UserResponse<Farm> respons = new UserResponse<Farm>();
            HttpResponseMessage response = new HttpResponseMessage();
            //查询数据库
            string str ="select thumb from tb_farm where \"name\"= @name";
            PostgreSQL.OpenCon();
            var para = new DbParameter[0];
            para[0]= PostgreSQL.NewParameter("@name", name);
            var qfarm = PostgreSQL.ExecuteTQuery<Farm>(str, null, para);
            PostgreSQL.CloseCon();
            //响应内容
            respons.msg = "0";
            respons.data = qfarm;
            token = request.Headers["Token"];

            //添加响应头
            var resultObj = JsonConvert.SerializeObject(respons, Formatting.Indented);
            response.Headers.Add("Token", token);
            response.Content = new StringContent(resultObj);
            return response;
        }

        ////获取指定地块农事信息
        //[HttpGet]
        //[AuthFilterOutside]
        //public object GetFieldplan(Field_plan fieldplan)
        //{
        //    //获取请求
        //    var request = HttpContext.Current.Request;

        //    //查询数据库
        //    string str = string.Format("select * from tb_field_plan where field = @field");
        //    PostgreSQL.OpenCon();
        //    var para = new DbParameter[1];
        //    para[0] = PostgreSQL.NewParameter("@field", fieldplan.filed);
        //    var qFieldplan = PostgreSQL.ExecuteTQuery<Field_plan>(str, null, para);
        //    //var list = PostgreSQL.ExecuteObjectQuery(str, typeof(Field_plan), trans, new DbParameter[0]);
        //    PostgreSQL.CloseCon();

        //    //响应
        //    UserResponse<Field_plan> respons = new UserResponse<Field_plan>();
        //    HttpResponseMessage response = new HttpResponseMessage();
        //    respons.msg = "0";
        //    respons.data = qFieldplan;
        //    string token = request.Headers["Token"];
        //    //添加响应内容
        //    var resultObj = JsonConvert.SerializeObject(respons, Formatting.Indented);
        //    response.Headers.Add("Token", token);
        //    response.Content = new StringContent(resultObj);
        //    return response;
        //}
        //获取token里用户的角色信息
        private string RoleType(string token)
        {
            //解密Token
            string strtoken = SunGolden.Encryption.DEncrypt.Decrypt(token);
            //
            var index = strtoken.IndexOf(",");
            string str_mobile = strtoken.Substring(0, index);
            string str_role = strtoken.Substring(index + 1);
            return str_role;

        }
    }
}
