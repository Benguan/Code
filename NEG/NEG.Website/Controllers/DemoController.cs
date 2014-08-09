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
                var detail = DBHelper.DemoDetailInfoContext.FirstOrDefault(m =>
                                                                           m.DemoName == demoName);

                if (detail != null)
                {
                    ViewData["DemoDetailInfo"] = detail;
                }

            }

            return View();
        }
    }
}
