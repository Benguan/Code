using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using NEG.Website.Models;

namespace NEG.Website.Controllers
{
    public class ModuleController : Controller
    {
        //
        // GET: /Module/

        private NEGWebsiteEntities db = new NEGWebsiteEntities();

        public ActionResult Detail()
        {
            string moduleId = RouteData.Values["id"].ToString();

            if (!string.IsNullOrWhiteSpace(moduleId))
            {
                ViewData["moduleDetailInfo"] = db.ModuleDetailInfos.FirstOrDefault(m => m.ModuleKey == moduleId);
            }

            return View();
        }

    }
}
