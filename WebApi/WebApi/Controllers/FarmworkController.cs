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
    /// 农情相关操作
    /// </summary>
    [AuthFilterOutside]
    public class FarmworkController : ApiController
    {
        /// <summary>
        /// 获取指定农情产品信息表
        /// </summary>
        /// <param name="farm">农场编号</param>
        /// <param name="type">农情类型编号</param>
        /// <returns></returns>
        [HttpGet]
        public object GetAgricultureinfo(int farm,int type)
        {
            string token = null;
            //获取请求
            var request = HttpContext.Current.Request;
            //响应
            ResultMsg<List<Field_rsi>> resultmsg = new ResultMsg<List<Field_rsi>>();
            HttpResponseMessage response = new HttpResponseMessage();
            PostgreSQL.OpenCon();//打开数据库
            //查询数据库
            string str = "select * from tb_field_rsi where farm=@farm and \"type\"=@type";
            var para = new DbParameter[2];
            para[0] = PostgreSQL.NewParameter("@farm", farm);
            para[1] = PostgreSQL.NewParameter("@type", type);
            var qfarm = PostgreSQL.ExecuteTListQuery<Field_rsi>(str, null, para);
            PostgreSQL.CloseCon();//关闭数据库
            if (qfarm.Count <=0)
            {
                //返回信息
                resultmsg.status = false;
                resultmsg.msg = "没有此产品类型的农情产品信息!";
                resultmsg.data = null;
            }
            else
            {
                resultmsg.status = true;
                resultmsg.msg = "成功获取指定农情产品信息表！";
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
        /// 获取农情类型表
        /// </summary>
        /// <returns>农情列表</returns>
        [HttpGet]
        public object GetRsiType()
        {
            string token = null;
            //获取请求
            var request = HttpContext.Current.Request;
            //响应
            ResultMsg<List<RSI_type>> resultmsg = new ResultMsg<List<RSI_type>>();
            HttpResponseMessage response = new HttpResponseMessage();
            PostgreSQL.OpenCon();//打开数据库
            //查询数据库
            string str_select = "select * from dic_rsi_type ";
            var para = new DbParameter[0];
            var qRsiType = PostgreSQL.ExecuteTListQuery<RSI_type>(str_select, null, para);
            PostgreSQL.CloseCon();  //关闭数据库    
            //返回信息
            resultmsg.status = true;
            resultmsg.msg = "成功获取农情类型列表！";
            resultmsg.data = qRsiType;
            token = request.Headers["Token"];
            //添加响应头
            var resultObj = JsonConvert.SerializeObject(resultmsg, Formatting.Indented);
            response.Headers.Add("Token", token);
            response.Content = new StringContent(resultObj);
            return response;
        }
        /// <summary>
        /// 获取指定地块的实况信息
        /// </summary>
        /// <param name="field">地块编号</param>
        /// <returns>地块实况信息列表</returns>
        [HttpGet]
        public object GetFieldLive(int field)
        {
            string token = null;
            //获取请求
            var request = HttpContext.Current.Request;
            //响应
            ResultMsg<Field_live> resultmsg = new ResultMsg<Field_live>();
            HttpResponseMessage response = new HttpResponseMessage();
            PostgreSQL.OpenCon();//打开数据库
            //查询数据库
            string str_select = "select * from tb_field_live where  field = @field";
            var para = new DbParameter[1];
            para[0] = PostgreSQL.NewParameter("@field", field);
            var qlive = PostgreSQL.ExecuteTQuery<Field_live>(str_select, null, para);
            PostgreSQL.CloseCon();//关闭数据库
            if (qlive == null)
            {
                //返回信息
                resultmsg.status = false;
                resultmsg.msg = "此地块不存在,未获取此地块实况信息!";
                resultmsg.data = null;
            }
           else
            {
                resultmsg.status = true;
                resultmsg.msg = "成功获取地块实况信息";
                resultmsg.data = qlive;
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
        /// 添加指定地块的实况信息
        /// </summary>
        /// <param name="live">地块实况信息类</param>
        /// <returns></returns>
        [HttpPost]
        public object AddFieldLive(Field_live live)
        {
            string token = null;
            //获取请求
            var request = HttpContext.Current.Request;
            //响应
            ResultMsg<Field_live> resultmsg = new ResultMsg<Field_live>();
            HttpResponseMessage response = new HttpResponseMessage();
            if (live == null)//判断输入的数据格式是否正确
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
            var para1 = new DbParameter[1];
            para1[0] = PostgreSQL.NewParameter("@moblie", str_mobile);
            var quser = PostgreSQL.ExecuteTQuery<User>(str1, null, para1);
            if(quser.id != live.collector)
            {
                //返回信息
                resultmsg.status = false;
                resultmsg.msg = "输入的采集人不是登陆用户！";
                resultmsg.data = null;
                token = request.Headers["Token"];
                //添加响应头
                var resultObj1 = JsonConvert.SerializeObject(resultmsg, Formatting.Indented);
                response.Headers.Add("Token", token);
                response.Content = new StringContent(resultObj1);
                return response;
            }
            //查询数据库，判断地块是否存在
            string str_select = "select * from tb_field_live where field = @field";
            var para = new DbParameter[1];
            para[0] = PostgreSQL.NewParameter("@field", live.field);
            var qLive = PostgreSQL.ExecuteTQuery<Field_live>(str_select, null, para);
            if (qLive != null)
            {
                //返回信息
                resultmsg.status = false;
                resultmsg.msg = "地块实况已经存在，是否要重新添加！";
                resultmsg.data = null;
                token = request.Headers["Token"];
            }
            else
            {
                //数据检查是否超出界限
                DataAuth auth = new DataAuth();
                if (auth.field(live.field)&&auth.growthtype(live.growth)&&auth.moisturetype(live.moisture)&&auth.pesttype(live.pest)&&auth.diseasetype(live.disease))
                {
                    string str_insert = "insert into tb_field_live(field,growth,moisture,disease,pest,collector,collect_date,gps,picture) values(@field,@growth,@moisture,@disease,@pest,@collector,@collect_date,@gps,@picture) ";
                    var trans = PostgreSQL.BeginTransaction();
                    try
                    {
                        var para2 = new DbParameter[9];
                        para2[0] = PostgreSQL.NewParameter("@field", live.field);
                        para2[1] = PostgreSQL.NewParameter("@growth", live.growth);
                        para2[2] = PostgreSQL.NewParameter("@moisture", live.moisture);
                        para2[3] = PostgreSQL.NewParameter("@disease", live.disease);
                        para2[4] = PostgreSQL.NewParameter("@pest", live.pest);
                        para2[5] = PostgreSQL.NewParameter("@collector", live.collector);
                        para2[6] = PostgreSQL.NewParameter("@collect_date", DateTime.Now.Date);
                        para2[7] = PostgreSQL.NewParameter("@gps", live.gps);
                        para2[8] = PostgreSQL.NewParameter("@picture", live.picture);
                        var list = PostgreSQL.ExecuteNoneQuery(str_insert, trans, para2);
                        PostgreSQL.CommitTransaction(trans);
                        //返回信息
                        resultmsg.status = true;
                        resultmsg.msg = "添加指定地块的实况信息成功!";
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
                else
                {
                    //返回信息
                    resultmsg.status = false;
                    resultmsg.msg = "输入的长势类型/土壤湿度类型/病害类型/虫害类型数据，其中有超出界限的数据，请检查！";
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
    }
}
