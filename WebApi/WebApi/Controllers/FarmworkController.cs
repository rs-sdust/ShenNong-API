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
    public class FarmworkController : ApiController
    {

        //获取市场行情信息
        [HttpGet]
        [AuthFilterOutside]
        public object GetMarket(int crop_type)
        {
            string token = null;

            //获取请求
            var request = HttpContext.Current.Request;
            //响应
            UserResponse<Market> respons = new UserResponse<Market>();
            HttpResponseMessage response = new HttpResponseMessage();
            //查询数据库
            string str = "select * from tb_market where crop_type=@crop_type";
            PostgreSQL.OpenCon();
            var para = new DbParameter[1];
            para[0] = PostgreSQL.NewParameter("@crop_type", crop_type);
           
            var qfarm = PostgreSQL.ExecuteTQuery<Market>(str, null, para);
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

        //获取指定农情产品信息表
        [HttpGet]
        [AuthFilterOutside]
        public object GetAgricultureinfo(int farm,int type)
        {
            string token = null;

            //获取请求
            var request = HttpContext.Current.Request;
            //响应
            UserResponse<Field_rsi> respons = new UserResponse<Field_rsi>();
            HttpResponseMessage response = new HttpResponseMessage();
            //查询数据库
            string str = "select * from tb_filed_rsi where farm=@farm,type=@type";
            PostgreSQL.OpenCon();
            var para = new DbParameter[2];
            para[0] = PostgreSQL.NewParameter("@farm", farm);
            para[1] = PostgreSQL.NewParameter("@type", type);
            var qfarm = PostgreSQL.ExecuteTQuery<Field_rsi>(str, null, para);
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
        //获取农情类型表
        [HttpGet]
        [AuthFilterOutside]
        public object GetRsiType()
        {
            string token = null;
            //获取请求
            var request = HttpContext.Current.Request;

            //查询数据库
            string str_select = "select * from dic_rsi_type ";
            PostgreSQL.OpenCon();

            var para = new DbParameter[0];
            var qCrop = PostgreSQL.ExecuteTListQuery<RSI_type>(str_select, null, para);
            PostgreSQL.CloseCon();
            //响应
            UserResponse<List<RSI_type>> respons = new UserResponse<List<RSI_type>>();
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
        //获取指定地块的实况信息
        [HttpGet]
        [AuthFilterOutside]
        public object GetFieldLive(Field_live live )
        {
            string token = null;
            //获取请求
            var request = HttpContext.Current.Request;

            //查询数据库
            string str_select = "select * from tb_field_live where field = @field ";
            PostgreSQL.OpenCon();

            var para = new DbParameter[1];
            para[0] = PostgreSQL.NewParameter("@field", live.field);
            var qlive = PostgreSQL.ExecuteTListQuery<Field_live>(str_select, null, para);
            PostgreSQL.CloseCon();
            //响应
            UserResponse<List<Field_live>> respons = new UserResponse<List<Field_live>>();
            HttpResponseMessage response = new HttpResponseMessage();
            respons.msg = "0";
            respons.data = qlive;
            token = request.Headers["Token"];
            //添加响应头
            var resultObj = JsonConvert.SerializeObject(respons, Formatting.Indented);
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
            UserResponse<Field_live> respons = new UserResponse<Field_live>();
            HttpResponseMessage response = new HttpResponseMessage();
            //查询数据库
            string str_select = "select * from tb_field_live where field = @field ";
            PostgreSQL.OpenCon();

            var para = new DbParameter[1];
            para[0] = PostgreSQL.NewParameter("@field", live.field);
            var qLive = PostgreSQL.ExecuteTQuery<Field_live>(str_select, null, para);
            if(qLive!=null)
            {
                //???????
            }
            else
            {
                string str_insert = "insert into tb_field_live(field,growth,moisture,disease,pest,collector,collect_date,gps,picture) values(@field,@growth,@moisture,@disease,@pest,@collector,@collect_date,@gps,@picture) ";
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

                    var list = PostgreSQL.ExecuteNoneQuery(str_insert, trans, para1);
                    PostgreSQL.CommitTransaction(trans);
                }
                catch(Exception ex)
                {
                    PostgreSQL.RollbackTransaction(trans);
                }
                finally
                {
                    PostgreSQL.CloseCon();
                }
            }

           //响应内容
            respons.msg = "0";
            respons.data = qLive;
            token = request.Headers["Token"];
            //添加响应头
            var resultObj = JsonConvert.SerializeObject(respons, Formatting.Indented);
            response.Headers.Add("Token", token);
            response.Content = new StringContent(resultObj);
            return response;
        }
    }
}
