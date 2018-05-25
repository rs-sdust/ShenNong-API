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
    /// <summary>
    /// 农场相关操作
    /// </summary>
    [AuthFilterOutside]
    public class FarmController : ApiController
    {
        /// <summary>
        /// 根据农场地址获取农场列表
        /// </summary>
        /// <param name="address">农场地址</param>
        /// <returns></returns>
        [HttpGet]
        public object GetFarms(string address)
        {
            string token = null;
            //获取请求
            var request = HttpContext.Current.Request;
            //响应
            ResultMsg<List<Farm>> resultmsg = new ResultMsg<List<Farm>>();
            HttpResponseMessage response = new HttpResponseMessage();
            PostgreSQL.OpenCon();//打开数据库
            //查询数据库
            string str = "select * from tb_farm where address = @address";
            var para = new DbParameter[1];
            para[0] = PostgreSQL.NewParameter("@address", address);
            var qfarm = PostgreSQL.ExecuteTListQuery<Farm>(str, null, para);
            //var qfarm = PostgreSQL.ExecuteTableQuery(str, null, para);
            PostgreSQL.CloseCon();//关闭数据库
            if (qfarm.Count <= 0)//判断是否含有农场列表
            {
                //返回信息
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
            //返回信息           
            token = request.Headers["Token"];
            //添加响应头
            var resultObj = JsonConvert.SerializeObject(resultmsg, Formatting.Indented);
            response.Headers.Add("Token", token);
            response.Content = new StringContent(resultObj);
            return response;
        }
        /// <summary>
        /// 创建农场
        /// </summary>
        /// <param name="farm">农场类</param>
        /// <returns></returns>
        [HttpPost]
        public object CreatFarm(Farm farm)
        {
            string token = null;
            //获取请求
            var request = HttpContext.Current.Request;
            token = request.Headers["Token"];
            DataAuth auth = new DataAuth();
            var role = auth.RoleType(token);
            //响应
            ResultMsg<Farm> resultmsg = new ResultMsg<Farm>();
            HttpResponseMessage response = new HttpResponseMessage();
            if (farm == null)//判断输入的数据格式是否正确
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
            if (role == "0")//"农场主"判断是否是农场主,如果用户是农场主，则创建农场
            {
                var para = new DbParameter[2];
                string str_select = "select * from tb_farm where name=@name and address=@address";
                para[0] = PostgreSQL.NewParameter("@name", farm.name);
                para[1] = PostgreSQL.NewParameter("@address", farm.address);
                var qfarm = PostgreSQL.ExecuteTQuery<Farm>(str_select, null, para);
                if (qfarm != null)//判断此地址的农场名是否存在
                {
                    resultmsg.status = false;
                    resultmsg.msg = "农场名已经存在，请重新命名！";
                    resultmsg.data = qfarm;
                    token = request.Headers["Token"];
                }
                else
                {
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
                        //查询
                        var para2 = new DbParameter[2];
                        para2[0] = PostgreSQL.NewParameter("@name", farm.name);
                        para2[1] = PostgreSQL.NewParameter("@address", farm.address);
                        var qfarm1 = PostgreSQL.ExecuteTQuery<Farm>(str_select, null, para2);
                        //返回信息
                        resultmsg.status = true;
                        resultmsg.msg = "成功创建农场!";
                        resultmsg.data = qfarm1;
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
            else
            {
                resultmsg.status = false;
                resultmsg.msg = "不是农场主无法创建农场!";
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
        /// 获取农场简报
        /// </summary>
        /// <param name="farmid">农场id</param>
        /// <returns></returns>
        [HttpGet]
        public object GetFarmbrief(string farmid)
        {

            string response = null;
            return response;
        }
        /// <summary>
        /// 根据农场名称查询农场信息
        /// </summary>
        /// <param name="name">农场名称</param>
        /// <returns></returns>
        [HttpGet]
        public object QueryFarm(string name)
        {
            string token = null;
            //获取请求
            var request = HttpContext.Current.Request;
            //响应
            ResultMsg<List<Farm>> resultmsg = new ResultMsg<List<Farm>>();
            HttpResponseMessage response = new HttpResponseMessage();
            PostgreSQL.OpenCon();//打开数据库
            //查询数据库
            string str = string.Format("select * from tb_farm where name like \'%{0}%\'", name);
            //string str = "select * from tb_farm where name like \'%@name%\'";
            var para = new DbParameter[1];
            para[0] = PostgreSQL.NewParameter("@name", name);
            var qfarm = PostgreSQL.ExecuteTListQuery<Farm>(str, null, para);
            PostgreSQL.CloseCon();//关闭数据库
            if (qfarm.Count <= 0)//判断此农场是否存在
            {
                //返回信息
                resultmsg.status = false;
                resultmsg.msg = "此农场不存在!";
                resultmsg.data = null;
            }
            else
            {
                resultmsg.status = true;
                resultmsg.msg = "成功获取此农场信息!";
                resultmsg.data = qfarm;
            }
            //返回token信息           
            token = request.Headers["Token"];
            //添加响应头
            var resultObj = JsonConvert.SerializeObject(resultmsg, Formatting.Indented);
            response.Headers.Add("Token", token);
            response.Content = new StringContent(resultObj);
            return response;
        }
        /// <summary>
        /// 更新指定农场信息
        /// </summary>
        /// <param name="farm">农场类</param>
        /// <returns></returns>
        [HttpPost]
        public object UpdateFarm(Farm farm)
        {
            string token = null;
            //获取请求
            var request = HttpContext.Current.Request;
            //声明响应
            ResultMsg<Farm> resultmsg = new ResultMsg<Farm>();
            HttpResponseMessage response = new HttpResponseMessage();
            if (farm == null)//判断输入的数据格式是否正确
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
            string str_select = "select * from tb_farm where id = @id;";//name = @name ";
            var para = new DbParameter[1];
            para[0] = PostgreSQL.NewParameter("@id", farm.id);
            var qFarm = PostgreSQL.ExecuteTQuery<Farm>(str_select, null, para);
            if (qFarm == null)//判断此id的农场是否存在
            {
                resultmsg.status = false;
                resultmsg.msg = "农场不存在!";
                resultmsg.data = null;
                token = request.Headers["Token"];
            }
            else
            {
                string str_update = "update tb_farm set \"name\" = @name,address = @address where id=@id";
                var trans = PostgreSQL.BeginTransaction();
                try
                {
                    var para1 = new DbParameter[3];
                    para1[0] = PostgreSQL.NewParameter("@id", farm.id);
                    para1[1] = PostgreSQL.NewParameter("@name", farm.name);
                    para1[2] = PostgreSQL.NewParameter("@address", farm.address);
                    var num = PostgreSQL.ExecuteNoneQuery(str_update, trans, para1);
                    PostgreSQL.CommitTransaction(trans);

                    //返回信息
                    resultmsg.status = true;
                    resultmsg.msg = "成功更新农场信息!";
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
            PostgreSQL.CloseCon();//关闭数据库
            //添加响应头
            var resultObj = JsonConvert.SerializeObject(resultmsg, Formatting.Indented);
            response.Headers.Add("Token", token);
            response.Content = new StringContent(resultObj);
            return response;
        }
        /// <summary>
        /// 获取指定农场信息
        /// </summary>
        /// <param name="id">农场主键</param>
        /// <returns></returns>
        [HttpGet]
        public object GetFarm(int id)
        {
            string token = null;
            //获取请求
            var request = HttpContext.Current.Request;
            //响应
            ResultMsg<Farm> resultmsg = new ResultMsg<Farm>();
            HttpResponseMessage response = new HttpResponseMessage();
            PostgreSQL.OpenCon();//打开数据库 
            //查询数据库
            string str = "select * from tb_farm where id = @id";
            var para = new DbParameter[1];
            para[0] = PostgreSQL.NewParameter("@id", id);
            var qfarm = PostgreSQL.ExecuteTQuery<Farm>(str, null, para);
            PostgreSQL.CloseCon();//关闭数据库
            if (qfarm==null)
            {
                //返回信息
                resultmsg.status = false;
                resultmsg.msg = "此农场不存在!";
                resultmsg.data = null;              
            }
            else
            {
                resultmsg.status = true;
                resultmsg.msg = "成功获取此农场信息!";
                resultmsg.data = qfarm;
            }
            //返回信息           
            token = request.Headers["Token"];
            //添加响应头
            var resultObj = JsonConvert.SerializeObject(resultmsg, Formatting.Indented);
            response.Headers.Add("Token", token);
            response.Content = new StringContent(resultObj);
            return response;
        }
        
    }
}
