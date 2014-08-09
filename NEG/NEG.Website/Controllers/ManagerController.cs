using System.Data.Entity;
using NEG.Website.Models;
using System;
using System.Collections.Generic;
using System.Data.Entity.Infrastructure;
using System.Data.Entity.Validation;
using System.Linq;
using System.Web.Mvc;
using NEG.Website.Models.DataAccess;

namespace NEG.Website.Controllers
{
    public class ManagerController : BaseController
    {
        private NEGDbContext db;

        public ManagerController()
        {
            db = new NEGDbContext();
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

            ViewData["apiCategories"] = db.APICategories.ToList();
            ViewData["apiDetailInfos"] = db.APIDetailInfos.ToList();
            return View();
        }

        public ActionResult API()
        {
            int id = 0;

            if (!int.TryParse(Convert.ToString(RouteData.Values["id"]), out id))
            {
                return View();
            }

            ViewData["IsUpdate"] = id > 0;

            APIDetailInfo detailInfo = db.APIDetailInfos.First(m => m.APIID == id);

            if (detailInfo == null)
            {
                RedirectToAction("APIList", "Manager");
            }

            ViewData["apiDetailInfo"] = detailInfo;

            return View();
        }


        [HttpPost]
        [ValidateInput(false)]
        public ActionResult APIAdd(APIDetailInfo api)
        {
            try
            {
                db.APIDetailInfos.Add(api);
                db.SaveChanges();
            }
            catch (DbEntityValidationException ex)
            {
                throw ex;
            }

            return RedirectToAction("APIList", "Manager");
        }


        [HttpPost]
        [ValidateInput(false)]
        public ActionResult APIUpdate(APIDetailInfo api)
        {
            //Database.SetInitializer(new DropCreateDatabaseIfModelChanges<NEGWebsiteEntities>());

            if (api == null || api.APIID <= 0)
            {
                return RedirectToAction("APIList", "Manager");
            }

            try
            {
                DbEntityEntry<APIDetailInfo> entry = db.Entry<APIDetailInfo>(api);

                entry.State = EntityState.Unchanged;

                entry.Property(m => m.APIKey).IsModified = true;
                entry.Property(m => m.APIName).IsModified = true;
                entry.Property(m => m.CategoryID).IsModified = true;
                entry.Property(m => m.DemoKey).IsModified = true;
                entry.Property(m => m.EventInfo).IsModified = true;
                entry.Property(m => m.Example).IsModified = true;
                entry.Property(m => m.ParameterInfo).IsModified = true;
                entry.Property(m => m.ReturnValue).IsModified = true;
                entry.Property(m => m.Summary).IsModified = true;
                entry.Property(m => m.Syntax).IsModified = true;
                entry.Property(m => m.LANG).IsModified = true;
                entry.Property(m => m.Priority).IsModified = true;

                db.SaveChanges();
            }
            catch (DbEntityValidationException ex)
            {
                throw ex;
            }

            return RedirectToAction("APIList", "Manager");
        }

        #endregion


        #region Module相关,list ,add ,update

        public ActionResult ModuleList()
        {
            ViewData["moduleCategories"] = db.ModuleCategories.ToList();
            ViewData["moduleDetailInfos"] = db.ModuleDetailInfos.ToList();
            return View();
        }

        public ActionResult Module()
        {
            int id = 0;

            if (!int.TryParse(Convert.ToString(RouteData.Values["id"]), out id))
            {
                return View();
            }

            ViewData["IsUpdate"] = id > 0;

            ModuleDetailInfo detailInfo = db.ModuleDetailInfos.First(m => m.ModuleID == id);

            if (detailInfo == null)
            {
                RedirectToAction("ModuleList", "Manager");
            }

            ViewData["moduleDetailInfo"] = detailInfo;

            return View();
        }

        [HttpPost]
        [ValidateInput(false)]
        public ActionResult ModuleAdd(ModuleDetailInfo module)
        {
            try
            {
                db.ModuleDetailInfos.Add(module);
                db.SaveChanges();
            }
            catch (DbEntityValidationException ex)
            {
                throw ex;
            }

            return RedirectToAction("ModuleList", "Manager");
        }

        [HttpPost]
        [ValidateInput(false)]
        public ActionResult ModuleUpdate(ModuleDetailInfo module)
        {
            if (module == null || module.ModuleID <= 0)
            {
                return RedirectToAction("ModuleList", "Manager");
            }

            try
            {
                DbEntityEntry<ModuleDetailInfo> entry = db.Entry<ModuleDetailInfo>(module);

                entry.State = EntityState.Unchanged;

                entry.Property(m => m.ModuleKey).IsModified = true;
                entry.Property(m => m.ModuleName).IsModified = true;
                entry.Property(m => m.CategoryID).IsModified = true;
                entry.Property(m => m.Demokey).IsModified = true;
                entry.Property(m => m.EventInfo).IsModified = true;
                entry.Property(m => m.Example).IsModified = true;
                entry.Property(m => m.ParameterInfo).IsModified = true;
                entry.Property(m => m.ReturnValue).IsModified = true;
                entry.Property(m => m.Summary).IsModified = true;
                entry.Property(m => m.Syntax).IsModified = true;
                entry.Property(m => m.LANG).IsModified = true;
                entry.Property(m => m.Priority).IsModified = true;

                db.SaveChanges();
            }
            catch (DbEntityValidationException ex)
            {
                throw ex;
            }

            return RedirectToAction("ModuleList", "Manager");
        }

        #endregion


    }
}

