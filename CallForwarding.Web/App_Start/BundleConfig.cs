using System.Web.Optimization;

namespace CallForwarding.Web
{
    public class BundleConfig
    {
        // For more information on bundling, visit http://go.microsoft.com/fwlink/?LinkId=301862
        public static void RegisterBundles(BundleCollection bundles)
        {
            bundles.Add(new ScriptBundle("~/bundles/scripts").Include(
                      "~/Scripts/lib/dist/js/jquery.js",
                      "~/Scripts/lib/dist/js/bootstrap.js"));

            bundles.Add(new StyleBundle("~/Content/css").Include(
                      "~/Scripts/lib/dist/css/bootstrap.css",
                      "~/Scripts/lib/dist/css/font-awesome.css",
                      "~/Content/css/main.css"));
        }
    }
}
