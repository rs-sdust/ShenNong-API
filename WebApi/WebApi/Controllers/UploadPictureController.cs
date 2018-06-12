using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Data;
using System.IO;
using System.Net;
using System.Net.Http;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;
using System.Web;
using System.Web.Http;
using WebApi.Auth;
using WebApi.Models;

namespace WebApi.Controllers
{
    /// <summary>
    /// 图片上传接口
    /// </summary>
    [AuthFilterOutside]
    public class UploadPictureController : ApiController
    {
        /// <summary>
        /// 上传图片
        /// </summary>
        /// <returns></returns>
        [HttpPost]
        public async Task<HttpResponseMessage> PostUpload()
        {
            string token = "";
            //获取请求
            var request = HttpContext.Current.Request;
            //声明响应
            ResultMsg<List<object >> resultmsg = new ResultMsg<List<object>>();
            HttpResponseMessage response = new HttpResponseMessage();
            string strtoken = SunGolden.Encryption.DEncrypt.Decrypt(request.Headers["Token"]); //解密Token
            string user_mobile = strtoken.Substring(0, strtoken.IndexOf(","));//获取用户手机号
            //指定要将文件存入的服务器物理位置,如果路径不存在，创建路径
            string fileSaveLocation = HttpContext.Current.Server.MapPath("~/User_Img/") + user_mobile;
            //HttpPostedFile
            if (!Directory.Exists(fileSaveLocation))
            {
                Directory.CreateDirectory(fileSaveLocation);
            }
            //检查请求中是否包含multipart/form-data,即文件上传请求
            if (!Request.Content.IsMimeMultipartContent())
            {
                //返回信息
                resultmsg.status = false;
                resultmsg.msg = "不是有效的'form-data'类型";
                resultmsg.data = null;
                token = request.Headers["Token"];
                throw new HttpResponseException(HttpStatusCode.UnsupportedMediaType);
            }
            //初始化MultipartFormDataStreamProvider实例
            CustomMultipartFormDataStreamProvider provider = new CustomMultipartFormDataStreamProvider(fileSaveLocation);
            List<string> files = new List<string>();
            List<object> path = new List<object >();
            Picture pic = new Picture();
            try
            {
                // 读取文件上传
                await Request.Content.ReadAsMultipartAsync(provider);
                string filepath = "";
                int i = 0;
                //获取已上传的文件名
                foreach (MultipartFileData file in provider.FileData)
                {
                    if (file.Headers.ContentDisposition.FileName != null)
                    {
                        //接收文件
                        files.Add(Path.GetFileName(file.LocalFileName));
                        filepath = string.Format("{0}/{1}", "~/User_Img/" + user_mobile, Path.GetFileName(file.LocalFileName));
                        //string s = filepath.Replace("/", "");
                        pic.id = i;
                        pic.path = filepath;
                        var m = JsonConvert.SerializeObject(pic, Formatting.Indented);
                        JObject job = (JObject)JsonConvert.DeserializeObject(m);
                        path.Add(job);
                        i++;
                    }
                }                   
                //return Request.CreateResponse(HttpStatusCode.OK, files);
                //返回信息
                resultmsg.status = true;
                resultmsg.msg = "图片上传成功！";
                resultmsg.data = path;
                token= request.Headers["Token"];
            }
            catch (Exception ex)
            {
                //返回信息
                resultmsg.status = false;
                resultmsg.msg = "图片保存失败" + ex.Message;
                resultmsg.data = null;
                token = request.Headers["Token"];
            }
            //添加响应内容
            var resultObj = JsonConvert.SerializeObject(resultmsg, Formatting.Indented);
            response.Headers.Add("Token", token);
            response.Content = new StringContent(resultObj);
            return response;

        }
       
        ///// <summary>
        ///// 
        ///// </summary>
        ///// <param name="Part"></param>
        ///// <returns></returns>
        //public async Task<HttpResponseMessage> PostUploadFile(int Part)
        //{
        //    //获取请求
        //    var request = HttpContext.Current.Request;
        //    //声明响应
        //    ResultMsg<List<object>> resultmsg = new ResultMsg<List<object>>();
        //    HttpResponseMessage response = new HttpResponseMessage();
        //    string strtoken = SunGolden.Encryption.DEncrypt.Decrypt(request.Headers["Token"]); //解密Token
        //    string user_mobile = strtoken.Substring(0, strtoken.IndexOf(","));//获取用户手机号
        //    // Check whether the POST operation is MultiPart?    
        //    if (!Request.Content.IsMimeMultipartContent())
        //    {
        //        throw new HttpResponseException(HttpStatusCode.UnsupportedMediaType);
        //    }

        //    // Prepare CustomMultipartFormDataStreamProvider in which our multipart form    
        //    // data will be loaded.    
        //    string partFolder = string.Empty;
        //    switch (Part)
        //    {
        //        case 1:
        //            partFolder = "MobileUploadImages_头像";
        //            break;
        //        case 2:
        //            partFolder = "MobileUploadImages_地块缩略图";
        //            break;
        //        case 3:
        //            partFolder = "MobileUploadImages_地块实况图";
        //            break;
        //        case 4:
        //            partFolder = "MobileUploadImages_意见反馈";
        //            break;
        //        default:
        //            partFolder = "MobileUploadImages_其他";
        //            break;
        //    }
        //    //指定要将文件存入的服务器物理位置,如果路径不存在，创建路径
        //    string fileSaveLocation = HttpContext.Current.Server.MapPath("~/App_Data/") + user_mobile + "/" + partFolder;
        //    //string fileSaveLocation = HttpContext.Current.Server.MapPath("~/" + ImageUploadPublic.FoldersAddress + "/" + partFolder);
        //    if (!Directory.Exists(fileSaveLocation))
        //        Directory.CreateDirectory(fileSaveLocation);

        //    CustomMultipartFormDataStreamProvider provider = new CustomMultipartFormDataStreamProvider(fileSaveLocation);
        //    List<string> files = new List<string>();
        //    try
        //    {
        //        // Read all contents of multipart message into CustomMultipartFormDataStreamProvider.                    
        //        await Request.Content.ReadAsMultipartAsync(provider);
        //        string newFileName = string.Empty;
        //        foreach (MultipartFileData file in provider.FileData)
        //        {
        //            newFileName = Path.GetFileName(file.LocalFileName);
        //            files.Add(newFileName);
        //        }

        //        return Request.CreateResponse(HttpStatusCode.OK, files);
        //    }
        //    catch (Exception ex)
        //    {
        //        return Request.CreateErrorResponse(HttpStatusCode.InternalServerError, ex);
        //    }
        //}



        // /// <summary>
        ///// App上传图片
        ///// </summary>
        ///// <returns>返回上传图片的相对路径</returns>
        //[HttpPost]
        //public object UploadImage()
        //{
        //    //获取请求
        //    var request = HttpContext.Current.Request;
        //    //声明响应
        //    ResultMsg<List<object>> resultmsg = new ResultMsg<List<object>>();
        //    HttpResponseMessage response = new HttpResponseMessage();
        //    string strtoken = SunGolden.Encryption.DEncrypt.Decrypt(request.Headers["Token"]); //解密Token
        //    string user_mobile = strtoken.Substring(0, strtoken.IndexOf(","));//获取用户手机号
        //    //指定要将文件存入的服务器物理位置,如果路径不存在，创建路径
        //    string fileSaveLocation = HttpContext.Current.Server.MapPath("~/App_Data/") + user_mobile;

        //    // 检查是否是 multipart/form-data
        //    if (!Request.Content.IsMimeMultipartContent("form-data"))
        //    {

        //    }


        //    DateTime dt = DateTime.Now;
        //    string path = string.Format("/imagestore/{0}/{1}{2}", dt.Year, dt.Month.ToString().PadLeft(2, '0'), dt.Day.ToString().PadLeft(2, '0'));
        //    string abtPath = HttpContext.Current.Server.MapPath(path);
        //    if (!Directory.Exists(abtPath))
        //    {

        //    }


        //    string fileName = "";
        //    string ext = "";
        //    string filePath = "";
        //    try
        //    {
        //        HttpContextBase context = (HttpContextBase)Request.Properties["MS_HttpContext"];//获取传统context
        //        HttpRequestBase request1 = context.Request;//定义传统request对象
        //        HttpFileCollectionBase imgFiles = request1.Files;
        //        for (int i = 0; i < imgFiles.Count; i++)
        //        {
        //            ext = InvestmentCommon.FileHelper.GetExtention(imgFiles[i].FileName);
        //            fileName = string.Format("{0}{1}", System.Guid.NewGuid().ToString(), ext);
        //            filePath = string.Format("{0}/{1}", path, fileName);
        //            imgFiles[i].SaveAs(abtPath + "\\" + fileName);
        //            imgFiles[i].InputStream.Position = 0;
        //            rModel.data = filePath.Replace("/", "");
        //            rModel.state = 1;
        //            rModel.msg = "success";
        //        }


        //    }
        //    catch (Exception e)
        //    {
        //        InvestmentCommon.Log4NetHelper.Log.Error("图片保存失败");
        //        rModel.state = 0;
        //        rModel.msg = "图片保存失败";
        //        return rModel;
        //    }


        //    //result = Newtonsoft.Json.JsonConvert.SerializeObject(rList);
        //    return rModel;
        //}
    }
}
