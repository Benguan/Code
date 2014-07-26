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

        #region Demo相关，list，add，update
        public ActionResult DemoList()
        {
            ViewData["DemoDetailInfos"] = db.DemoDetailInfos.ToList() as List<DemoDetailInfo>;
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
        #endregion


        #region API相关，list, add, update

        public ActionResult APIList()
        {
            return View();
        }

        public ActionResult API()
        {
            return View();
        }

        #endregion


        public ActionResult ModuleList()
        {

            InitDB();
            return View();
        }

        public void InitDB()
        {
            Database.SetInitializer(new DropCreateDatabaseIfModelChanges<NEGWebsiteEntities>());
            db.APICategories.Add(new APICategory("UTILITY", 2));
            db.APICategories.Add(new APICategory("Base", 1));

            db.ModuleCategories.Add(new ModuleCategory("THIRD PARTY", 1));
            db.ModuleCategories.Add(new ModuleCategory("UTILITY", 2));
            db.ModuleCategories.Add(new ModuleCategory("WIDGET", 3));

            db.SaveChanges();
        }

    }
}

