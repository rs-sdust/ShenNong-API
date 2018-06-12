using Newtonsoft.Json;
using SunGolden.DBUtils;
using System;
using System.Collections.Generic;
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
    /// <summary>
    /// 用户相关操作
    /// </summary>
    public class UserController : ApiController
    {
        /// <summary>
        /// 判断用户是否存在
        /// </summary>
        /// <param name="mobile"></param>
        /// <returns></returns>
        [HttpGet]
        public object IsExist(string mobile)
        {
            string token = "";
            ResultMsg<User> resultmsg = new ResultMsg<User>();
            HttpResponseMessage response = new HttpResponseMessage();
            PostgreSQL.OpenCon();//打开数据库
            DateTime time = DateTime.Now.Date.AddDays(30);
            if (mobile.Length != 11 || !Regex.IsMatch(mobile, @"^[0-9]*[0-9][0-9]*$"))
            {
                resultmsg.status = false;
                resultmsg.msg = "手机号必须是11位有效数字！";
                resultmsg.data = null;
                token = null;
            }
            else
            {
                string str_select = "select * from tb_user where mobile = @mobile;";
                var para = new DbParameter[1];
                para[0] = PostgreSQL.NewParameter("@mobile", mobile);
                var qUser = PostgreSQL.ExecuteTQuery<User>(str_select, null, para);
                if (qUser != null)//判断用户是否存在
                {
                    resultmsg.status = true;
                    resultmsg.msg = "用户已经存在，登录成功！";
                    resultmsg.data = qUser;
                    token = SunGolden.Encryption.DEncrypt.Encrypt(mobile + "," + qUser.role + "," + time);
                }
                else
                {
                    //返回信息
                    resultmsg.status = false;
                    resultmsg.msg = "用户不存在，请编辑用户资料!";
                    resultmsg.data = null;
                    token = null;
                }
            }
            PostgreSQL.CloseCon();//关闭数据库
            //添加响应头
            var resultObj = JsonConvert.SerializeObject(resultmsg, Formatting.Indented);
            response.Headers.Add("Token", token);
            response.Content = new StringContent(resultObj);
            return response;
        }
        /// <summary>
        /// 注册
        /// </summary>
        /// <param name="user">用户类</param>
        /// <returns></returns>
        [HttpPost]
        public object Register(User user)
        {
            string token = "";
            ResultMsg<User> resultmsg = new ResultMsg<User>();
            HttpResponseMessage response = new HttpResponseMessage();
            if (user.mobile.Length != 11 || !Regex.IsMatch(user.mobile, @"^[0-9]*[0-9][0-9]*$"))
            {
                resultmsg.status = false;
                resultmsg.msg = "手机号必须是11位有效数字！";
                resultmsg.data = null;
                token = null;
            }
            else
            {
                PostgreSQL.OpenCon();//打开数据库
                DateTime time = DateTime.Now.Date.AddDays(30);
                //插入数据
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
                    //重新查询
                    string str_select = "select * from tb_user where mobile = @mobile;";
                    var para2 = new DbParameter[1];
                    para2[0] = PostgreSQL.NewParameter("@mobile", user.mobile);
                    var qUser = PostgreSQL.ExecuteTQuery<User>(str_select, null, para2);
                    //返回信息
                    resultmsg.status = true;
                    resultmsg.msg = "登录成功!";
                    resultmsg.data = qUser;
                    token = SunGolden.Encryption.DEncrypt.Encrypt(user.mobile + "," + user.role + "," + time);
                }
                catch (Exception ex)
                {
                    PostgreSQL.RollbackTransaction(trans);
                    //返回信息
                    resultmsg.status = false;
                    resultmsg.msg = "[ERROR] 数据库操作出现异常：" + ex.Message;
                    resultmsg.data = null;
                    token = null;
                }
                PostgreSQL.CloseCon();//关闭数据库
            }
            //添加响应头
            var resultObj = JsonConvert.SerializeObject(resultmsg, Formatting.Indented);
            response.Headers.Add("Token", token);
            response.Content = new StringContent(resultObj);
            return response;
        }
        /// <summary>
        /// 登录
        /// </summary>
        /// <param name="user">用户类</param>
        /// <returns></returns>
        [HttpPost]
        public object Login(User user)
        {
            string token="";  
            ResultMsg<User> resultmsg = new ResultMsg<User>();
            HttpResponseMessage response = new HttpResponseMessage();
            PostgreSQL.OpenCon();//打开数据库
            DateTime time = DateTime.Now.Date.AddDays(30);
            if (user.mobile.Length != 11 || !Regex.IsMatch(user.mobile, @"^[0-9]*[0-9][0-9]*$"))
            {
                resultmsg.status = false;
                resultmsg.msg = "手机号必须是11位有效数字！";
                resultmsg.data = null;
                token = null;
            }
            else
            {
                string str_select = "select * from tb_user where mobile = @mobile;";
            //    var para = new DbParameter[1];
            //    para[0] = PostgreSQL.NewParameter("@mobile",user.mobile);
            //    //var list = PostgreSQL.ExecuteObjectQuery(str_select, typeof(User), null, para);
            //    //var qUser = PostgreSQL.ExecuteObjectQuery(str_select, typeof(User), null, para);
            //    var qUser = PostgreSQL.ExecuteTQuery<User>(str_select, null, para);
            //    if (qUser != null)//判断用户是否存在
            //    {
            //        resultmsg.status = true;
            //        resultmsg.msg = "用户已经存在，登录成功！";
            //        resultmsg.data = qUser;
            //        token = SunGolden.Encryption.DEncrypt.Encrypt(user.mobile + "," + user.role + ","+ time);
            //    }
            //    else
            //    {
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
                        //重新查询
                        var para2 = new DbParameter[1];
                        para2[0] = PostgreSQL.NewParameter("@mobile", user.mobile);
                        var qUser = PostgreSQL.ExecuteTQuery<User>(str_select, null, para2);
                        //返回信息
                        resultmsg.status = true;
                        resultmsg.msg = "登录成功!";
                        resultmsg.data = qUser;
                        token = SunGolden.Encryption.DEncrypt.Encrypt(user.mobile + "," + user.role+ ","+ time);
                    }
                    catch (Exception ex)
                    {
                        PostgreSQL.RollbackTransaction(trans);
                        //返回信息
                        resultmsg.status = false;
                        resultmsg.msg = "[ERROR] 数据库操作出现异常：" + ex.Message;
                        resultmsg.data = null;
                        token = null;
                    }

                //添加OpenIM用户信息
                //Userinfo info = new Userinfo();
                //info.mobile = user.mobile;
                //info.userid = user.mobile;
                //info.password = SunGolden.Encryption.DEncrypt.Encrypt(user.mobile);
                //OpenIm(info);                   
            }
            //}
            PostgreSQL.CloseCon();//关闭数据库
            //添加响应头
            var resultObj =JsonConvert.SerializeObject(resultmsg, Formatting.Indented);
            response.Headers.Add("Token", token);
            response.Content = new StringContent(resultObj);
            return response;
        }
        /// <summary>
        /// 编辑用户资料
        /// </summary>
        /// <param name="user">用户类</param>
        /// <returns></returns>
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
            if (user == null&& user.name==null)//判断输入的数据格式是否正确
            {
                resultmsg.status = false;
                resultmsg.msg = "输入的数据格式不正确,请检查后重新输入!";
                resultmsg.data = null;
                token = request.Headers["Token"];
                //添加响应头
                var resultObj1 = JsonConvert.SerializeObject(resultmsg, Formatting.Indented);
                response.Headers.Add("Token", token);
                response.Content = new StringContent(resultObj1);
                return response;
            }
            if (user.mobile.Length != 11 || !Regex.IsMatch(user.mobile, @"^[0-9]*[0-9][0-9]*$"))//判断更换的手机号是否是纯数字
            {
                resultmsg.status = false;
                resultmsg.msg = "手机号必须是11位有效数字！";
                resultmsg.data = null;
                token = null;
                //添加响应头
                var resultObj1 = JsonConvert.SerializeObject(resultmsg, Formatting.Indented);
                response.Headers.Add("Token", token);
                response.Content = new StringContent(resultObj1);
                return response;
            }
            PostgreSQL.OpenCon();//打开数据库
            //判断用户
            string strtoken = SunGolden.Encryption.DEncrypt.Decrypt(request.Headers["Token"]); //解密Token
            string str_mobile = strtoken.Substring(0, strtoken.IndexOf(","));
            //查询数据库
            string str1 = string.Format("select * from tb_user where \"mobile\" =\'{0}\'", str_mobile);
            //string str ="select farm from tb_user where \"mobile\" = \'@mobile\'}";
            var para2 = new DbParameter[1];
            para2[0] = PostgreSQL.NewParameter("@moblie", str_mobile);
            var quser = PostgreSQL.ExecuteTQuery<User>(str1, null, para2);
            if (quser.id==user.id)//判断输入id是否是登陆用户的id
            {
                var trans = PostgreSQL.BeginTransaction();
                string str_update = "";
                try
                {
                    var num = 0;
                    if (quser.role == 0)//判断用户角色是否是农场主，若是农场主获取农场id，否则farm=-1
                    {
                        str_update = "update tb_user set \"name\"= @name,mobile= @mobile,\"role\"= @role ,farm=@farm where id=@id;";
                        var para = new DbParameter[5];
                        para[0] = PostgreSQL.NewParameter("@name", user.name);
                        para[1] = PostgreSQL.NewParameter("@mobile", user.mobile);
                        para[2] = PostgreSQL.NewParameter("@role", user.role);
                        para[3] = PostgreSQL.NewParameter("@id", user.id);
                        para[4] = PostgreSQL.NewParameter("@farm", user.farm);
                        num = PostgreSQL.ExecuteNoneQuery(str_update, trans, para);
                    }
                    else
                    {
                        str_update = "update tb_user set \"name\"= @name,mobile= @mobile,\"role\"= @role,farm=@farm  where id=@id;";
                        var para = new DbParameter[4];
                        para[0] = PostgreSQL.NewParameter("@name", user.name);
                        para[1] = PostgreSQL.NewParameter("@mobile", user.mobile);
                        para[2] = PostgreSQL.NewParameter("@role", user.role);
                        para[3] = PostgreSQL.NewParameter("@id", user.id);
                        para[4]= PostgreSQL.NewParameter("@farm", -1);
                        num = PostgreSQL.ExecuteNoneQuery(str_update, trans, para);
                    }
                    PostgreSQL.CommitTransaction(trans);
                    //查询数据库
                    string str_select = "select * from tb_user where mobile = @mobile;";
                    var para1 = new DbParameter[1];
                    para1[0] = PostgreSQL.NewParameter("@mobile", user.mobile);
                    var qUser = PostgreSQL.ExecuteTQuery<User>(str_select, null, para1);
                    //返回信息
                    resultmsg.status = true;
                    resultmsg.msg = "成功更新用户资料！";
                    resultmsg.data = qUser;
                    token = request.Headers["Token"]; ;
                  
                }
                catch (Exception ex)
                {
                    PostgreSQL.RollbackTransaction(trans);
                    //返回信息
                    resultmsg.status = false;
                    resultmsg.msg = "[ERROR] 数据库操作出现异常：" + ex.Message;
                    resultmsg.data = null;
                    token = request.Headers["Token"];
                }
            }
            else
            {
                //返回信息
                resultmsg.status = true;
                resultmsg.msg = "输入的id，不是登录用户，无法更新用户资料！";
                resultmsg.data = null;
                token = request.Headers["Token"];
            }
            PostgreSQL.CloseCon();//关闭数据库
            //添加响应头
            var resultObj = JsonConvert.SerializeObject(resultmsg, Formatting.Indented);
            response.Headers.Add("Token", token);
            response.Content = new StringContent(resultObj);
            return response;
        }
        /// <summary>
        /// 获取指定农场的所有用户,即我的伙伴
        /// </summary>
        /// <param name="farmid">农场id</param>
        /// <returns></returns>
        [HttpGet]
        [AuthFilterOutside]
        public object GetUsers(int farmid)
        {
            string token = null;
            //获取请求
            var request = HttpContext.Current.Request;
            //响应
            ResultMsg<List<User>> resultmsg = new ResultMsg<List<User>>();
            HttpResponseMessage response = new HttpResponseMessage();
            PostgreSQL.OpenCon();//打开数据库
            //查询数据库 
            string str_select = "select * from tb_user where farm= @farm;";
            var para = new DbParameter[1];
            para[0] = PostgreSQL.NewParameter("@farm", farmid);
            var qlist = PostgreSQL.ExecuteTListQuery<User>(str_select, null, para);
            PostgreSQL.CloseCon();//关闭数据库
            if (qlist.Count <= 0)
            {
                //返回信息
                resultmsg.status = false;
                resultmsg.msg = "未获取所有用户!";
                resultmsg.data = null;
            }
            else
            {
                //返回信息
                resultmsg.status = true;
                resultmsg.msg = "成功获取所有用户！";
                resultmsg.data = qlist;
            }
            //返回信息token
            token = request.Headers["Token"];
            //添加响应头
            var resultObj = JsonConvert.SerializeObject(resultmsg, Formatting.Indented);
            response.Headers.Add("Token", token);
            response.Content = new StringContent(resultObj);
            return response;
        }
        /// <summary>
        /// 意见反馈
        /// </summary>
        /// <param name="feed">反馈类</param>
        /// <returns></returns>
        [HttpPost]
        [AuthFilterOutside]
        public object CreateFeedback(FeedBack feed)
        {
            string token = null;
            //获取请求
            var request = HttpContext.Current.Request;
            //声明响应
            ResultMsg<User> resultmsg = new ResultMsg<User>();
            HttpResponseMessage response = new HttpResponseMessage();
            if (feed == null)//判断输入的数据格式是否正确
            {
                resultmsg.status = false;
                resultmsg.msg = "输入的数据格式不正确,请检查后重新输入!";
                resultmsg.data = null;
                token = request.Headers["Token"];
            }
            else
            {
                PostgreSQL.OpenCon();
                var tans = PostgreSQL.BeginTransaction();
                try
                {
                    string str = "insert into tb_feedback (userid ,suggestion ,url)values(@userid,@suggestion,@url) ; ";                 
                    var para = new DbParameter[3];
                    para[0] = PostgreSQL.NewParameter("@suggestion", feed.suggestion);
                    para[1] = PostgreSQL.NewParameter("@url", feed.url);
                    para[2] = PostgreSQL.NewParameter("@userid", feed.userid);
                    int num = PostgreSQL.ExecuteNoneQuery(str, tans, para);
                    PostgreSQL.CommitTransaction(tans);
                    //返回信息
                    resultmsg.status = true;
                    resultmsg.msg = "意见反馈成功！";
                    resultmsg.data = null;
                    token = request.Headers["Token"];
                }
                catch(Exception ex)
                {
                    PostgreSQL.RollbackTransaction(tans);
                    //返回信息
                    resultmsg.status = false;
                    resultmsg.msg = "[ERROR] 数据库操作出现异常：" + ex.Message;
                    resultmsg.data = null;
                    token = request.Headers["Token"];
                }
                PostgreSQL.CloseCon();
            }
            //添加响应头
            var resultObj = JsonConvert.SerializeObject(resultmsg, Formatting.Indented);
            response.Headers.Add("Token", token);
            response.Content = new StringContent(resultObj);
            return response;
        }
        ///// <summary>
        ///// 加入农场
        ///// </summary>
        ///// <param name="user">用户类</param>
        ///// <returns></returns>       
        //[HttpPost]
        //[AuthFilterOutside]
        //public object JoinFarm(User user)
        //{
        //    string token = null;
        //    //获取请求
        //    var request = HttpContext.Current.Request;
        //    token = request.Headers["Token"];
        //    AuthFilterOutside auth = new AuthFilterOutside();
        //    var role = auth.RoleType(token);
        //    //响应
        //    ResultMsg<User> resultmsg = new ResultMsg<User>();
        //    HttpResponseMessage response = new HttpResponseMessage();
        //    PostgreSQL.OpenCon();//打开数据库
        //    //判断用户
        //    string strtoken = SunGolden.Encryption.DEncrypt.Decrypt(request.Headers["Token"]); //解密Token
        //    string str_mobile = strtoken.Substring(0, strtoken.IndexOf(","));
        //    //查询数据库
        //    string str1 = string.Format("select * from tb_user where \"mobile\" =\'{0}\'", str_mobile);
        //    //string str ="select farm from tb_user where \"mobile\" = \'@mobile\'}";
        //    var para2 = new DbParameter[1];
        //    para2[0] = PostgreSQL.NewParameter("@moblie", str_mobile);
        //    var quser = PostgreSQL.ExecuteTQuery<User>(str1, null, para2);
        //    if (quser != null)
        //    {
        //        if (role == "1")// "管理员"
        //        {
        //            //string str = "select * from  tb_user where mobile=@mobile;";  //SQL查询语句       
        //            var trans = PostgreSQL.BeginTransaction();
        //            try
        //            {
        //                string str_update = "update tb_user set farm= @farm where id=@id;";// mobile= @mobile;";
        //                var para1 = new DbParameter[2];
        //                para1[0] = PostgreSQL.NewParameter("@farm", user.farm);
        //                para1[1] = PostgreSQL.NewParameter("@id", user.id);
        //                var num = PostgreSQL.ExecuteNoneQuery(str_update, trans, para1);
        //                PostgreSQL.CommitTransaction(trans);
        //                //查询数据库
        //                //var para2 = new DbParameter[1];
        //                //para2[0] = PostgreSQL.NewParameter("@mobile", user.mobile);
        //                //var qfarm1 = PostgreSQL.ExecuteTQuery<User>(str, null, para2);
        //                //PostgreSQL.CloseCon();
        //                //返回信息
        //                resultmsg.status = true;
        //                resultmsg.msg = "成功加入农场!";
        //                resultmsg.data = null;
        //                token = request.Headers["Token"];
        //            }
        //            catch (Exception ex)
        //            {
        //                PostgreSQL.RollbackTransaction(trans);
        //                //返回信息
        //                resultmsg.status = false;
        //                resultmsg.msg = "[ERROR] 数据库操作出现异常：" + ex.Message;
        //                resultmsg.data = null;
        //                token = request.Headers["Token"];
        //            }
        //        }
        //        else
        //        {
        //            resultmsg.status = false;
        //            resultmsg.msg = "用户为农场主，不用加入农场!";
        //            resultmsg.data = null;
        //            token = request.Headers["Token"];
        //        }
        //    }
        //    else
        //    {
        //        resultmsg.status = false;
        //        resultmsg.msg = "此用户不存在,非用户操作!";
        //        resultmsg.data = null;
        //        token = request.Headers["Token"];
        //    }
        //    PostgreSQL.CloseCon();//关闭数据库
        //    //添加响应头信息
        //    var resultObj = JsonConvert.SerializeObject(resultmsg, Formatting.Indented);
        //    response.Headers.Add("Token", token);
        //    response.Content = new StringContent(resultObj);
        //    return response;
        //}
       
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
