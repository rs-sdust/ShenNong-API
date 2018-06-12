using SunGolden.DBUtils;
using System;
using System.Data.Common;
using WebApi.Models;
namespace WebApi.Auth
{
    /// <summary>
    /// 数据验证
    /// </summary>
    public class DataAuth
    {
        /// <summary>
        /// 作物类型
        /// </summary>
        /// <param name="cropid">作物类型id</param>
        /// <returns></returns>
        public bool crop(int cropid)
        {
            bool f = false;
            //查询数据库
            string str_select = "select * from dic_crop_type where id=@id";
            var para = new DbParameter[1];
            para[0] = PostgreSQL.NewParameter("@id", cropid);
            var q_select = PostgreSQL.ExecuteTListQuery<CropTypes>(str_select, null, para);
            if (q_select.Count >0)
            {
                f = true;
            }
            return f;
        }
        /// <summary>
        /// 农场类型
        /// </summary>
        /// <param name="farmid">农场类型id</param>
        /// <returns></returns>
        public bool farm(int farmid)
        {
            bool f = false;
            //查询数据库
            string str_select = "select * from tb_farm where id=@id";
            var para = new DbParameter[1];
            para[0] = PostgreSQL.NewParameter("@id", farmid);
            var q_select = PostgreSQL.ExecuteTListQuery<Farm>(str_select, null, para);
            if (q_select.Count > 0)
            {
                f = true;
            }
            return f;
        }
        /// <summary>
        /// 地块类型
        /// </summary>
        /// <param name="fieldid">地块编号</param>
        /// <returns></returns>
        public bool field(int fieldid)
        {
            bool f = false;
            //查询数据库
            string str_select = "select * from tb_field where id=@id";
            var para = new DbParameter[1];
            para[0] = PostgreSQL.NewParameter("@id", fieldid);
            var q_select = PostgreSQL.ExecuteTListQuery<Field>(str_select, null, para);
            if (q_select.Count > 0)
            {
                f = true;
            }
            return f;
        }
        /// <summary>
        /// 用户类型
        /// </summary>
        /// <param name="userid">用户id</param>
        /// <returns></returns>
        public bool user(int userid)
        {
            bool f = false;
            //查询数据库
            string str_select = "select * from tb_user where id=@id";
            var para = new DbParameter[1];
            para[0] = PostgreSQL.NewParameter("@id", userid);
            var q_select = PostgreSQL.ExecuteTListQuery<User>(str_select, null, para);
            if (q_select.Count > 0)
            {
                f = true;
            }
            return f;
        }
        /// <summary>
        /// 农情类型
        /// </summary>
        /// <param name="rsi_typeid">农情类型id</param>
        /// <returns></returns>
        public bool rsitype(int rsi_typeid)
        {
            bool f = false;
            //查询数据库
            string str_select = "select * from dic_rsi_type where id=@id";
            var para = new DbParameter[1];
            para[0] = PostgreSQL.NewParameter("@id", rsi_typeid);
            var q_select = PostgreSQL.ExecuteTListQuery<RsiTypes>(str_select, null, para);
            if (q_select.Count > 0)
            {
                f = true;
            }
            return f;
        }
        /// <summary>
        /// 农情等级类型
        /// </summary>
        /// <param name="rsi_typeid">农情等级类型id</param>
        /// <returns></returns>
        public bool rsigrade(int rsi_typeid)
        {
            bool f = false;
            //查询数据库
            string str_select = "select * from dic_rsi_grade where id=@id";
            var para = new DbParameter[1];
            para[0] = PostgreSQL.NewParameter("@id", rsi_typeid);
            var q_select = PostgreSQL.ExecuteTListQuery<RsiGrade>(str_select, null, para);
            if (q_select.Count > 0)
            {
                f = true;
            }
            return f;
        }
        /// <summary>
        /// 新闻资讯类型
        /// </summary>
        /// <param name="news_typeid">新闻资讯类型id</param>
        /// <returns></returns>
        public bool newstype(int news_typeid)
        {
            bool f = false;
            //查询数据库
            string str_select = "select * from dic_news_type where id=@id";
            var para = new DbParameter[1];
            para[0] = PostgreSQL.NewParameter("@id", news_typeid);
            var q_select = PostgreSQL.ExecuteTListQuery<NewsTypes>(str_select, null, para);
            if (q_select.Count > 0)
            {
                f = true;
            }
            return f;
        }
        /// <summary>
        /// 长势类型
        /// </summary>
        /// <param name="growth_typeid">长势类型id</param>
        /// <returns></returns>
        public bool growthtype(int growth_typeid)
        {
            bool f = false;
            //查询数据库
            string str_select = "select * from dic_growth_type where id=@id";
            var para = new DbParameter[1];
            para[0] = PostgreSQL.NewParameter("@id", growth_typeid);
            var q_select = PostgreSQL.ExecuteTListQuery<GrowthTypes>(str_select, null, para);
            if (q_select.Count > 0)
            {
                f = true;
            }
            return f;
        }
        /// <summary>
        /// 土壤湿度类型
        /// </summary>
        /// <param name="moisture_typeid">土壤湿度类型id</param>
        /// <returns></returns>
        public bool moisturetype(int moisture_typeid)
        {
            bool f = false;
            //查询数据库
            string str_select = "select * from dic_moisture_type where id=@id";
            var para = new DbParameter[1];
            para[0] = PostgreSQL.NewParameter("@id", moisture_typeid);
            var q_select = PostgreSQL.ExecuteTListQuery<MoistureTypes>(str_select, null, para);
            if (q_select.Count > 0)
            {
                f = true;
            }
            return f;
        }
        /// <summary>
        /// 虫害类型
        /// </summary>
        /// <param name="pest_typeid">虫害类型id</param>
        /// <returns></returns>
        public bool pesttype(int pest_typeid)
        {
            bool f = false;
            //查询数据库
            string str_select = "select * from dic_pest_type where id=@id";
            var para = new DbParameter[1];
            para[0] = PostgreSQL.NewParameter("@id", pest_typeid);
            var q_select = PostgreSQL.ExecuteTListQuery<PestTypes>(str_select, null, para);
            if (q_select.Count > 0)
            {
                f = true;
            }
            return f;
        }
        /// <summary>
        /// 病害类型
        /// </summary>
        /// <param name="disease_typeid">病害类型id</param>
        /// <returns></returns>
        public bool diseasetype(int disease_typeid)
        {
            bool f = false;
            //查询数据库
            string str_select = "select * from dic_disease_type where id=@id";
            var para = new DbParameter[1];
            para[0] = PostgreSQL.NewParameter("@id", disease_typeid);
            var q_select = PostgreSQL.ExecuteTListQuery<DiseaseTypes>(str_select, null, para);
            if (q_select.Count > 0)
            {
                f = true;
            }
            return f;
        }
        /// <summary>
        /// 获取token里role的值
        /// </summary>
        /// <param name="token">令牌</param>
        /// <returns></returns>
        public string RoleType(string token)
        {
            //解密Token
            string strtoken = SunGolden.Encryption.DEncrypt.Decrypt(token);
            //
            var index = strtoken.IndexOf(",");
            var indexend = strtoken.LastIndexOf(",");
            string str_mobile = strtoken.Substring(0, index);
            string str_role = strtoken.Substring(index + 1, 1);
            return str_role;
        }
        /// <summary>
        /// 根据种植时间和当前作物获取当前时间的物候期
        /// </summary>
        /// <param name="date"></param>
        /// <param name="currentcrop"></param>
        /// <returns></returns>
        public int phentype(DateTime date, int currentcrop)
        {
            int phenophase = -1;
            if (DateTime.Now.Date < date)//判断种植日期
            {
                phenophase = -1;
            }
            else
            {
                TimeSpan sowdays1 = DateTime.Now.Date.Subtract(date);
                int sowdays = sowdays1.Days;
                //获取当前作物的物候
                string str_phen = "select id, phen_type,time from tb_phenophase where crop_type = @crop_type ;";
                var para3 = new DbParameter[1];
                para3[0] = PostgreSQL.NewParameter("@crop_type", currentcrop);
                var q_phen = PostgreSQL.ExecuteTableQuery(str_phen, null, para3);
                if (q_phen != null && q_phen.Rows != null && q_phen.Rows.Count > 0)
                {
                    //作物的整个物候期时间
                    //object phen_sum = q_phen.Compute("sum(time)", "");
                    int time = Convert.ToInt16(q_phen.Rows[0]["time"]);
                    if (sowdays <= time && sowdays >= 0)
                    {
                        phenophase = Convert.ToInt16(q_phen.Rows[0]["phen_type"]);
                    }
                    else
                    {
                        #region
                        //int[] sum = new int[q_phen.Rows.Count];
                        //int s = 0;
                        //for (int i = 0; i < q_phen.Rows.Count; i++)
                        //{
                        //    s += Convert.ToInt16(q_phen.Rows[i]["time"]);
                        //    sum[i] = s;
                        //}
                        #endregion
                        for (int i = 1; i <= q_phen.Rows.Count; i++)
                        {
                            if (sowdays > Convert.ToInt16(q_phen.Rows[i - 1]["time"]) && sowdays <= Convert.ToInt16(q_phen.Rows[i]["time"]))
                            {
                                phenophase = Convert.ToInt16(q_phen.Rows[i - 1]["id"]);
                            }
                            else if (sowdays > Convert.ToInt16(q_phen.Rows[q_phen.Rows.Count - 1]["time"]))
                            {
                                phenophase = Convert.ToInt16(q_phen.Rows[q_phen.Rows.Count - 1]["id"]);
                            }
                        }
                    }
                }
                else phenophase = -1;
            }
            return phenophase;
        }
        //public int phentype(DateTime date, int currentcrop)
        //{
        //    int phenophase = -1;
        //    if (DateTime.Now.Date < date)//判断种植日期
        //    {
        //        phenophase = -1;
        //    }
        //    else
        //    {
        //        TimeSpan sowdays1 = DateTime.Now.Date.Subtract(date);
        //        int sowdays = sowdays1.Days;
        //        //获取当前作物的物候
        //        string str_phen = "select id, phen_type,time from tb_phenophase where crop_type = @crop_type ORDER BY id DESC;";
        //        var para3 = new DbParameter[1];
        //        para3[0] = PostgreSQL.NewParameter("@crop_type", currentcrop);
        //        var q_phen = PostgreSQL.ExecuteTableQuery(str_phen, null, para3);
        //        if (q_phen != null && q_phen.Rows != null && q_phen.Rows.Count > 0)
        //        {
        //            //作物的整个物候期时间
        //            int time = Convert.ToInt16(q_phen.Rows[q_phen.Rows.Count - 1]["time"]);
        //            if (sowdays <= time && sowdays >= 0)
        //            {
        //                phenophase = Convert.ToInt16(q_phen.Rows[q_phen.Rows.Count - 1]["phen_type"]);
        //            }
        //            else
        //            {
        //                for (int i = 1; i < q_phen.Rows.Count; i++)
        //                {
        //                    if (sowdays <= Convert.ToInt16(q_phen.Rows[i]["time"]))
        //                    {
        //                        phenophase = Convert.ToInt16(q_phen.Rows[i + 1]["id"]);
        //                    }
        //                    else if (sowdays > Convert.ToInt16(q_phen.Rows[0]["time"]))
        //                    {
        //                        phenophase = Convert.ToInt16(q_phen.Rows[0]["id"]);
        //                    }
        //                }
        //            }
        //        }
        //        else phenophase = -1;
        //    }
        //    return phenophase;
        //}

    }
   
}