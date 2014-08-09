using NEG.Website.Models;
using System;
using System.Linq;
using System.Web.Mvc;
using NEG.Website.Models.DataAccess;

namespace NEG.Website.Controllers
{
    public class DemoController : BaseController
    {
        public ActionResult Detail()
        {
            string demoName = Convert.ToString(RouteData.Values["id"]);

            if (!string.IsNullOrWhiteSpace(demoName))
            {
                ViewData["DemoDetailInfo"] = DBHelper.DemoDetailInfoContext.FirstOrDefault(m =>
                                                                                           m.DemoName == demoName);
            }

            return View();
        }
    }
}
