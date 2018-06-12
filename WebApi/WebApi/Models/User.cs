using System;
namespace WebApi.Models
{
    /// <summary>
    /// 用户类
    /// </summary>
    public class User
    {
        /// <summary>
        /// 用户主键
        /// </summary>
        public int id { get; set; }
        /// <summary>
        /// 用户名
        /// </summary>
        public string name { get; set; }
        /// <summary>
        /// 手机号
        /// </summary>
        public string mobile { get; set; }
        //public string password { get; set; }
        /// <summary>
        /// 即时密码
        /// </summary>
        public string im_pwd { get; set; }
        /// <summary>
        /// 角色编号
        /// </summary>
        public int role { get; set; }
        /// <summary>
        /// 农场编号
        /// </summary>
        public int farm { get; set; }
        /// <summary>
        /// 用户头像
        /// </summary>
        public string icon { get; set; }
      

    }
    /// <summary>
    /// 返回信息
    /// </summary>
    /// <typeparam name="T">数据</typeparam>
    public class ResultMsg<T>
    {
        /// <summary>
        /// 处理状态
        /// </summary>
        public bool status { get; set; }
        /// <summary>
        /// 返回消息
        /// </summary>
        public string msg { get; set; }
        /// <summary>
        /// 返回数据信息
        /// </summary>
        public T data { get; set; }
    }
    /// <summary>
    /// 用户信息
    /// </summary>
    public class Userinfo
    {
        /// <summary>
        /// 用户编号
        /// </summary>
        public string userid { get; set; }
        /// <summary>
        /// 密码
        /// </summary>
        public string password { get; set; }
        /// <summary>
        /// 别名
        /// </summary>
        public string nick { get; set; }
        /// <summary>
        /// 手机号
        /// </summary>
        public string mobile { get; set; }
        /// <summary>
        /// 邮箱
        /// </summary>
        public string email { get; set; }
        /// <summary>
        /// 头像
        /// </summary>
        public string icon_url { get; set; }

    }
    /// <summary>
    /// 待处理通知任务
    /// </summary>
    public class UserTask
    {
        /// <summary>
        /// 主键
        /// </summary>
        public int id { get; set; }
        /// <summary>
        /// 创建人
        /// </summary>
        public int creator { get; set; }
        /// <summary>
        /// 农场编号
        /// </summary>
        public int farm { get; set; }
        /// <summary>
        /// 审查人
        /// </summary>
        public int examiner { get; set; }
        /// <summary>
        /// 任务类型
        /// </summary>
        public int type { get; set; }
        /// <summary>
        /// 任务描述
        /// </summary>
        public string  description { get; set; }
        /// <summary>
        /// 处理状态
        /// </summary>
        public int state { get; set; }
        /// <summary>
        /// 是否通过
        /// </summary>
        public bool agree { get; set; }
        /// <summary>
        /// 创建时间
        /// </summary>
        public DateTime createdate { get; set; }
        /// <summary>
        /// 处理时间
        /// </summary>
        public DateTime processdate { get; set; }
    }
    /// <summary>
    /// 信息反馈
    /// </summary>
    public class FeedBack
    {
        /// <summary>
        /// 主键
        /// </summary>
        public int id { get; set; }
        /// <summary>
        /// 用户编号
        /// </summary>
        public int userid { get; set; }
        /// <summary>
        /// 意见
        /// </summary>
        public string suggestion { get; set; }
        /// <summary>
        /// 图片链接
        /// </summary>
        public string url { get; set; }
    }
    /// <summary>
    /// 图片上传
    /// </summary>
    public class Picture
    {
        /// <summary>
        /// 图片地址编号
        /// </summary>
        public int id { get; set; }

        /// <summary>
        /// 图片相对地址
        /// </summary>
        public string path { get; set; }


    }
}