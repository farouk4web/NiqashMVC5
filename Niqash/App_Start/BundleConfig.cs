using System.Web;
using System.Web.Optimization;

namespace Niqash
{
    public class BundleConfig
    {
        // For more information on bundling, visit https://go.microsoft.com/fwlink/?LinkId=301862
        public static void RegisterBundles(BundleCollection bundles)
        {
            bundles.Add(new ScriptBundle("~/bundles/js").Include(
                        "~/Scripts/jquery-{version}.js",
                        "~/Scripts/bootstrap.js",
                        "~/Scripts/main.js"
                        ));



            bundles.Add(new ScriptBundle("~/bundles/jqueryval").Include(
                        "~/Scripts/jquery.validate*"));

            bundles.Add(new ScriptBundle("~/bundles/postCalls").Include(
                        "~/Scripts/apiCalls/postCalls.js"));
            
            bundles.Add(new ScriptBundle("~/bundles/postsCalls").Include(
                        "~/Scripts/apiCalls/postsCalls.js"));

            bundles.Add(new ScriptBundle("~/bundles/accountCalls").Include(
                        "~/Scripts/apiCalls/accountCalls.js"));

            bundles.Add(new ScriptBundle("~/bundles/editProfileCalls").Include(
                        "~/Scripts/apiCalls/editProfileCalls.js"));

            bundles.Add(new ScriptBundle("~/bundles/changePasswordCalls").Include(
                        "~/Scripts/apiCalls/changePasswordCalls.js"));



            // Use the development version of Modernizr to develop with and learn from. Then, when you're
            // ready for production, use the build tool at https://modernizr.com to pick only the tests you need.
            bundles.Add(new ScriptBundle("~/bundles/modernizr").Include(
                        "~/Scripts/modernizr-*"));

            bundles.Add(new StyleBundle("~/Content/css").Include(
                      "~/Content/bootstrap.css",
                      "~/Content/site.css"));


            bundles.Add(new StyleBundle("~/Content/rtl").Include(
                      "~/Content/bootstrap-rtl.css",
                      "~/Content/site.css",
                      "~/Content/site-rtl.css"));
        }
    }
}
