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
    [AuthFilterOutside]
    public class FarmworkController : ApiController
    {

        //获取市场行情信息
        [HttpGet]
        //[AuthFilterOutside]
        public object GetMarket(Market market)
        {
            string token = null;

            //获取请求
            var request = HttpContext.Current.Request;
            //响应
            ResultMsg<List<Market>> resultmsg = new ResultMsg<List<Market>>();
            HttpResponseMessage response = new HttpResponseMessage();
            //查询数据库
            string str = "select * from tb_market where crop_type = @crop_type";
            PostgreSQL.OpenCon();
            var para = new DbParameter[1];
            para[0] = PostgreSQL.NewParameter("@crop_type", market.crop_type);
           
            var qfarm = PostgreSQL.ExecuteTListQuery<Market>(str, null, para);
            PostgreSQL.CloseCon();
            //响应内容
            resultmsg.status = true;
            resultmsg.msg = "成功获取市场行情信息!";
            resultmsg.data = qfarm;
            token = request.Headers["Token"];

            //添加响应头
            var resultObj = JsonConvert.SerializeObject(resultmsg, Formatting.Indented);
            response.Headers.Add("Token", token);
            response.Content = new StringContent(resultObj);
            return response;
        }

        //获取指定农情产品信息表
        [HttpGet]
        //[AuthFilterOutside]
        public object GetAgricultureinfo(Field_rsi  rsi)
        {
            string token = null;

            //获取请求
            var request = HttpContext.Current.Request;
            //响应
            ResultMsg<List<Field_rsi>> resultmsg = new ResultMsg<List<Field_rsi>>();
            HttpResponseMessage response = new HttpResponseMessage();
            //查询数据库
            string str = "select * from tb_field_rsi where farm=@farm and \"type\"=@type";
            PostgreSQL.OpenCon();
            var para = new DbParameter[2];
            para[0] = PostgreSQL.NewParameter("@farm", rsi.farm);
            para[1] = PostgreSQL.NewParameter("@type", rsi.type);
            var qfarm = PostgreSQL.ExecuteTListQuery<Field_rsi>(str, null, para);
            PostgreSQL.CloseCon();
            //响应内容
            resultmsg.status = true;
            resultmsg.msg = "成功获取指定农情产品信息表！";
            resultmsg.data = qfarm;
            token = request.Headers["Token"];

            //添加响应头
            var resultObj = JsonConvert.SerializeObject(resultmsg, Formatting.Indented);
            response.Headers.Add("Token", token);
            response.Content = new StringContent(resultObj);
            return response;
        }
        //获取农情类型表
        [HttpGet]
        //[AuthFilterOutside]
        public object GetRsiType()
        {
            string token = null;
            //获取请求
            var request = HttpContext.Current.Request;

            //查询数据库
            string str_select = "select * from dic_rsi_type ";
            PostgreSQL.OpenCon();

            var para = new DbParameter[0];
            var qRsiType = PostgreSQL.ExecuteTListQuery<RSI_type>(str_select, null, para);
            PostgreSQL.CloseCon();
            //响应
            ResultMsg<List<RSI_type>> resultmsg = new ResultMsg<List<RSI_type>>();
            HttpResponseMessage response = new HttpResponseMessage();
            //响应内容
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
        //获取指定地块的实况信息
        [HttpGet]
        //[AuthFilterOutside]
        public object GetFieldLive(Field_live live )
        {
            string token = null;
            //获取请求
            var request = HttpContext.Current.Request;

            //查询数据库
            string str_select = "select * from tb_field_live where where id=@id;";// field = @field";
            PostgreSQL.OpenCon();

            var para = new DbParameter[1];
            para[0] = PostgreSQL.NewParameter("@id", live.id);
            var qlive = PostgreSQL.ExecuteTQuery<Field_live>(str_select, null, para);
            PostgreSQL.CloseCon();
            //响应
            ResultMsg<Field_live> resultmsg = new ResultMsg<Field_live>();
            HttpResponseMessage response = new HttpResponseMessage();
            //响应内容
            resultmsg.status = true;
            resultmsg.msg = "成功获取地块实况信息";
            resultmsg.data = qlive;
            token = request.Headers["Token"];
            //添加响应头
            var resultObj = JsonConvert.SerializeObject(resultmsg, Formatting.Indented);
            response.Headers.Add("Token", token);
            response.Content = new StringContent(resultObj);
            return response;
        }
        //添加指定地块的实况信息
        [HttpPost]
        public object AddFieldLive(Field_live live)
        {
            string token = null;
            //获取请求
            var request = HttpContext.Current.Request;
            //响应
            ResultMsg<Field_live> resultmsg = new ResultMsg<Field_live>();
            HttpResponseMessage response = new HttpResponseMessage();
            //查询数据库
            string str_select = "select * from tb_field_live where id=@id;";// field = @field";
            PostgreSQL.OpenCon();
            var para = new DbParameter[1];
            para[0] = PostgreSQL.NewParameter("@id", live.id);
            var qLive = PostgreSQL.ExecuteTQuery<Field_live>(str_select, null, para);
            if(qLive!=null)
            {
                //响应内容
                resultmsg.status = false;
                resultmsg.msg = "地块实况已经存在，是否要重新添加！";
                resultmsg.data = null;
                token = request.Headers["Token"];
            }
            else
            {
                string str = "insert into tb_field_live(field,growth,moisture,disease,pest,collector,collect_date,gps,picture) values(@field,@growth,@moisture,@disease,@pest,@collector,@collect_date,@gps,@picture) ";
                var trans = PostgreSQL.BeginTransaction();
                try
                {
                    var para1 = new DbParameter[9];
                    para1[0] = PostgreSQL.NewParameter("@field", live.field);
                    para1[1] = PostgreSQL.NewParameter("@growth", live.growth);
                    para1[2] = PostgreSQL.NewParameter("@moisture", live.moisture);
                    para1[3] = PostgreSQL.NewParameter("@disease", live.disease);
                    para1[4] = PostgreSQL.NewParameter("@pest", live.pest);
                    para1[5] = PostgreSQL.NewParameter("@collector", live.collector);
                    para1[6] = PostgreSQL.NewParameter("@collect_date", live.collect_date);
                    para1[7] = PostgreSQL.NewParameter("@gps", live.gps);
                    para1[8] = PostgreSQL.NewParameter("@picture", live.picture);

                    var list = PostgreSQL.ExecuteNoneQuery(str, trans, para1);
                    PostgreSQL.CommitTransaction(trans);
                  
                    //响应内容
                    resultmsg.status = true;
                    resultmsg.msg = "添加指定地块的实况信息成功!";
                    resultmsg.data = null;
                    token = request.Headers["Token"];
                }
                catch(Exception ex)
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
    }
}
