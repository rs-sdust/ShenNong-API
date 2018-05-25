
using SunGolden.DBUtils;
using System;
using System.Data.Common;
using System.Linq;
using System.Web;
using System.Web.Http;
using System.Web.Http.Controllers;
using WebApi.Models;

namespace WebApi.Auth
{
    public class AuthFilterOutside: AuthorizeAttribute
    {
        /// <summary>
        /// 重写基类的验证方式，加入我们自定义的Ticket验证 
        /// </summary>
        /// <param name="actionContext">请求消息</param>
        public override void OnAuthorization(HttpActionContext actionContext)
        {
            //url获取token  
            var content = actionContext.Request.Properties["MS_HttpContext"] as HttpContextBase;
            var token = content.Request.Headers["Token"];
            if (!string.IsNullOrEmpty(token))
            {
                //解密用户ticket,并校验用户名是否匹配  
                if (ValidateTicket(token))
                {
                    base.IsAuthorized(actionContext);

                }
                else
                {

                    HandleUnauthorizedRequest(actionContext);
                }
            }
            //如果取不到身份验证信息，并且不允许匿名访问，则返回未验证401  
            else
            {
                var attributes = actionContext.ActionDescriptor.GetCustomAttributes<AllowAnonymousAttribute>().OfType<AllowAnonymousAttribute>();
                bool isAnonymous = attributes.Any(a => a is AllowAnonymousAttribute);
                if (isAnonymous)
                {
                    base.OnAuthorization(actionContext);
                }
                  
                else
                {
                    HandleUnauthorizedRequest(actionContext);
                }       
            }
        }
        /// <summary>
        /// 校验票据（数据库数据匹配）
        /// </summary>
        /// <param name="encryptToken">令牌</param>
        /// <returns></returns>
        private bool ValidateTicket(string encryptToken)
        {
            bool flag = false;
            try
            {
                //解密Token
                string strtoken = SunGolden.Encryption.DEncrypt.Decrypt(encryptToken);
                //
                var index = strtoken.IndexOf(",");
                var indexend = strtoken.LastIndexOf(",");
                string str_mobile = strtoken.Substring(0, index);
                string str_role = strtoken.Substring(index + 1,1);
                DateTime vaild_time =Convert.ToDateTime( strtoken.Substring(indexend + 1));
                //获取数据库用户信息
                PostgreSQL.OpenCon();//打开数据库
                string str_select = "select * from tb_user where mobile = @mobile;";
                var para = new DbParameter[1];
                para[0] = PostgreSQL.NewParameter("@mobile", str_mobile);
                var qUser = PostgreSQL.ExecuteTQuery<User>(str_select, null, para);
                PostgreSQL.CloseCon();//关闭数据库
                if (qUser !=null) //存在  
                {
                    flag = (DateTime.Now <= vaild_time )? true : false;
                }
            }
            catch (Exception ex)
            {
                flag = false;
            }
            return flag;
        }
       
    }
}
