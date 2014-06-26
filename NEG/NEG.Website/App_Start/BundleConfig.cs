using System.Web;
using System.Web.Optimization;

namespace NEG.Website
{
    public class BundleConfig
    {
        public static void RegisterBundles(BundleCollection bundles)
        {
            bundles.Add(new ScriptBundle("~/Scripts/CommonJS").Include(
                "~/Scripts/GooglePretty/prettify.js"
                ));

            bundles.Add(new StyleBundle("~/Content/CommonCSS").Include(
                      "~/Content/NEG.css",
                      "~/Content/neg_site.css"));
        }
    }
}