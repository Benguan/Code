﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Web;
using System.Web.Mvc;
using NEG.Website.Controls.Common;
using NEG.Website.Models;
using NEG.Website.Models.DataAccess;

namespace NEG.Website.Controllers
{
    public class HomeController : BaseController
    {
        private NEGDbContext db;

        public HomeController()
        {
            db = new NEGDbContext();
        }

        public ActionResult Index()
        {
            return View();
        }

        public ActionResult Tutorial()
        {

            return View("Index");
        }

        public ActionResult About()
        {

            return View();
        }

        public ActionResult API()
        {
            ViewData["apiCategories"] = db.APICategories.OrderBy(m => m.Priority).ToList();
            ViewData["apiDetailInfos"] = db.APIDetailInfos.Where(m =>
                                                                 m.Status && m.LANG == ResourceManager.LANG_DEFAULT).ToList();

            return View();
        }

        public ActionResult Demo()
        {
            ViewData["DemoDetailInfos"] = db.DemoDetailInfos.Where(m => m.Status).ToList();
            return View();
        }

        public ActionResult Module()
        {
            ViewData["moduleCategories"] = db.ModuleCategories.ToList();
            ViewData["moduleDetailInfos"] = db.ModuleDetailInfos.Where(m => m.Status).ToList();
            return View();
        }


    }
}
