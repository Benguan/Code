using System;
using System.Linq;
using NEG.Website.Models;
using System.Web.Mvc;

namespace NEG.Website.Controllers
{
    public class ApiController : Controller
    {
        //
        // GET: /API/

        private NEGWebsiteEntities db = new NEGWebsiteEntities();

        public ActionResult Detail()
        {
            var apiId = -1;

            if (int.TryParse(RouteData.Values["id"].ToString(), out apiId))
            {
                ViewData["apiDetailInfo"] = db.APIDetailInfos.FirstOrDefault(m => m.APIID == apiId);
            }

            return View();
        }

    }
}
