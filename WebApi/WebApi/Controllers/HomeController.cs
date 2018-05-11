using SunGolden.DBUtils;
using System;
using System.Collections.Generic;
using System.Data.Common;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using WebApi.Models;

namespace WebApi.Controllers
{
    public class HomeController : Controller
    {
        public ActionResult Index()
        {
            ViewBag.Title = "Home Page";

            //PostgreSQL.OpenCon();
            //var trans = PostgreSQL.BeginTransaction();
            //var list = PostgreSQL.ExecuteObjectQuery("select * from tb_user", typeof(User), trans, new DbParameter[0]);
            //var para= new DbParameter[3];
            //para[0] = PostgreSQL.NewParameter("testc", 1);
            //var num = PostgreSQL.ExecuteNoneQuery("insert,update,delete",trans, para);
            //PostgreSQL.RollbackTransaction(trans);
            //PostgreSQL.CommitTransaction(trans);
            //PostgreSQL.CloseCon();
            return View();
        }
    }
}
