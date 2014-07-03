using System.Web;
using System.Web.Optimization;

namespace NEG.Website
{
    public class BundleConfig
    {
        public static void RegisterBundles(BundleCollection bundles)
        {
            bundles.Add(new ScriptBundle("~/Scripts/CommonJS").Include(
                "~/Scripts/GooglePretty/prettify.js",
                "~/Scripts/jquery1.7.min.js"
                ));

            bundles.Add(new StyleBundle("~/Content/CommonCSS").Include(
                 "~/Content/neg_site.css",
                "~/Content/GooglePretty/prettify.css"
               ));

            bundles.Add(new StyleBundle("~/Content/DemoCSS").Include(
                "~/Content/Demo.css"
              ));
        }
    }
}