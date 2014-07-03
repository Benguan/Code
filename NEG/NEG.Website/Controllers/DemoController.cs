using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using NEG.Website.Models;

namespace MVCTest.Controllers
{
    public class DemoController : Controller
    {
        private NEGWebsiteEntities db = new NEGWebsiteEntities();

        public ActionResult DemoDetail()
        {
            string demoName =  Convert.ToString(RouteData.Route.GetRouteData(this.HttpContext).Values["id"]);

            if (!string.IsNullOrWhiteSpace(demoName))
            {
                ViewData["DemoDetailInfo"] = db.DemoDetailInfos.FirstOrDefault(m => m.DemoName == demoName);
            }

            return View();
        }


    }
}
