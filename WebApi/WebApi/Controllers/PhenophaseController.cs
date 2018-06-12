using Newtonsoft.Json;
using SunGolden.DBUtils;
using System;
using System.Collections.Generic;
using System.Data.Common;
using System.Net.Http;
using System.Web;
using System.Web.Http;
using WebApi.Models;

namespace WebApi.Controllers
{
    /// <summary>
    /// 物候期相关接口
    /// </summary>
    public class PhenophaseController : ApiController
    {
        /// <summary>
        /// 获取指定作物的物候期详情
        /// </summary>
        /// <param name="crop_type">作物类型编号</param>
        /// <param name="phen_type">物候类型编号</param>
        /// <returns></returns> 
        [HttpGet]
        public object GetFieldPhenophase(int crop_type, int phen_type)
        {
            string token = null;
            //获取请求
            var request = HttpContext.Current.Request;
            //响应
            ResultMsg<Phenophase> resultmsg = new ResultMsg<Phenophase>();
            HttpResponseMessage response = new HttpResponseMessage();
            //查询数据库 
            string str = "select * from tb_phenophase  where crop_type = @crop_type and id=@id";
            PostgreSQL.OpenCon();//打开数据库
            var para = new DbParameter[2];
            para[0] = PostgreSQL.NewParameter("@crop_type", crop_type);
            para[1] = PostgreSQL.NewParameter("@id", phen_type);
            var qphen = PostgreSQL.ExecuteTQuery<Phenophase>(str, null, para);
            PostgreSQL.CloseCon();//关闭数据库
            if (qphen == null)
            {
                //返回信息
                resultmsg.status = false;
                resultmsg.msg = "此物候的物候详情信息，不存在!";
                resultmsg.data = null;
            }
            else
            {
                resultmsg.status = true;
                resultmsg.msg = "成功获取此物候的物候详情信息!";
                resultmsg.data = qphen;
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
        /// 获取指定农场主要作物的物候期
        /// </summary>
        /// <param name="farmid"></param>
        /// <returns></returns>
        [HttpGet]
        public object GetFarm_phen(int farmid)
        {
            string token = null;
            //获取请求
            var request = HttpContext.Current.Request;
            //响应
            ResultMsg<object> resultmsg = new ResultMsg<object>();
            HttpResponseMessage response = new HttpResponseMessage();
            PostgreSQL.OpenCon();//打开数据库
            //查询数据库
            //根据农场id ，按作物排序，获取同类作物的面积和
            string str_select = "select currentcrop,sum(area)area from tb_field where farm=@farm GROUP BY currentcrop order by area";
            var para = new DbParameter[1];
            para[0] = PostgreSQL.NewParameter("@farm", farmid);
            var qField = PostgreSQL.ExecuteTableQuery(str_select, null, para);
            int croptype = Convert.ToInt32(qField.Rows[qField.Rows.Count - 1][0]);
            string str = "SELECT phenophase FROM tb_field WHERE farm = @farm and currentcrop=@currentcrop ORDER BY area DESC";
            var para1 = new DbParameter[2];
            para1[0] = PostgreSQL.NewParameter("@farm", farmid);
            para1[1] = PostgreSQL.NewParameter("@currentcrop", croptype);
            var qphen = PostgreSQL.ExecuteScalarQuery(str, null, para1);
            PostgreSQL.CloseCon();
            if (qphen == null)
            {
                //返回信息
                resultmsg.status = false;
                resultmsg.msg = "未获取此农场主要作物的物候期!";
                resultmsg.data = qphen;
            }
            else
            {
                //返回信息
                resultmsg.status = true;
                resultmsg.msg = "成功获取此农场主要作物的物候期!";
                resultmsg.data = qphen;
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
        ///// 获取所有作物的物候信息列表
        ///// </summary>
        ///// <returns></returns>
        //[HttpGet]
        //public object GetPhenophases()
        //{
        //    string token = null;
        //    //获取请求
        //    var request = HttpContext.Current.Request;
        //    //声明响应
        //    ResultMsg<List<Phenophase>> resultmsg = new ResultMsg<List<Phenophase>>();
        //    HttpResponseMessage response = new HttpResponseMessage();
        //    //查询数据库
        //    string str_select = "select * from tb_phenophase";
        //    PostgreSQL.OpenCon(); //打开数据库    
        //    var para = new DbParameter[0];
        //    var q_select = PostgreSQL.ExecuteTListQuery<Phenophase>(str_select, null, para);
        //    PostgreSQL.CloseCon();//关闭数据库
        //    if (q_select.Count <= 0)
        //    {
        //        //返回信息
        //        resultmsg.status = false;
        //        resultmsg.msg = "未获取所有作物的物候信息列表!";
        //        resultmsg.data = null;
        //    }
        //    else
        //    {
        //        //返回信息
        //        resultmsg.status = true;
        //        resultmsg.msg = "成功获取所有作物的物候信息列表!";
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
        /// 获取指定作物的生长周期
        /// </summary>
        /// <param name="croptype"></param>
        /// <returns></returns>
        public object GetCropGrowthDay(int croptype)
        {
            string token = null;
            //获取请求
            var request = HttpContext.Current.Request;
            //响应
            ResultMsg<Crop_growthday> resultmsg = new ResultMsg<Crop_growthday>();
            HttpResponseMessage response = new HttpResponseMessage();
            //查询数据库 
            string str = "select * from tb_crop_growthday  where crop_type = @crop_type ";
            PostgreSQL.OpenCon();//打开数据库
            var para = new DbParameter[1];
            para[0] = PostgreSQL.NewParameter("@crop_type", croptype);
            var qgrowth = PostgreSQL.ExecuteTQuery<Crop_growthday>(str, null, para);
            PostgreSQL.CloseCon();//关闭数据库
            if (qgrowth == null)
            {
                //返回信息
                resultmsg.status = false;
                resultmsg.msg = "未获取此作物的生长周期信息!";
                resultmsg.data = null;
            }
            else
            {
                resultmsg.status = true;
                resultmsg.msg = "成功获取此作物的生长周期信息!";
                resultmsg.data = qgrowth;
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
