using System.Web.Optimization;

namespace BlackJack.UI
{
    public class BundleConfig
    {

        public static void RegisterBundles(BundleCollection bundles)
        {
            bundles.UseCdn = true;           

            bundles.Add(new ScriptBundle("~/bundles/jquery").Include(                       
                        "~/Scripts/jquery-{version}.js"));

             bundles.Add(new ScriptBundle("~/bundles/mybundle").Include(
                        "~/Scripts/MyBundle/select2.min.js"));

            bundles.Add(new ScriptBundle("~/bundles/jqueryval").Include(
                        "~/Scripts/jquery.validate*"));

            bundles.Add(new ScriptBundle("~/bundles/modernizr").Include(
                        "~/Scripts/modernizr-*"));

            bundles.Add(new ScriptBundle("~/bundles/bootstrap").Include(
                      "~/Scripts/bootstrap.js"));

           
            bundles.Add(new StyleBundle("~/Content/css").Include(
                        "~/Content/css/select2.min.css",
                        "~/Content/bootstrap.css",                      
                        "~/Content/site.css"));
        }
    }
}
