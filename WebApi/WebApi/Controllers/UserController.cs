using Newtonsoft.Json;
using SunGolden.DBUtils;
using System;
using System.Data.Common;
using System.Net.Http;
using System.Text.RegularExpressions;
using System.Web;
using System.Web.Http;
using Top.Api;
using Top.Api.Request;
using Top.Api.Response;
using WebApi.Auth;
using WebApi.Models;

namespace WebApi.Controllers
{
   
    public class UserController : ApiController
    {
        //登录
        [HttpPost]
        public object Login(User user)
        {
            string token="";
            //UserResponse<List<User>> respons = new UserResponse<List<User>>();
            UserResponse<User> respons = new UserResponse<User>();
            HttpResponseMessage response = new HttpResponseMessage();
            if (user.mobile.Length != 11 || !Regex.IsMatch(user.mobile, @"^[0-9]*[1-9][0-9]*$"))
            {
                respons.msg = "1";
                respons.data = null;
                token = null;
            }
            else
            {
                PostgreSQL.OpenCon();
                string str_select = "select * from tb_user where mobile = @mobile;";
                var para = new DbParameter[1];
                para[0] = PostgreSQL.NewParameter("@mobile", user.mobile);
                //var list = PostgreSQL.ExecuteObjectQuery(str_select, typeof(User), null, para);
                //var qUser = PostgreSQL.ExecuteObjectQuery(str_select, typeof(User), null, para);
                var qUser = PostgreSQL.ExecuteTQuery<User>(str_select, null, para);
                PostgreSQL.CloseCon();
                //判断用户是否存在
                if (qUser != null)
                {
                    respons.msg = "0";
                    respons.data = qUser;
                    token = SunGolden.Encryption.DEncrypt.Encrypt(user.mobile + "," + user.role);
                }
                else
                {
                    PostgreSQL.OpenCon();
                    var trans = PostgreSQL.BeginTransaction();
                    string str_insert = "insert into tb_user(mobile,im_pwd,\"role\") values(@mobile,@im_pwd,@role);";
                    try
                    {
                        var para1 = new DbParameter[3];
                        para1[0] = PostgreSQL.NewParameter("@mobile", user.mobile);
                        para1[1] = PostgreSQL.NewParameter("@im_pwd", SunGolden.Encryption.DEncrypt.Encrypt(user.mobile));
                        para1[2] = PostgreSQL.NewParameter("@role", user.role);
                        var num = PostgreSQL.ExecuteNoneQuery(str_insert, trans, para1);
                        PostgreSQL.CommitTransaction(trans);
                    }
                    catch (Exception ex)
                    {
                        PostgreSQL.RollbackTransaction(trans);
                    }
                    respons.msg = "0";
                    respons.data =  PostgreSQL.ExecuteTQuery<User>(str_select, null, para);
                    token = SunGolden.Encryption.DEncrypt.Encrypt(user.mobile + "," + user.role);
                    PostgreSQL.CloseCon();
                    //添加OpenIM用户信息
                    //Userinfo info = new Userinfo();
                    //info.mobile = user.mobile;
                    //info.userid = user.mobile;
                    //info.password = SunGolden.Encryption.DEncrypt.Encrypt(user.mobile);
                    //OpenIm(info);
                   
                }
            }
            var resultObj =JsonConvert.SerializeObject(respons, Formatting.Indented);
            response.Headers.Add("Token", token);
            response.Content = new StringContent(resultObj);
            return response;
        }
        [HttpPost]
        [AuthFilterOutside]
        public object UpdateUser(User user)
        {
            string token = null;
            //获取请求
            var request = HttpContext.Current.Request;
            //声明响应
            UserResponse<User> respons = new UserResponse<User>();
            HttpResponseMessage response = new HttpResponseMessage();
            PostgreSQL.OpenCon();
            var trans = PostgreSQL.BeginTransaction();
            
            string str_update = "update tb_user set \"name\"= @name,mobile= @mobile,\"role\"= @role,farm= @farm where mobile= @mobile;";
            try
            {
                var para1 = new DbParameter[4];
                para1[0] = PostgreSQL.NewParameter("@name", user.name);
                para1[1] = PostgreSQL.NewParameter("@mobile", user.mobile);
                para1[3] = PostgreSQL.NewParameter("@role", user.role);
                para1[4] = PostgreSQL.NewParameter("@farm", user.farm);
                var num = PostgreSQL.ExecuteNoneQuery(str_update, trans, para1);
                PostgreSQL.CommitTransaction(trans);
            }
            catch (Exception ex)
            {
                PostgreSQL.RollbackTransaction(trans);
            }
            PostgreSQL.CloseCon();
            PostgreSQL.OpenCon();
            string str_select = "select * from tb_user where mobile = @mobile;";
            var para = new DbParameter[1];
            para[0] = PostgreSQL.NewParameter("@mobile", user.mobile);
            var qUser = PostgreSQL.ExecuteTQuery<User>(str_select, null, para);
            PostgreSQL.CloseCon();
            respons.msg = "0";
            respons.data = qUser;
            token = request.Headers["Token"];
            return respons;
        }
        //OpenIM
        private void OpenIm(Userinfo userinfo)
        {
            string url = "https://eco.taobao.com/router/rest";
            string appkey = "24863493";
            string appsecret = "faaf14c5f5f5331387a50e43e2e9b599";
            ITopClient client = new DefaultTopClient(url, appkey, appsecret);
            OpenimUsersAddRequest req = new OpenimUsersAddRequest();
            req.Userinfos = JsonConvert.SerializeObject(userinfo, Formatting.Indented); //"{\"userid\":\"" + userinfo + "\" ,\"password\":\"" +SunGolden.Encryption.DEncrypt.Encrypt(userinfo) + "\" ,\"mobile\":\"" + userinfo + "\"}";
            OpenimUsersAddResponse response = client.Execute(req);
        }
    }
}
