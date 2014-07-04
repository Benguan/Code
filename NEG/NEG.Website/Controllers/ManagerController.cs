
using System.Collections.Generic;
using NEG.Website.Models;
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
            return View();
        }

        [HttpPost]
        [ValidateInput(false)]
        public ActionResult Demo(DemoDetailInfo model)
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

            return View();
        }


        public ActionResult API()
        {
            return View();
        }

    }
}

