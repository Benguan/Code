using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using NEG.Website.Models;
using NEG.Website.Models.DataAccess;

namespace NEG.Website.Controllers
{
    public class ModuleController : BaseController
    {
        public ActionResult Detail()
        {
            string moduleId = RouteData.Values["id"].ToString();

            if (!string.IsNullOrWhiteSpace(moduleId))
            {
                ViewData["moduleDetailInfo"] = DBHelper.ModuleDetailInfoContext.FirstOrDefault(m => m.ModuleKey == moduleId,
                                                                                               m => m.LANG == CurrentLang);
            }

            return View();
        }

    }
}
