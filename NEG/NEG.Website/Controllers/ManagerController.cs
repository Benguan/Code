using System;
using System.Data.Entity;
using System.Data.Entity.Validation;
using System.Web.Mvc;
using NEG.Website.Models;

namespace NEG.Website.Controllers
{
    public class ManagerController : Controller
    {
        private NEGWebsiteEntities db;

        public ManagerController()
        {
            db = new NEGWebsiteEntities();
        }


        public ActionResult Demo()
        {
            return View();
        }

        [HttpPost]
        [ValidateInput(false)]
        public ActionResult Demo(DemoDetailInfo model)
        {
            Database.SetInitializer(new Drop);
            

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

