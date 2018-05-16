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
            ResultMsg<User> resultmsg = new ResultMsg<User>();
            HttpResponseMessage response = new HttpResponseMessage();
            if (user.mobile.Length != 11 || !Regex.IsMatch(user.mobile, @"^[0-9]*[1-9][0-9]*$"))
            {
                resultmsg.status = false;
                resultmsg.msg = "手机号必须是11位有效数字！";
                resultmsg.data = null;
                token = null;
            }
            else
            {
                PostgreSQL.OpenCon();
                string str_select = "select * from tb_user where mobile = @mobile;";
                var para = new DbParameter[1];
                para[0] = PostgreSQL.NewParameter("@mobile",user.mobile);
                //var list = PostgreSQL.ExecuteObjectQuery(str_select, typeof(User), null, para);
                //var qUser = PostgreSQL.ExecuteObjectQuery(str_select, typeof(User), null, para);
                var qUser = PostgreSQL.ExecuteTQuery<User>(str_select, null, para);
                //判断用户是否存在
                if (qUser != null)
                {
                    resultmsg.status = true;
                    resultmsg.msg = "用户已经存在，登录成功！";
                    resultmsg.data = qUser;
                    token = SunGolden.Encryption.DEncrypt.Encrypt(user.mobile + "," + user.role);
                }
                else
                {
                    var trans = PostgreSQL.BeginTransaction();
                    string str_insert = "insert into tb_user(name,mobile,im_pwd,\"role\") values(@name,@mobile,@im_pwd,@role);";
                    try
                    {
                        var para1 = new DbParameter[4];
                        para1[0] = PostgreSQL.NewParameter("@name", user.mobile);
                        para1[1] = PostgreSQL.NewParameter("@mobile", user.mobile);
                        para1[2] = PostgreSQL.NewParameter("@im_pwd", SunGolden.Encryption.DEncrypt.Encrypt(user.mobile));
                        para1[3] = PostgreSQL.NewParameter("@role", user.role);
                        var num = PostgreSQL.ExecuteNoneQuery(str_insert, trans, para1);
                        PostgreSQL.CommitTransaction(trans);
                    }
                    catch (Exception ex)
                    {
                        PostgreSQL.RollbackTransaction(trans);
                    }
                   
                    //重新查询
                    var para2 = new DbParameter[1];
                    para2[0] = PostgreSQL.NewParameter("@mobile", user.mobile);
                    var qUser1 = PostgreSQL.ExecuteTQuery<User>(str_select, null, para2);

                    resultmsg.status = true;
                    resultmsg.msg = "登录成功！";
                    resultmsg.data = qUser1;
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
            //添加响应头
            var resultObj =JsonConvert.SerializeObject(resultmsg, Formatting.Indented);
            response.Headers.Add("Token", token);
            response.Content = new StringContent(resultObj);
            return response;
        }
        //编辑用户资料
        [HttpPost]
        [AuthFilterOutside]
        public object UpdateUser(User user)
        {
            string token = null;
            //获取请求
            var request = HttpContext.Current.Request;
            //声明响应
            ResultMsg<User> resultmsg = new ResultMsg<User>();
            HttpResponseMessage response = new HttpResponseMessage();
            PostgreSQL.OpenCon();
            var trans = PostgreSQL.BeginTransaction();
            
            string str_update = "update tb_user set \"name\"= @name,mobile= @mobile,\"role\"= @role,farm= @farm where mobile= @mobile;";
            try
            {
                var para = new DbParameter[4];
                para[0] = PostgreSQL.NewParameter("@name", user.name);
                para[1] = PostgreSQL.NewParameter("@mobile", user.mobile);
                para[2] = PostgreSQL.NewParameter("@role", user.role);
                para[3] = PostgreSQL.NewParameter("@farm", user.farm);
                var num = PostgreSQL.ExecuteNoneQuery(str_update, trans, para);
                PostgreSQL.CommitTransaction(trans);
                //查询数据库
                string str_select = "select * from tb_user where mobile = @mobile;";
                var para1 = new DbParameter[1];
                para1[0] = PostgreSQL.NewParameter("@mobile", user.mobile);
                var qUser = PostgreSQL.ExecuteTQuery<User>(str_select, null, para1);
                //响应内容
                resultmsg.status = true;
                resultmsg.msg = "成功编辑用户资料！";
                resultmsg.data = qUser;
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
            //添加响应头
            var resultObj = JsonConvert.SerializeObject(resultmsg, Formatting.Indented);
            response.Headers.Add("Token", token);
            response.Content = new StringContent(resultObj);
            return response;
        }
        //等待验证
        [HttpPost]
        [AuthFilterOutside]
        public object WaitProve(UserTask task)
        {
            string token = null;

            //获取请求
            var request = HttpContext.Current.Request;
            //响应
            ResultMsg<UserTask> resultmsg = new ResultMsg<UserTask>();
            HttpResponseMessage response = new HttpResponseMessage();
            PostgreSQL.OpenCon();
            //查询数据库 
            string str_select = "select * from tb_user_task where  farm= @farm and creator= @creator;";
            var para2 = new DbParameter[2];
            para2[0] = PostgreSQL.NewParameter("@creator", task.creator);
            para2[1] = PostgreSQL.NewParameter("@farm", task.farm);
            var qselcet = PostgreSQL.ExecuteTQuery<UserTask>(str_select, null, para2);
            if(qselcet!=null)
            {
                var trans = PostgreSQL.BeginTransaction();
                string str_update = "update tb_user_task set state=@state,agree=@agree where farm= @farm and creator= @creator;";
                try
                {
                    
                    var para = new DbParameter[4];
                    para[0] = PostgreSQL.NewParameter("@creator", task.creator);
                    para[1] = PostgreSQL.NewParameter("@state", task.state);
                    para[2] = PostgreSQL.NewParameter("@agree", task.agree);
                    para[3] = PostgreSQL.NewParameter("@farm", task.farm);
                    var num = PostgreSQL.ExecuteNoneQuery(str_update, trans, para);
                    PostgreSQL.CommitTransaction(trans);
                    //响应内容
                    if (task.agree==false)
                    {
                        resultmsg.msg = "不同意加入农场！";
                    }
                   else
                    {
                        resultmsg.msg = "同意加入农场！";
                    }
                    resultmsg.status = true;
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
                //查询数据库
                string str = "insert into tb_user_task(creator,\"type\",examiner,description,\"state\",agree,farm) values(@creator,@type,@examiner,@description,@state,@agree,@farm);";
                var trans = PostgreSQL.BeginTransaction();
                try
                {
                    var para = new DbParameter[7];
                    para[0] = PostgreSQL.NewParameter("@creator", task.creator);
                    para[1] = PostgreSQL.NewParameter("@type", task.type);
                    para[2] = PostgreSQL.NewParameter("@examiner", task.examiner);
                    para[3] = PostgreSQL.NewParameter("@description", task.description);
                    para[4] = PostgreSQL.NewParameter("@state", task.state);
                    para[5] = PostgreSQL.NewParameter("@agree", task.agree);
                    para[6] = PostgreSQL.NewParameter("@farm", task.farm);

                    var num = PostgreSQL.ExecuteNoneQuery(str, trans, para);
                    PostgreSQL.CommitTransaction(trans);
                    //查询数据库 
                    //string str_select = "select agree from tb_user_task where  farm= @farm and creator= @creator;";
                    var para1 = new DbParameter[2];
                    para1[0] = PostgreSQL.NewParameter("@creator", task.creator);
                    para1[1] = PostgreSQL.NewParameter("@farm", task.farm);
                    var qtask = PostgreSQL.ExecuteTQuery<UserTask>(str_select, null, para1);

                    //响应内容
                    resultmsg.status = true;
                    resultmsg.msg = "等待验证";
                    resultmsg.data = qtask;
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
           
            //添加响应头
            var resultObj = JsonConvert.SerializeObject(resultmsg, Formatting.Indented);
            response.Headers.Add("Token", token);
            response.Content = new StringContent(resultObj);
            return response;
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
