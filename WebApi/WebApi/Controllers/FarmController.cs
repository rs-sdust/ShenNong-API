using Newtonsoft.Json;
using SunGolden.DBUtils;
using System;
using System.Collections.Generic;
using System.Data.Common;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web;
using System.Web.Http;
using WebApi.Auth;
using WebApi.Models;

namespace WebApi.Controllers
{
    [AuthFilterOutside]
    public class FarmController : ApiController
    {
        //获取农场列表
        [HttpPost]
        public object GetFarms(Farm farm)
        {
            string token = null;
            //获取请求
            var request = HttpContext.Current.Request;
            //响应
            ResultMsg<List<Farm>> resultmsg = new ResultMsg<List<Farm>>();
            HttpResponseMessage response = new HttpResponseMessage();
            //查询数据库
            string str = "select * from tb_farm where address = @address";
            PostgreSQL.OpenCon();
            var para = new DbParameter[1];
            para[0] = PostgreSQL.NewParameter("@address", farm.address);
            var qfarm = PostgreSQL.ExecuteTListQuery<Farm>(str, null, para);
            PostgreSQL.CloseCon();
            if(qfarm.Count <= 0)
            {
                //响应内容
                resultmsg.status = false;
                resultmsg.msg = "未获取此区域的农场列表!";
                resultmsg.data = null;
            }
            else
            {
                resultmsg.status = true;
                resultmsg.msg = "成功获取农场列表!";
                resultmsg.data = qfarm;
            }
            //响应内容           
            token = request.Headers["Token"];
            //添加响应头
            var resultObj = JsonConvert.SerializeObject(resultmsg, Formatting.Indented);
            response.Headers.Add("Token", token);
            response.Content = new StringContent(resultObj);
            return response;
        }
        //创建农场
        [HttpPost]
        public object CreatFarm(Farm farm)
        {
            string token = null;
            //获取请求
            var request = HttpContext.Current.Request;
            token = request.Headers["Token"];
            var role = RoleType(token);
            //响应
            ResultMsg<Farm> resultmsg = new ResultMsg<Farm>();
            HttpResponseMessage response = new HttpResponseMessage();
            //如果用户是农场主，则创建农场
            if (role == "0")//"农场主"
            {
                PostgreSQL.OpenCon();
                var para = new DbParameter[2];
                string str_select = "select * from tb_farm where name=@name and address=@address";
                para[0] = PostgreSQL.NewParameter("@name", farm.name);
                para[1] = PostgreSQL.NewParameter("@address", farm.address);
                var qfarm = PostgreSQL.ExecuteTQuery<Farm>(str_select, null, para);
                PostgreSQL.CloseCon();
                if (qfarm != null)
                {
                    resultmsg.status = false;
                    resultmsg.msg = "农场名已经存在，请重新命名！";
                    resultmsg.data = null;
                    token = request.Headers["Token"];
                }
                else
                {
                    PostgreSQL.OpenCon();
                    var trans = PostgreSQL.BeginTransaction();
                    try
                    {
                        string str_insert = "insert into tb_farm(\"name\",address) values(@name,@address);";
                        var para1 = new DbParameter[2];
                        para1[0] = PostgreSQL.NewParameter("@name", farm.name);
                        para1[1] = PostgreSQL.NewParameter("@address", farm.address);
                        //para1[2] = PostgreSQL.NewParameter("@thumb", farm.thumb);
                        var num = PostgreSQL.ExecuteNoneQuery(str_insert, trans, para1);
                        PostgreSQL.CommitTransaction(trans);
                        //响应内容
                        resultmsg.status = true;
                        resultmsg.msg = "成功创建农场!";
                        resultmsg.data = null;
                        token = request.Headers["Token"];
                    }
                    catch (Exception ex)
                    {
                        PostgreSQL.RollbackTransaction(trans);
                        //响应内容
                        resultmsg.status = false;
                        resultmsg.msg = "[ERROR] 数据库操作出现异常：" + ex.Message;
                        resultmsg.data = null;
                        token = request.Headers["Token"];
                    }
                    finally
                    {
                        PostgreSQL.CloseCon();
                    }                                  
                }
            }
            else
            {
                resultmsg.status = false;
                resultmsg.msg = "不是农场主无法创建农场!";
                resultmsg.data = null;
                token = request.Headers["Token"];
            }
            //添加响应头
            var resultObj = JsonConvert.SerializeObject(resultmsg, Formatting.Indented);
            response.Headers.Add("Token", token);
            response.Content = new StringContent(resultObj);
            return response;
        }
        //加入农场
        [HttpPost]
        public object JoinFarm(User user)
        {
            string token = null;
            //获取请求
            var request = HttpContext.Current.Request;
            token = request.Headers["Token"];
            var role = RoleType(token);
            //响应
            ResultMsg<User> resultmsg = new ResultMsg<User>();
            HttpResponseMessage response = new HttpResponseMessage();

            if (role == "1")// "管理员"
            {
                //string str = "select * from  tb_user where mobile=@mobile;";  //SQL查询语句       
                PostgreSQL.OpenCon();
                var trans = PostgreSQL.BeginTransaction();
                try
                {
                    string str_update = "update tb_user set farm= @farm where id=@id;";// mobile= @mobile;";
                    var para1 = new DbParameter[2];
                    para1[0] = PostgreSQL.NewParameter("@farm", user.farm);
                    para1[1] = PostgreSQL.NewParameter("@id", user.id);
                    var num = PostgreSQL.ExecuteNoneQuery(str_update, trans, para1);
                    PostgreSQL.CommitTransaction(trans);
                    //查询数据库
                    //var para2 = new DbParameter[1];
                    //para2[0] = PostgreSQL.NewParameter("@mobile", user.mobile);
                    //var qfarm1 = PostgreSQL.ExecuteTQuery<User>(str, null, para2);
                    //PostgreSQL.CloseCon();
                    //响应内容
                    resultmsg.status = true;
                    resultmsg.msg = "成功加入农场!";
                    resultmsg.data = null;
                    token = request.Headers["Token"];
                }
                catch (Exception ex)
                {
                    PostgreSQL.RollbackTransaction(trans);
                    //响应内容
                    resultmsg.status = false;
                    resultmsg.msg = "[ERROR] 数据库操作出现异常：" + ex.Message;
                    resultmsg.data = null;
                    token = request.Headers["Token"];
                }            
            }
            else
            {
                resultmsg.status = false;
                resultmsg.msg = "用户为农场主，不用加入农场!";
                resultmsg.data = null;
                token = request.Headers["Token"];
            }
            //添加响应头信息
            var resultObj = JsonConvert.SerializeObject(resultmsg, Formatting.Indented);
            response.Headers.Add("Token", token);
            response.Content = new StringContent(resultObj);
            return response;
        }
        //获取农场简报
        [HttpGet]
        public object GetFarmbrief(string name, string weather)
        {

            string response = null;
            return response;
        }
        //获取token里role的值
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
