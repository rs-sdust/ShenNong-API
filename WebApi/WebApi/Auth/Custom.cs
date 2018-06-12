
using System;
using System.IO;
using System.Net.Http;
using System.Net.Http.Headers;

namespace WebApi.Auth
{
    /// <summary>
    /// 
    /// </summary>
    public class CustomMultipartFormDataStreamProvider : MultipartFormDataStreamProvider
    {
        /// <summary>
        /// 
        /// </summary>
        /// <param name="path"></param>
        public CustomMultipartFormDataStreamProvider(string path) : base(path) { }
        /// <summary>
        /// 
        /// </summary>
        /// <param name="headers"></param>
        /// <returns></returns>
        public override string GetLocalFileName(HttpContentHeaders headers)
        {
            //修改图片名称并返回  
            string newFileName = string.Empty;
            newFileName = Path.GetExtension(headers.ContentDisposition.FileName.Replace("\"", string.Empty));//获取后缀名  
            newFileName = DateTime.Now.ToString("yyyyMMddHHmmss") + new Random().Next(0, 99999) + newFileName;
            return newFileName;
        }
    }
}