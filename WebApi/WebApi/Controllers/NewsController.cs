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
    ///  发现新闻资讯和市场相关接口
    /// </summary>
    [AuthFilterOutside]
    public class NewsController : ApiController
    {
        /// <summary>
        /// 获取指定类型的新闻资讯信息
        /// </summary>
        /// <param name="newstype">资讯类型</param>
        /// <returns></returns>
        public object GetNewsType(int newstype)
        {
            string token = null;
            //获取请求
            var request = HttpContext.Current.Request;
            //响应
            ResultMsg<List<News>> resultmsg = new ResultMsg<List<News>>();
            HttpResponseMessage response = new HttpResponseMessage();
            //查询数据库 
            string str = "select * from tb_news where news_type = @news_type";
            PostgreSQL.OpenCon();//打开数据库
            var para = new DbParameter[1];
            para[0] = PostgreSQL.NewParameter("@news_type", newstype);
            var qnews = PostgreSQL.ExecuteTListQuery<News>(str, null, para);
            PostgreSQL.CloseCon();//关闭数据库
            if (qnews.Count <= 0)
            {
                //返回信息
                resultmsg.status = false;
                resultmsg.msg = "未获取新闻资讯信息!";
                resultmsg.data = null;
            }
            else
            {
                resultmsg.status = true;
                resultmsg.msg = "成功获取新闻资讯信息!";
                resultmsg.data = qnews;
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
        /// 获取市场行情信息
        /// </summary>
        /// <param name="crop_type">作物类型编号</param>
        /// <returns></returns>
        [HttpGet]
        public object GetMarket(int crop_type)
        {
            string token = null;
            //获取请求
            var request = HttpContext.Current.Request;
            //响应
            ResultMsg<List<Market>> resultmsg = new ResultMsg<List<Market>>();
            HttpResponseMessage response = new HttpResponseMessage();
            PostgreSQL.OpenCon();//打开数据库
            //查询数据库
            string str = "select * from tb_market where crop_type = @crop_type";
            var para = new DbParameter[1];
            para[0] = PostgreSQL.NewParameter("@crop_type", crop_type);
            var qmarket = PostgreSQL.ExecuteTListQuery<Market>(str, null, para);
            PostgreSQL.CloseCon();//关闭数据库
            if (qmarket.Count <= 0)
            {
                //返回信息
                resultmsg.status = false;
                resultmsg.msg = "未获取此作物的市场行情信息!";
                resultmsg.data = null;
            }
            else
            {
                resultmsg.status = true;
                resultmsg.msg = "成功获取市场行情信息!";
                resultmsg.data = qmarket;
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
