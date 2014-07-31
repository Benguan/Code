using NEG.Website.Models;
using System;
using System.Linq;
using System.Web.Mvc;

namespace NEG.Website.Controllers
{
    public class DemoController : Controller
    {
        private NEGWebsiteEntities db = new NEGWebsiteEntities();

        public ActionResult Detail()
        {
            string demoName = Convert.ToString(RouteData.Values["id"]);

            if (!string.IsNullOrWhiteSpace(demoName))
            {
                ViewData["DemoDetailInfo"] = db.DemoDetailInfos.FirstOrDefault(m => m.DemoName == demoName);
            }

            return View();
        }


    }
}
