﻿using System;
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

            return View();
        }

        public ActionResult About()
        {

            return View();
        }

        public ActionResult API()
        {

            ViewData["apiCategories"] = db.APICategories.ToList();
            ViewData["apiDetailInfos"] = db.APIDetailInfos.ToList();

            return View();
        }

        public ActionResult Demo()
        {
            ViewData["DemoDetailInfos"] = db.DemoDetailInfos.ToList();
            return View();
        }

        public ActionResult Module()
        {
            return View();
        }
    }
}
