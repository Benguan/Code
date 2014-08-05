﻿using System;
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
            string apiID = RouteData.Values["id"].ToString();

            if (!string.IsNullOrWhiteSpace(apiID))
            {
                ViewData["apiDetailInfo"] = db.APIDetailInfos.FirstOrDefault(m => m.APIKey == apiID);
            }

            return View();
        }

    }
}
