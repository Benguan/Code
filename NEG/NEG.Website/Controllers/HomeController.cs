using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Web;
using System.Web.Mvc;
using NEG.Website.Controls.Common;
using NEG.Website.Models;

namespace MVCTest.Controllers
{
    public class HomeController : Controller
    {
        private NEGWebsiteEntities db = new NEGWebsiteEntities();

        public ActionResult Tutorial()
        {
            ViewBag.Message = "Modify this template to jump-start your ASP.NET MVC application.";

            return View();
        }

        public ActionResult About()
        {
            ViewBag.Message = "Modify this template to jump-start your ASP.NET MVC application.";

            return View();
        }

        public ActionResult API()
        {
            ViewBag.Message = "Modify this template to jump-start your ASP.NET MVC application.";

            return View();
        }

        public ActionResult Demo()
        {
            ViewData["DemoDetailInfos"] = db.DemoDetailInfos.ToList();
            return View();
        }

        public ActionResult Module()
        {
            ViewBag.Message = "Modify this template to jump-start your ASP.NET MVC application.";

            return View();
        }
    }
}
