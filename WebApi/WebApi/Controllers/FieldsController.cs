using Newtonsoft.Json;
using SunGolden.DBUtils;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Common;
using System.Net.Http;
using System.Web;
using System.Web.Http;
using WebApi.Auth;
using WebApi.Models;

namespace WebApi.Controllers
{
    [AuthFilterOutside]
    public class FieldsController : ApiController
    {
        //获取地块列表
        [HttpGet]
        public object GetFields(Field field)
        { 
            //获取请求
            var request = HttpContext.Current.Request;
            //声明响应
            ResultMsg<List<GetFields>> resultmsg = new ResultMsg<List<GetFields>>();
            HttpResponseMessage response = new HttpResponseMessage();
            //查询数据库
            string str = "select name,area,currentcrop,thumb from tb_field where farm = @farm";
            PostgreSQL.OpenCon();
            var para = new DbParameter[1];
            para[0] = PostgreSQL.NewParameter("@farm", field.farm);      
            var qField = PostgreSQL.ExecuteTListQuery<GetFields>(str, null, para);
            PostgreSQL.CloseCon();
           //响应
            resultmsg.status = true;
            resultmsg.msg = "成功获取地块列表";
            resultmsg.data = qField;
            string token = request.Headers["Token"];
            //添加响应内容
            var resultObj = JsonConvert.SerializeObject(resultmsg, Formatting.Indented);
            response.Headers.Add("Token",token );
            response.Content = new StringContent(resultObj);
            return response;
        }       
        //添加地块
        [HttpPost]
        public object AddField(Field field)
        {
            string token = null;
            //获取请求
            var request = HttpContext.Current.Request;
            //声明响应
            ResultMsg<Field> resultmsg = new ResultMsg<Field>();
            HttpResponseMessage response = new HttpResponseMessage();
            //查询数据库
            PostgreSQL.OpenCon();
            string str_select = "select * from tb_field where \"name\" = @name and farm=@farm";
            var para1 = new DbParameter[2];
            para1[0] = PostgreSQL.NewParameter("@name", field.name );
            para1[1] = PostgreSQL.NewParameter("@farm", field.farm);
            var qField = PostgreSQL.ExecuteTQuery<Field>(str_select, null, para1);      
            //判断地块名是否存在
            if (qField != null)
            {
                resultmsg.status = false;
                resultmsg.msg = "地块名已经存在，请重新命名!";
                resultmsg.data = null;
                token = request.Headers["Token"];
            }
            else  
            {
                string str_insert = "insert into tb_field(farm,\"name\",geom,area,createdate,currentcrop,sowdate,phenophase,thumb) values(@farm,@name,@geom,@area,@createdate,@currentcrop,@sowdate,@phenophase,@thumb);";
                var trans = PostgreSQL.BeginTransaction();
                try  
                {
                    var para = new DbParameter[9];
                    para[0] = PostgreSQL.NewParameter("@farm", field.farm);
                    para[1] = PostgreSQL.NewParameter("@name", field.name);
                    para[2] = PostgreSQL.NewParameter("@geom", field.geom);
                    para[3] = PostgreSQL.NewParameter("@area", field.area);
                    para[4] = PostgreSQL.NewParameter("@createdate", field.createdate);
                    para[5] = PostgreSQL.NewParameter("@currentcrop", field.currentcrop);
                    para[6] = PostgreSQL.NewParameter("@sowdate", field.sowdate);
                    para[7] = PostgreSQL.NewParameter("@phenophase", field.phenophase);
                    para[8] = PostgreSQL.NewParameter("thumb", field.thumb);
                    var num = PostgreSQL.ExecuteNoneQuery(str_insert, trans, para);
                    PostgreSQL.CommitTransaction(trans);
                    //响应内容
                    resultmsg.status = true;
                    resultmsg.msg = "添加地块成功!";
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
        //更新指定地块作物信息
        [HttpPost]
        public object UpdateField(Field field)
        {
            string token = null;
            //获取请求
            var request = HttpContext.Current.Request;
            //声明响应
            ResultMsg<Field> resultmsg = new ResultMsg<Field>();
            HttpResponseMessage response = new HttpResponseMessage();
            //查询数据库
            PostgreSQL.OpenCon();
            string str_select = "select * from tb_field where id = @id;";//name = @name and farm = @farm;";
            var para = new DbParameter[1];
            para[0] = PostgreSQL.NewParameter("@id", field.id);
            //para[1] = PostgreSQL.NewParameter("@farm",field.farm);
            var qField = PostgreSQL.ExecuteTQuery<Field>(str_select, null, para);
            if (qField == null)
            {
                resultmsg.status = false ;
                resultmsg.msg = "地块不存在!";
                resultmsg.data = null;
                token = request.Headers["Token"];
            }
            else
            {
                string str_update = "update tb_field set \"name\" = @name,sowdate = @sowdate ,currentcrop = @currentcrop where id=@id";
                var trans = PostgreSQL.BeginTransaction();
                try
                {
                    var para1 = new DbParameter[6];
                    para1[0] = PostgreSQL.NewParameter("@name", field.name);
                    para1[1] = PostgreSQL.NewParameter("@area", field.area);
                    para1[2] = PostgreSQL.NewParameter("@sowdate", field.sowdate);
                    para1[3] = PostgreSQL.NewParameter("@currentcrop", field.currentcrop);
                    para1[4] = PostgreSQL.NewParameter("@thumb", field.thumb);
                    para1[5] = PostgreSQL.NewParameter("@id", field.id);
                    var num = PostgreSQL.ExecuteNoneQuery(str_update, trans, para1);
                    PostgreSQL.CommitTransaction(trans);
                    ////查询数据库
                    //PostgreSQL.OpenCon();
                    //string str = "select * from tb_field where name = @name;";
                    //var para2 = new DbParameter[1];
                    //para2[0] = PostgreSQL.NewParameter("@name", field.name);
                    //var qField1 = PostgreSQL.ExecuteTQuery<Field>(str, null, para2);
                    //PostgreSQL.CloseCon();
                    //响应内容
                    resultmsg.status = true;
                    resultmsg.msg = "成功更新地块信息!";
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
       //批量修改地块作物类型
        [HttpPost]
        public object BatchField(Field field)
        {
            string token = null;
            //获取请求
            var request = HttpContext.Current.Request;
            //声明响应
            ResultMsg<Field> resultmsg = new ResultMsg<Field>();
            HttpResponseMessage response = new HttpResponseMessage();
            //查询数据库
            PostgreSQL.OpenCon();
            string str_update = "update tb_field set sowdate = @sowdate ,currentcrop = @currentcrop where id=@id;";//currentcrop = @currentcrop and farm = @farm";
            var trans = PostgreSQL.BeginTransaction();
            try
            {
                var para1 = new DbParameter[3];
                para1[0] = PostgreSQL.NewParameter("@currentcrop", field.currentcrop);
                para1[1] = PostgreSQL.NewParameter("@id", field.id);
                para1[2] = PostgreSQL.NewParameter("@sowdate", field.sowdate);
                //para1[3] = PostgreSQL.NewParameter("@currentcrop", field.currentcrop);
                var num = PostgreSQL.ExecuteNoneQuery(str_update, trans, para1);
                PostgreSQL.CommitTransaction(trans);
                //响应内容
                resultmsg.status = true;
                resultmsg.msg = "成功批量修改地块信息!";
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
        //获取地块物候期
        [HttpGet]
        public object GetFieldPhenophase(Phenophase phen)
        {
            string token = null;
            //获取请求
            var request = HttpContext.Current.Request;
            //响应
            ResultMsg<Phenophase> resultmsg = new ResultMsg<Phenophase>();
            HttpResponseMessage response = new HttpResponseMessage();
            //查询数据库 
            string str = "select * from tb_phenophase  where crop_type = @crop_type";
            PostgreSQL.OpenCon();
            var para = new DbParameter[1];
            para[0] = PostgreSQL.NewParameter("@crop_type", phen.crop_type);
            var qphen = PostgreSQL.ExecuteTQuery<Phenophase>(str, null, para);
            PostgreSQL.CloseCon();
            if(qphen==null)
            {
                //响应内容
                resultmsg.status = false;
                resultmsg.msg = "未获取此地块的物候信息!";
                resultmsg.data = null;
            }
            else
            {
                resultmsg.status = true;
                resultmsg.msg = "成功获取此地块的物候信息!";
                resultmsg.data = qphen;
            }
            //响应内容
            token = request.Headers["Token"];

            //添加响应头
            var resultObj = JsonConvert.SerializeObject(resultmsg, Formatting.Indented);
            response.Headers.Add("Token", token);
            response.Content = new StringContent(resultObj);
            return response;
        }
        //获取作物类型
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
            PostgreSQL.OpenCon();       
            var para = new DbParameter[0];        
            var qCrop = PostgreSQL.ExecuteTListQuery<CropTypes>(str_select, null, para);
            PostgreSQL.CloseCon();
            //响应内容
            resultmsg.status = true;
            resultmsg.msg = "成功获取作物列表!";
            resultmsg.data = qCrop;
            token = request.Headers["Token"];
            //添加响应头
            var resultObj = JsonConvert.SerializeObject(resultmsg, Formatting.Indented);
            response.Headers.Add("Token", token);
            response.Content = new StringContent(resultObj);
            return response;
        }
        //删除指定地块信息
        [HttpPost]
        public object DeleteField(Field field)
        {
            string token = null;
            //获取请求
            var request = HttpContext.Current.Request;
            //声明响应
            ResultMsg<Field> resultmsg = new ResultMsg<Field>();
            HttpResponseMessage response = new HttpResponseMessage();
            PostgreSQL.OpenCon();
            //查询数据库，判断地块是否存在
            string str_select = "select * from tb_field where id = @id;";
            var para = new DbParameter[1];
            para[0] = PostgreSQL.NewParameter("@id", field.id);     
            var qField = PostgreSQL.ExecuteTQuery<Field>(str_select, null, para);
            if (qField == null)
            {
                resultmsg.status = false;
                resultmsg.msg = "地块不存在!";
                resultmsg.data = null;
                token = request.Headers["Token"];
            }
            else
            {
                var trans = PostgreSQL.BeginTransaction();
                try
                {
                    //查询数据库
                    string str = "delete  from tb_field where id = @id;";//name = @name and farm = @farm;";
                    var para1 = new DbParameter[1];
                    para1[0] = PostgreSQL.NewParameter("@id", field.id);
                    var num = PostgreSQL.ExecuteNoneQuery(str, trans, para1);
                    PostgreSQL.CommitTransaction(trans);
                    //响应内容
                    resultmsg.status = true;
                    resultmsg.msg = "成功删除地块信息!";
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
        ////获取指定地块农事信息
        //[HttpGet]
        //[AuthFilterOutside]
        //public object GetFieldplan(Field_plan fieldplan)
        //{
        //    //获取请求
        //    var request = HttpContext.Current.Request;

        //    //查询数据库
        //    string str = string.Format("select * from tb_field_plan where field = @field");
        //    PostgreSQL.OpenCon();
        //    var para = new DbParameter[1];
        //    para[0] = PostgreSQL.NewParameter("@field", fieldplan.filed);
        //    var qFieldplan = PostgreSQL.ExecuteTQuery<Field_plan>(str, null, para);
        //    //var list = PostgreSQL.ExecuteObjectQuery(str, typeof(Field_plan), trans, new DbParameter[0]);
        //    PostgreSQL.CloseCon();

        //    //响应
        //    UserResponse<Field_plan> resultmsg = new UserResponse<Field_plan>();
        //    HttpResponseMessage response = new HttpResponseMessage();
        //    resultmsg.msg = "0";
        //    resultmsg.data = qFieldplan;
        //    string token = request.Headers["Token"];
        //    //添加响应内容
        //    var resultObj = JsonConvert.SerializeObject(resultmsg, Formatting.Indented);
        //    response.Headers.Add("Token", token);
        //    response.Content = new StringContent(resultObj);
        //    return response;
        //}
        //获取token里用户的角色信息
       
    }
}
