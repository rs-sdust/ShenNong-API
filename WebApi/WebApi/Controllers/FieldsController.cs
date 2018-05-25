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
    /// <summary>
    /// 地块相关操作
    /// </summary>
    [AuthFilterOutside]
    public class FieldsController : ApiController
    {
        /// <summary>
        /// 获取指定农场的地块列表
        /// </summary>
        /// <param name="farmid">农场编号</param>
        /// <returns></returns>
        [HttpGet]
        public object GetFields(int farmid)
        { 
            //获取请求
            var request = HttpContext.Current.Request;
            //声明响应
            ResultMsg<List<Field>> resultmsg = new ResultMsg<List<Field>>();
            HttpResponseMessage response = new HttpResponseMessage();
            //查询数据库
            string str = "select * from tb_field where farm = @farm";
            PostgreSQL.OpenCon();//打开数据库
            var para = new DbParameter[1];
            para[0] = PostgreSQL.NewParameter("@farm",farmid);      
            var qField = PostgreSQL.ExecuteTListQuery<Field>(str, null, para);
            PostgreSQL.CloseCon();//关闭数据库
           //返回信息
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
        /// <summary>
        /// 添加地块
        /// </summary>
        /// <param name="field">地块类</param>
        /// <returns></returns>
        [HttpPost]
        public object AddField(Field field)
        {
            string token = null;
            //获取请求
            var request = HttpContext.Current.Request;
            //声明响应
            ResultMsg<Field> resultmsg = new ResultMsg<Field>();
            HttpResponseMessage response = new HttpResponseMessage();
            if(field==null)//判断输入的数据格式是否正确
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
            //查询用户所在农场编号
            string strtoken = SunGolden.Encryption.DEncrypt.Decrypt(request.Headers["Token"]); //解密Token
            string str_mobile = strtoken.Substring(0, strtoken.IndexOf(","));
            //查询数据库tb_user
            string str = string.Format("select * from tb_user where \"mobile\" =\'{0}\'", str_mobile);
            //string str ="select farm from tb_user where \"mobile\" = \'@mobile\'}";
            var para = new DbParameter[1];
            para[0] = PostgreSQL.NewParameter("@moblie", str_mobile); 
            var qfarm = PostgreSQL.ExecuteTQuery<User>(str, null, para);
            if (qfarm.farm!=field.farm)//判断添加地块的农场是否是登录用户所在的农场
            {
                resultmsg.status = false;
                resultmsg.msg = "此地块不属于这个农场,无法添加!";
                resultmsg.data = null;
                token = request.Headers["Token"];

            }
            else
            {
                //查询数据库
                string str_select = "select * from tb_field where \"name\" = @name and farm=@farm";
                var para1 = new DbParameter[2];
                para1[0] = PostgreSQL.NewParameter("@name", field.name);
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
                    //数据检查
                    DataAuth auth = new DataAuth();
                    if(auth.crop(field.currentcrop))
                    {
                        string str_insert = "insert into tb_field(farm,\"name\",geom,area,createdate,currentcrop,sowdate,phenophase,thumb) values(@farm,@name,@geom,@area,@createdate,@currentcrop,@sowdate,@phenophase,@thumb);";
                        var trans = PostgreSQL.BeginTransaction();
                        try
                        {
                            //int phenophase =Convert.ToInt16(DateTime.Now.Date - field.sowdate);
                            var para2 = new DbParameter[9];
                            para2[0] = PostgreSQL.NewParameter("@farm", field.farm);
                            para2[1] = PostgreSQL.NewParameter("@name", field.name);
                            para2[2] = PostgreSQL.NewParameter("@geom", field.geom);
                            para2[3] = PostgreSQL.NewParameter("@area", field.area);
                            para2[4] = PostgreSQL.NewParameter("@createdate", DateTime.Now.Date);
                            para2[5] = PostgreSQL.NewParameter("@currentcrop", field.currentcrop);
                            para2[6] = PostgreSQL.NewParameter("@sowdate", field.sowdate);
                            para2[7] = PostgreSQL.NewParameter("@phenophase", field.phenophase);
                            para2[8] = PostgreSQL.NewParameter("thumb", field.thumb);
                            var num = PostgreSQL.ExecuteNoneQuery(str_insert, trans, para2);
                            PostgreSQL.CommitTransaction(trans);
                            //返回信息
                            resultmsg.status = true;
                            resultmsg.msg = "添加地块成功!";
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
                        resultmsg.msg = "输入的作物id不存在！" ;
                        resultmsg.data = null;
                        token = request.Headers["Token"];
                    }   
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
        /// 更新指定地块作物信息
        /// </summary>
        /// <param name="field">地块类</param>
        /// <returns></returns>
        [HttpPost]
        public object UpdateField(Field field)
        {
            string token = null;
            //获取请求
            var request = HttpContext.Current.Request;
            //声明响应
            ResultMsg<Field> resultmsg = new ResultMsg<Field>();
            HttpResponseMessage response = new HttpResponseMessage();
            if (field == null)//判断输入的数据格式是否正确
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
            //查询用户所在农场编号
            string strtoken = SunGolden.Encryption.DEncrypt.Decrypt(request.Headers["Token"]); //解密Token
            string str_mobile = strtoken.Substring(0, strtoken.IndexOf(","));
             //查询数据库tb_user
            string str = string.Format("select * from tb_user where \"mobile\" =\'{0}\'", str_mobile);
            var para = new DbParameter[1];
            para[0] = PostgreSQL.NewParameter("@moblie", str_mobile);
            var qfarm = PostgreSQL.ExecuteTQuery<User>(str, null, para);
            //查询数据库tb_field
            string str_select = "select * from tb_field where id = @id;";//name = @name and farm = @farm;";
            var para1 = new DbParameter[1];
            para1[0] = PostgreSQL.NewParameter("@id", field.id);
            var qField = PostgreSQL.ExecuteTQuery<Field>(str_select, null, para1);
            if (qfarm.farm != qField.farm)
            {
                resultmsg.status = false;
                resultmsg.msg = "此地块不属于这个农场,无法更新!";
                resultmsg.data = null;
                token = request.Headers["Token"];
            }
            else
            {
               //判断地块是否存在
                if (qField == null)
                {
                    resultmsg.status = false;
                    resultmsg.msg = "地块不存在!";
                    resultmsg.data = null;
                    token = request.Headers["Token"];
                }
                else
                {
                    //数据检查
                    DataAuth auth = new DataAuth();
                    if (auth.crop(field.currentcrop))
                    {
                          
                        string str_update = "update tb_field set \"name\" = @name,sowdate = @sowdate ,currentcrop = @currentcrop where id=@id";
                        var trans = PostgreSQL.BeginTransaction();
                        try
                        {
                            var para2 = new DbParameter[6];
                            para2[0] = PostgreSQL.NewParameter("@name", field.name);
                            para2[1] = PostgreSQL.NewParameter("@area", field.area);
                            para2[2] = PostgreSQL.NewParameter("@sowdate", field.sowdate);
                            para2[3] = PostgreSQL.NewParameter("@currentcrop", field.currentcrop);
                            para2[4] = PostgreSQL.NewParameter("@thumb", field.thumb);
                            para2[5] = PostgreSQL.NewParameter("@id", field.id);
                            var num = PostgreSQL.ExecuteNoneQuery(str_update, trans, para2);
                            PostgreSQL.CommitTransaction(trans);
                            //返回信息
                            resultmsg.status = true;
                            resultmsg.msg = "成功更新地块信息!";
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
                        resultmsg.msg = "输入的作物id不存在！";
                        resultmsg.data = null;
                        token = request.Headers["Token"];
                    }
                    
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
        /// 批量修改地块作物类型
        /// </summary>
        /// <param name="field">地块类</param>
        /// <returns></returns>
        [HttpPost]
        public object BatchField(Field field)
        {
            string token = null;
            //获取请求
            var request = HttpContext.Current.Request;
            //声明响应
            ResultMsg<Field> resultmsg = new ResultMsg<Field>();
            HttpResponseMessage response = new HttpResponseMessage();
            if (field == null)//判断输入的数据格式是否正确
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
            //查询用户所在农场编号
            string strtoken = SunGolden.Encryption.DEncrypt.Decrypt(request.Headers["Token"]); //解密Token
            string str_mobile = strtoken.Substring(0, strtoken.IndexOf(","));
            //查询数据库tb_user
            PostgreSQL.OpenCon();//打开数据库
            string str = string.Format("select * from tb_user where \"mobile\" =\'{0}\'", str_mobile);
            //string str ="select farm from tb_user where \"mobile\" = \'@mobile\'}";
            var para = new DbParameter[1];
            para[0] = PostgreSQL.NewParameter("@moblie", str_mobile);
            var quser = PostgreSQL.ExecuteTQuery<User>(str, null, para);
            //查询数据库tb_field
            PostgreSQL.OpenCon();//打开数据库
            string str_select = "select * from tb_field where id = @id;";//name = @name and farm = @farm;";
            var para1 = new DbParameter[1];
            para1[0] = PostgreSQL.NewParameter("@id", field.id);
            var qField = PostgreSQL.ExecuteTQuery<Field>(str_select, null, para1);
            if (qField == null) //判断地块是否存在
            {
                resultmsg.status = false;
                resultmsg.msg = "地块不存在!";
                resultmsg.data = null;
                token = request.Headers["Token"];
            }
            else
            {
                if (quser.farm != qField.farm)
                {
                    resultmsg.status = false;
                    resultmsg.msg = "此地块不属于这个农场,无法修改!";
                    resultmsg.data = null;
                    token = request.Headers["Token"];
                }
                else
                {
                    //数据检查
                    DataAuth auth = new DataAuth();
                    if (auth.crop(field.currentcrop))
                    {

                        //查询数据库
                        string str_update = "update tb_field set sowdate = @sowdate ,currentcrop = @currentcrop where id=@id;";//currentcrop = @currentcrop and farm = @farm";
                        var trans = PostgreSQL.BeginTransaction();
                        try
                        {
                            var para2 = new DbParameter[3];
                            para2[0] = PostgreSQL.NewParameter("@currentcrop", field.currentcrop);
                            para2[1] = PostgreSQL.NewParameter("@id", field.id);
                            para2[2] = PostgreSQL.NewParameter("@sowdate", field.sowdate);
                            var num = PostgreSQL.ExecuteNoneQuery(str_update, trans, para2);
                            PostgreSQL.CommitTransaction(trans);
                            //返回信息
                            resultmsg.status = true;
                            resultmsg.msg = "成功批量修改地块信息!";
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
                        resultmsg.msg = "输入的作物id不存在！";
                        resultmsg.data = null;
                        token = request.Headers["Token"];
                    }
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
        /// 获取作物类型
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
            var qCrop = PostgreSQL.ExecuteTListQuery<CropTypes>(str_select, null, para);
            PostgreSQL.CloseCon();//关闭数据库
            //返回信息
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
        /// <summary>
        /// 删除指定地块信息
        /// </summary>
        /// <param name="id">地块类</param>
        /// <returns></returns>
        [HttpDelete]
        public object DeleteField(int id) //Field field)
        { 
            string token = null;
            //获取请求
            var request = HttpContext.Current.Request;
            //声明响应
            ResultMsg<Field> resultmsg = new ResultMsg<Field>();
            HttpResponseMessage response = new HttpResponseMessage();
            //if(field ==null)//判断输入的数据格式是否正确
            //{
            //    resultmsg.status = false;
            //    resultmsg.msg = "输入的数据格式不正确,请检查后重新输入!";
            //    resultmsg.data = null;
            //    token = request.Headers["Token"];
            //    //添加响应头
            //    var resultObj1 = JsonConvert.SerializeObject(resultmsg, Formatting.Indented);
            //    response.Headers.Add("Token", token);
            //    response.Content = new StringContent(resultObj1);
            //    return response;
            //}
            PostgreSQL.OpenCon();//打开数据库
            //查询用户所在农场编号
            string strtoken = SunGolden.Encryption.DEncrypt.Decrypt(request.Headers["Token"]); //解密Token
            string str_mobile = strtoken.Substring(0, strtoken.IndexOf(","));
            //查询tb_user数据库
            string str = string.Format("select * from tb_user where \"mobile\" =\'{0}\'", str_mobile);
            //string str ="select farm from tb_user where \"mobile\" = \'@mobile\'}";
            var para = new DbParameter[1];
            para[0] = PostgreSQL.NewParameter("@moblie", str_mobile);
            var quser = PostgreSQL.ExecuteTQuery<User>(str, null, para);
            //查询tb_field数据库
            string str_select = "select * from tb_field where id = @id;";
            var para1 = new DbParameter[1];
            para1[0] = PostgreSQL.NewParameter("@id", id);
            var qField = PostgreSQL.ExecuteTQuery<Field>(str_select, null, para1);
            if (qField == null)//判断地块是否存在
            {
                resultmsg.status = false;
                resultmsg.msg = "地块不存在!";
                resultmsg.data = null;
                token = request.Headers["Token"];
            }
            else
            {
               
               if (quser.farm != qField.farm)//判断地块是否属于农场
                {
                    resultmsg.status = false;
                    resultmsg.msg = "此地块不属于这个农场,无法删除!";
                    resultmsg.data = null;
                    token = request.Headers["Token"];
                }
                else
                {
                    var trans = PostgreSQL.BeginTransaction();
                    try
                    {
                        //查询数据库
                        string str_delete = "delete  from tb_field where id = @id;";//name = @name and farm = @farm;";
                        var para3 = new DbParameter[1];
                        para3[0] = PostgreSQL.NewParameter("@id", id);
                        var num = PostgreSQL.ExecuteNoneQuery(str_delete, trans, para3);
                        PostgreSQL.CommitTransaction(trans);
                        //返回信息
                        resultmsg.status = true;
                        resultmsg.msg = "成功删除地块信息!";
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
            PostgreSQL.CloseCon();//关闭数据库
            //添加响应头
            var resultObj = JsonConvert.SerializeObject(resultmsg, Formatting.Indented);
            response.Headers.Add("Token", token);
            response.Content = new StringContent(resultObj);
            return response;
        }

        ///// <summary>
        ///// 获取指定作物的物候期
        ///// </summary>
        ///// <param name="crop_type">作物类型编号</param>
        ///// <returns></returns>
        //[HttpGet]
        //public object GetFieldPhenophase(int crop_type)
        //{
        //    string token = null;
        //    //获取请求
        //    var request = HttpContext.Current.Request;
        //    //响应
        //    ResultMsg<List<Phenophase>> resultmsg = new ResultMsg<List<Phenophase>>();
        //    HttpResponseMessage response = new HttpResponseMessage();
        //    //查询数据库 
        //    string str = "select * from tb_phenophase  where crop_type = @crop_type";
        //    PostgreSQL.OpenCon();//打开数据库
        //    var para = new DbParameter[1];
        //    para[0] = PostgreSQL.NewParameter("@crop_type", crop_type);
        //    //para[1] = PostgreSQL.NewParameter("@time", time);
        //    var qphen = PostgreSQL.ExecuteTListQuery<Phenophase>(str, null, para);
        //    PostgreSQL.CloseCon();//关闭数据库
        //    if (qphen == null)
        //    {
        //        //返回信息
        //        resultmsg.status = false;
        //        resultmsg.msg = "未获取此作物的物候信息!";
        //        resultmsg.data = null;
        //    }
        //    else
        //    {
        //        resultmsg.status = true;
        //        resultmsg.msg = "成功获取此作物的物候信息!";
        //        resultmsg.data = qphen;
        //    }
        //    //返回信息
        //    token = request.Headers["Token"];

        //    //添加响应头
        //    var resultObj = JsonConvert.SerializeObject(resultmsg, Formatting.Indented);
        //    response.Headers.Add("Token", token);
        //    response.Content = new StringContent(resultObj);
        //    return response;
        //}
    }
}
