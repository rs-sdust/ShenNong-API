using Newtonsoft.Json;
using SunGolden.DBUtils;
using System;
using System.Collections.Generic;
using System.Data.Common;
using System.Net.Http;
using System.Web;
using System.Web.Http;
using WebApi.Auth;
using WebApi.Models;

namespace WebApi.Controllers
{
    /// <summary>
    /// 任务消息相关接口
    /// </summary>
    [AuthFilterOutside]
    public class TaskController : ApiController
    {
        /// <summary>
        /// 获取指定用户的所有任务信息
        /// </summary>
        /// <param name="creator"></param>
        /// <returns></returns>
        [HttpGet]
        public object GetUserTask(int creator)
        {
            string token = null;
            //获取请求
            var request = HttpContext.Current.Request;
            //响应
            ResultMsg<List<UserTask>> resultmsg = new ResultMsg<List<UserTask>>();
            HttpResponseMessage response = new HttpResponseMessage();
            PostgreSQL.OpenCon();//打开数据库
            //查询数据库 
            string str_select = "select * from tb_user_task where creator= @creator;";
            var para = new DbParameter[1];
            para[0] = PostgreSQL.NewParameter("@creator", creator);
            var qlist = PostgreSQL.ExecuteTListQuery<UserTask>(str_select, null, para);
            PostgreSQL.CloseCon();//关闭数据库
            if (qlist.Count <= 0)
            {
                //返回信息
                resultmsg.status = false;
                resultmsg.msg = "未获取任务消息!";
                resultmsg.data = null;
            }
            else
            {
                //返回信息
                resultmsg.status = true;
                resultmsg.msg = "成功获取任务消息！";
                resultmsg.data = qlist;
            }
            //返回信息
            token = request.Headers["Token"];
            //添加响应头
            var resultObj = JsonConvert.SerializeObject(resultmsg, Formatting.Indented);
            response.Headers.Add("Token", token);
            response.Content = new StringContent(resultObj);
            return response;
        }
        /// <summary>
        /// 获取指定农场的所有任务信息
        /// </summary>
        /// <param name="examiner">审查人编号</param>
        /// <returns></returns>
        [HttpGet]
        public object GetFarmerTask(int examiner)
        {
            string token = null;
            //获取请求
            var request = HttpContext.Current.Request;
            //响应
            ResultMsg<List<UserTask>> resultmsg = new ResultMsg<List<UserTask>>();
            HttpResponseMessage response = new HttpResponseMessage();
            PostgreSQL.OpenCon();//打开数据库
            //查询数据库 
            string str_select = "select * from tb_user_task where examiner= @examiner;";
            var para = new DbParameter[1];
            para[0] = PostgreSQL.NewParameter("@examiner", examiner);
            var qlist = PostgreSQL.ExecuteTListQuery<UserTask>(str_select, null, para);
            PostgreSQL.CloseCon();//关闭数据库
            if (qlist.Count <= 0)
            {
                //返回信息
                resultmsg.status = false;
                resultmsg.msg = "未获取任务消息!";
                resultmsg.data = null;
            }
            else
            {
                //返回信息
                resultmsg.status = true;
                resultmsg.msg = "成功获取任务消息！";
                resultmsg.data = qlist;  
            }
            //返回信息
            token = request.Headers["Token"];
            //添加响应头
            var resultObj = JsonConvert.SerializeObject(resultmsg, Formatting.Indented);
            response.Headers.Add("Token", token);
            response.Content = new StringContent(resultObj);
            return response;
        }
        /// <summary>
        /// 创建申请加入农场任务
        /// </summary>
        /// <param name="task">待处理通知任务</param>
        /// <returns></returns>
        [HttpPost]
        public object CreateTask(UserTask task)
        {
            string token = null;
            //获取请求
            var request = HttpContext.Current.Request;
            //响应
            ResultMsg<UserTask> resultmsg = new ResultMsg<UserTask>();
            HttpResponseMessage response = new HttpResponseMessage();
            if (task == null||task.description==null)//判断输入的数据格式是否正确
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
            PostgreSQL.OpenCon();//打开数据库
            //判断用户
            string strtoken = SunGolden.Encryption.DEncrypt.Decrypt(request.Headers["Token"]); //解密Token
            string str_mobile = strtoken.Substring(0, strtoken.IndexOf(","));
            //查询数据库
            string str1 = string.Format("select * from tb_user where \"mobile\" =\'{0}\'", str_mobile);
            var para3 = new DbParameter[1];
            para3[0] = PostgreSQL.NewParameter("@moblie", str_mobile);
            var quser = PostgreSQL.ExecuteTQuery<User>(str1, null, para3);
            if(quser.id==task.creator)//判断申请加入农场的用户与输入的用户是否是一样
            {
                //查询数据库,查看任务是否存在 
                string str_select = "select * from tb_user_task where  farm= @farm and creator= @creator and \"type\"=@type;";
                var para = new DbParameter[3];
                para[0] = PostgreSQL.NewParameter("@creator", task.creator);
                para[1] = PostgreSQL.NewParameter("@farm", task.farm);
                para[2] = PostgreSQL.NewParameter("@type", task.type);
                var qselcet = PostgreSQL.ExecuteTQuery<UserTask>(str_select, null, para);
                if (qselcet != null)//判断任务是否存在
                {
                    resultmsg.status = false;
                    resultmsg.msg = "任务类型已经存在！";
                    resultmsg.data = null;
                    token = request.Headers["Token"];
                }
                else
                {
                    //查询获取farm的农场主
                    string str = "select * from tb_user where farm= @farm and role= 0;";
                    var para1 = new DbParameter[1];
                    para1[0] = PostgreSQL.NewParameter("@farm", task.farm);
                    var qfarmer = PostgreSQL.ExecuteTQuery<User>(str, null, para1);
                    if (qfarmer == null)//判断农场是否存在
                    {
                        //返回信息
                        resultmsg.status = true;
                        resultmsg.msg = "申请加入的农场id,不存在！";
                        resultmsg.data = null;
                        token = request.Headers["Token"];
                    }
                    else
                    {
                        //查询数据库
                        string str_insert = "insert into tb_user_task(creator,\"type\",description,farm,createdate,examiner) values(@creator,@type,@description,@farm,@createdate,@examiner);";
                        var trans = PostgreSQL.BeginTransaction();
                        try
                        {
                            var para2 = new DbParameter[6];
                            para2[0] = PostgreSQL.NewParameter("@creator", task.creator);
                            para2[1] = PostgreSQL.NewParameter("@type", task.type);
                            para2[2] = PostgreSQL.NewParameter("@description", task.description);
                            para2[3] = PostgreSQL.NewParameter("@farm", task.farm);
                            para2[4] = PostgreSQL.NewParameter("@createdate", DateTime.Now.Date);
                            para2[5] = PostgreSQL.NewParameter("@examiner", qfarmer.id);
                            var num = PostgreSQL.ExecuteNoneQuery(str_insert, trans, para2);
                            PostgreSQL.CommitTransaction(trans);
                            //返回信息
                            resultmsg.status = true;
                            resultmsg.msg = task.description;
                            resultmsg.data = null;
                            token = request.Headers["Token"];
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
                }
            }
            else
            {
                //返回信息 
                resultmsg.status = true;
                resultmsg.msg = "输入的用户id,非申请加入农场用户id";
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
        /// 农场主处理任务 
        /// </summary>
        /// <param name="task">任务类</param>
        /// <returns></returns>
        [HttpPost]
        public object ProcessTask(UserTask task)
        {
            string token = null;
            //获取请求
            var request = HttpContext.Current.Request;
            //响应
            ResultMsg<UserTask> resultmsg = new ResultMsg<UserTask>();
            HttpResponseMessage response = new HttpResponseMessage();
            if (task == null)//判断输入的数据格式是否正确
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
            PostgreSQL.OpenCon();//打开数据库
            //查询数据库 
            string str_select = "select * from tb_user_task where  creator= @creator and \"type\"=@type ;";
            var para = new DbParameter[2];
            para[0] = PostgreSQL.NewParameter("@creator", task.creator);
            para[1] = PostgreSQL.NewParameter("@type", task.type);
            var qselect = PostgreSQL.ExecuteTableQuery(str_select, null, para);
            if (qselect != null&&qselect.Rows.Count==1)//判断任务是否存在，并且只有一个
            {
                if (Convert.ToInt16(qselect.Rows[0]["state"]) == 0)//判断处理状态
                {
                    var trans = PostgreSQL.BeginTransaction();
                    string str_update = "update tb_user_task set state=1,agree=@agree ,processdate=@processdate where farm= @farm and creator= @creator;";
                    try
                    {
                        var para1 = new DbParameter[4];
                        para1[0] = PostgreSQL.NewParameter("@creator", task.creator);
                        //para1[1] = PostgreSQL.NewParameter("@state", 1);
                        para1[1] = PostgreSQL.NewParameter("@agree", task.agree);
                        para1[2] = PostgreSQL.NewParameter("@farm", task.farm);
                        para1[3] = PostgreSQL.NewParameter("@processdate", DateTime.Now.Date);
                        var num = PostgreSQL.ExecuteNoneQuery(str_update, trans, para1);
                        PostgreSQL.CommitTransaction(trans);
                        if (task.agree == true)//同意加入农场
                        {
                            var trans1 = PostgreSQL.BeginTransaction();
                            string str_user = "update tb_user set farm= @farm where id=@id;";
                            var para2 = new DbParameter[2];
                            para2[0] = PostgreSQL.NewParameter("@farm", task.farm);
                            para2[1] = PostgreSQL.NewParameter("@id", task.creator);
                            var num1 = PostgreSQL.ExecuteNoneQuery(str_user, trans1, para2);
                            PostgreSQL.CommitTransaction(trans1);
                            //返回信息
                            resultmsg.status = true;
                            resultmsg.msg = "任务已处理，同意加入农场!";
                            resultmsg.data = null;
                            token = request.Headers["Token"];
                        }
                        else//不同意加入农场
                        {
                            //返回信息
                            resultmsg.status = false;
                            resultmsg.msg = "任务已处理，不同意加入农场!";
                            resultmsg.data = null;
                            token = request.Headers["Token"];
                        }
                        #region //查询
                        ////再次查询
                        //var para2 = new DbParameter[3];
                        //para2[0] = PostgreSQL.NewParameter("@creator", task.creator);
                        //para2[1] = PostgreSQL.NewParameter("@farm", task.farm);
                        //para2[2] = PostgreSQL.NewParameter("@type", task.type);
                        //var qlist = PostgreSQL.ExecuteTQuery<UserTask>(str_select, null, para2);

                        //返回信息
                        //resultmsg.status = true;
                        //resultmsg.msg = task.description;
                        //resultmsg.data = qlist;
                        //token = request.Headers["Token"];
                        #endregion
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
                    resultmsg.status = false;
                    resultmsg.msg = "任务类型已处理！";
                    resultmsg.data = null;
                    token = request.Headers["Token"];
                }                            
            }
            else
            {
                resultmsg.status = false;
                resultmsg.msg = "任务类型不存在！";
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




    }
}
