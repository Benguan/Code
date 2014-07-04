using System.Data.Entity;
using NEG.Website.Models;
using System;
using System.Collections.Generic;
using System.Data.Entity.Infrastructure;
using System.Data.Entity.Validation;
using System.Linq;
using System.Web.Mvc;

namespace NEG.Website.Controllers
{
    public class ManagerController : Controller
    {
        private NEGWebsiteEntities db;

        public ManagerController()
        {
            db = new NEGWebsiteEntities();
        }

        public ActionResult DemoList()
        {
            ViewData["DemoDetailInfos"] = db.DemoDetailInfos.ToList() as List<DemoDetailInfo>;
            return View();
        }

        public ActionResult APIList()
        {
            return View();
        }

        public ActionResult ModuleList()
        {
            return View();
        }

        public ActionResult Demo()
        {
            int id = 0;

            if (!int.TryParse(Convert.ToString(RouteData.Values["id"]), out id))
            {
                return View();
            }

            ViewData["IsUpdate"] = id > 0;

            DemoDetailInfo detailInfo = db.DemoDetailInfos.First(m => m.DemoID == id);

            if (detailInfo == null)
            {
                RedirectToAction("DemoList", "Manager");
            }

            ViewData["demoDetailInfo"] = detailInfo;

            return View();
        }

        [HttpPost]
        [ValidateInput(false)]
        public ActionResult DemoAdd(DemoDetailInfo model)
        {
            //Database.SetInitializer(new DropCreateDatabaseIfModelChanges<NEGWebsiteEntities>());

            try
            {
                db.DemoDetailInfos.Add(model);
                db.SaveChanges();
            }
            catch (DbEntityValidationException ex)
            {
                throw ex;
            }

            return RedirectToAction("DemoList", "Manager");
        }


        [HttpPost]
        [ValidateInput(false)]
        public ActionResult DemoUpdate(DemoDetailInfo model)
        {
            //Database.SetInitializer(new DropCreateDatabaseIfModelChanges<NEGWebsiteEntities>());

            if (model == null || model.DemoID <= 0)
            {
                return RedirectToAction("DemoList", "Manager");
            }

            try
            {
                DbEntityEntry<DemoDetailInfo> entry = db.Entry<DemoDetailInfo>(model);

                entry.State = EntityState.Unchanged;

                entry.Property(m => m.DemoName).IsModified = true;
                entry.Property(m => m.DemoCode).IsModified = true;
                entry.Property(m => m.DemoShowParts).IsModified = true;
                entry.Property(m => m.HtmlCode).IsModified = true;
                entry.Property(m => m.ShowImage).IsModified = true;

                db.SaveChanges();
            }
            catch (DbEntityValidationException ex)
            {
                throw ex;
            }

            return RedirectToAction("DemoList", "Manager");
        }


        public ActionResult API()
        {
            return View();
        }

    }
}

