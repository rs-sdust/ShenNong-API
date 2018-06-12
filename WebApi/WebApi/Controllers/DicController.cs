using Newtonsoft.Json;
using SunGolden.DBUtils;
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
    /// 字典表信息接口
    /// </summary>
    [AuthFilterOutside]
    public class DicController : ApiController
    {
        /// <summary>
        /// 获取作物类型列表
        /// </summary>
        /// <returns>作物列表</returns>
        [HttpGet]
        public object GetCrops()
        {
            string token = null;
            //获取请求
            var request = HttpContext.Current.Request;
            //声明响应
            ResultMsg<List<CropTypes>> resultmsg = new ResultMsg<List<CropTypes>>();
            HttpResponseMessage response = new HttpResponseMessage();
            //查询数据库
            string str_select = "select * from dic_crop_type";
            PostgreSQL.OpenCon(); //打开数据库    
            var para = new DbParameter[0];
            var q_select = PostgreSQL.ExecuteTListQuery<CropTypes>(str_select, null, para);
            PostgreSQL.CloseCon();//关闭数据库
            if (q_select.Count <= 0)
            {
                //返回信息
                resultmsg.status = false;
                resultmsg.msg = "未获取作物列表!";
                resultmsg.data = null;
            }
            else
            {
                //返回信息
                resultmsg.status = true;
                resultmsg.msg = "成功获取作物列表!";
                resultmsg.data = q_select;
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
        /// 获取作物病害列表
        /// </summary>
        /// <returns>作物病害列表</returns>
        public object GetDiseaseType()
        {
            string token = null;
            //获取请求
            var request = HttpContext.Current.Request;
            //声明响应
            ResultMsg<List<DiseaseTypes>> resultmsg = new ResultMsg<List<DiseaseTypes>>();
            HttpResponseMessage response = new HttpResponseMessage();
            //查询数据库
            string str_select = "select * from dic_disease_type";
            PostgreSQL.OpenCon(); //打开数据库    
            var para = new DbParameter[0];
            var q_select = PostgreSQL.ExecuteTListQuery<DiseaseTypes>(str_select, null, para);
            PostgreSQL.CloseCon();//关闭数据库
            if (q_select.Count <= 0)
            {
                //返回信息
                resultmsg.status = false;
                resultmsg.msg = "未获取作物病害列表!";
                resultmsg.data = null;
            }
            else
            {
                //返回信息
                resultmsg.status = true;
                resultmsg.msg = "成功获取作物病害列表!";
                resultmsg.data = q_select;
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
        /// 获取作物长势列表
        /// </summary>
        /// <returns>作物长势列表</returns>
        public object GetGrowthType()
        {
            string token = null;
            //获取请求
            var request = HttpContext.Current.Request;
            //声明响应
            ResultMsg<List<GrowthTypes>> resultmsg = new ResultMsg<List<GrowthTypes>>();
            HttpResponseMessage response = new HttpResponseMessage();
            //查询数据库
            string str_select = "select * from dic_growth_type";
            PostgreSQL.OpenCon(); //打开数据库    
            var para = new DbParameter[0];
            var q_select = PostgreSQL.ExecuteTListQuery<GrowthTypes>(str_select, null, para);
            PostgreSQL.CloseCon();//关闭数据库
            if (q_select.Count <= 0)
            {
                //返回信息
                resultmsg.status = false;
                resultmsg.msg = "未获取作物长势列表!";
                resultmsg.data = null;
            }
            else
            {
                //返回信息
                resultmsg.status = true;
                resultmsg.msg = "成功获取作物长势列表!";
                resultmsg.data = q_select;
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
        /// 获取土壤湿度列表
        /// </summary>
        /// <returns>土壤湿度列表</returns>
        public object GetMoistureType()
        {
            string token = null;
            //获取请求
            var request = HttpContext.Current.Request;
            //声明响应
            ResultMsg<List<MoistureTypes>> resultmsg = new ResultMsg<List<MoistureTypes>>();
            HttpResponseMessage response = new HttpResponseMessage();
            //查询数据库
            string str_select = "select * from dic_moisture_type";
            PostgreSQL.OpenCon(); //打开数据库    
            var para = new DbParameter[0];
            var q_select = PostgreSQL.ExecuteTListQuery<MoistureTypes>(str_select, null, para);
            PostgreSQL.CloseCon();//关闭数据库
            if (q_select.Count <= 0)
            {
                //返回信息
                resultmsg.status = false;
                resultmsg.msg = "未获取土壤湿度列表!";
                resultmsg.data = null;
            }
            else
            {
                //返回信息
                resultmsg.status = true;
                resultmsg.msg = "成功获取土壤湿度列表!";
                resultmsg.data = q_select;
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
        /// 获取资讯类型列表
        /// </summary>
        /// <returns>资讯类型列表</returns>
        public object GetNewsType()
        {
            string token = null;
            //获取请求
            var request = HttpContext.Current.Request;
            //声明响应
            ResultMsg<List<NewsTypes>> resultmsg = new ResultMsg<List<NewsTypes>>();
            HttpResponseMessage response = new HttpResponseMessage();
            //查询数据库
            string str_select = "select * from dic_news_type";
            PostgreSQL.OpenCon(); //打开数据库    
            var para = new DbParameter[0];
            var q_select = PostgreSQL.ExecuteTListQuery<NewsTypes>(str_select, null, para);
            PostgreSQL.CloseCon();//关闭数据库
            if (q_select.Count <= 0)
            {
                //返回信息
                resultmsg.status = false;
                resultmsg.msg = "未获取资讯类型列表!";
                resultmsg.data = null;
            }
            else
            {
                //返回信息
                resultmsg.status = true;
                resultmsg.msg = "成功获取资讯类型列表!";
                resultmsg.data = q_select;
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
        /// 获取虫害类型列表
        /// </summary>
        /// <returns>虫害类型列表</returns>
        public object GetPestType()
        {
            string token = null;
            //获取请求
            var request = HttpContext.Current.Request;
            //声明响应
            ResultMsg<List<PestTypes>> resultmsg = new ResultMsg<List<PestTypes>>();
            HttpResponseMessage response = new HttpResponseMessage();
            //查询数据库
            string str_select = "select * from dic_pest_type";
            PostgreSQL.OpenCon(); //打开数据库    
            var para = new DbParameter[0];
            var q_select = PostgreSQL.ExecuteTListQuery<PestTypes>(str_select, null, para);
            PostgreSQL.CloseCon();//关闭数据库
            if (q_select.Count <= 0)
            {
                //返回信息
                resultmsg.status = false;
                resultmsg.msg = "未获取虫害类型列表!";
                resultmsg.data = null;
            }
            else
            {
                //返回信息
                resultmsg.status = true;
                resultmsg.msg = "成功获取虫害类型列表!";
                resultmsg.data = q_select;
            }
            //返回token信息           
            token = request.Headers["Token"];
            //添加响应头
            var resultObj = JsonConvert.SerializeObject(resultmsg, Formatting.Indented);
            response.Headers.Add("Token", token);
            response.Content = new StringContent(resultObj);
            return response;
        }
        ///// <summary>
        ///// 获取物候类型列表
        ///// </summary>
        ///// <returns>物候类型列表</returns>
        //public object GetPhenType()
        //{
        //    string token = null;
        //    //获取请求
        //    var request = HttpContext.Current.Request;
        //    //声明响应
        //    ResultMsg<List<PhenTypes>> resultmsg = new ResultMsg<List<PhenTypes>>();
        //    HttpResponseMessage response = new HttpResponseMessage();
        //    //查询数据库
        //    string str_select = "select * from dic_phen_type";
        //    PostgreSQL.OpenCon(); //打开数据库    
        //    var para = new DbParameter[0];
        //    var q_select = PostgreSQL.ExecuteTListQuery<PhenTypes>(str_select, null, para);
        //    PostgreSQL.CloseCon();//关闭数据库
        //    if (q_select.Count <= 0)
        //    {
        //        //返回信息
        //        resultmsg.status = false;
        //        resultmsg.msg = "未获取物候类型列表!";
        //        resultmsg.data = null;
        //    }
        //    else
        //    {
        //        //返回信息
        //        resultmsg.status = true;
        //        resultmsg.msg = "成功获取物候类型列表!";
        //        resultmsg.data = q_select;
        //    }
        //    //返回token信息           
        //    token = request.Headers["Token"];
        //    //添加响应头
        //    var resultObj = JsonConvert.SerializeObject(resultmsg, Formatting.Indented);
        //    response.Headers.Add("Token", token);
        //    response.Content = new StringContent(resultObj);
        //    return response;
        //}
        /// <summary>
        /// 获取产品等级列表
        /// </summary>
        /// <returns>产品等级列表</returns>
        public object GetRsiGrade()
        {
            string token = null;
            //获取请求
            var request = HttpContext.Current.Request;
            //声明响应
            ResultMsg<List<RsiGrade>> resultmsg = new ResultMsg<List<RsiGrade>>();
            HttpResponseMessage response = new HttpResponseMessage();
            //查询数据库
            string str_select = "select * from dic_rsi_grade";
            PostgreSQL.OpenCon(); //打开数据库    
            var para = new DbParameter[0];
            var q_select = PostgreSQL.ExecuteTListQuery<RsiGrade>(str_select, null, para);
            PostgreSQL.CloseCon();//关闭数据库
            if (q_select.Count <= 0)
            {
                //返回信息
                resultmsg.status = false;
                resultmsg.msg = "未获取产品等级列表!";
                resultmsg.data = null;
            }
            else
            {
                //返回信息
                resultmsg.status = true;
                resultmsg.msg = "成功获取产品等级列表!";
                resultmsg.data = q_select;
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
            ResultMsg<List<RsiTypes>> resultmsg = new ResultMsg<List<RsiTypes>>();
            HttpResponseMessage response = new HttpResponseMessage();
            PostgreSQL.OpenCon();//打开数据库
            //查询数据库
            string str_select = "select * from dic_rsi_type ";
            var para = new DbParameter[0];
            var q_select = PostgreSQL.ExecuteTListQuery<RsiTypes>(str_select, null, para);
            PostgreSQL.CloseCon();  //关闭数据库 
            if (q_select.Count <= 0)
            {
                //返回信息
                resultmsg.status = false;
                resultmsg.msg = "未获取农情类型列表!";
                resultmsg.data = null;
            }
            else
            {
                //返回信息
                resultmsg.status = true;
                resultmsg.msg = "成功获取农情类型列表！";
                resultmsg.data = q_select;
            }
            //返回token信息           
            token = request.Headers["Token"];
            //添加响应头
            var resultObj = JsonConvert.SerializeObject(resultmsg, Formatting.Indented);
            response.Headers.Add("Token", token);
            response.Content = new StringContent(resultObj);
            return response;
        }
       
    }
}
