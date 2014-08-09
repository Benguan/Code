using NEG.Website.Controls.Common;
using NEG.Website.Models;
using NEG.Website.Models.DataAccess;
using System.Web.Mvc;

namespace NEG.Website.Controllers
{
    public class ApiController : BaseController
    {
        public ActionResult Detail()
        {
            string apiID = RouteData.Values["id"].ToString();

            if (!string.IsNullOrWhiteSpace(apiID))
            {
                APIDetailInfo info = DBHelper.APIDetailInfoContext.FirstOrDefault(
                    m => m.APIKey == apiID,
                    m => m.LANG == CurrentLang);

                ViewData["apiDetailInfo"] = info;
            }

            return View();
        }
    }
}
