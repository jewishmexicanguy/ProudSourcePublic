using System.Web;
using System.Web.Optimization;

namespace ProudSourcePrime.App_Start
{
    public class BundleConfig
    {
        public static void RegisterBundles(BundleCollection bundles)
        {
            bundles.Add(
                new ScriptBundle("~/bundles/jquery").Include(
                    "~/scripts/jquery-{version}.js"
                )
            );

            bundles.Add(
                new ScriptBundle("~/bundles/bootstrap").Include(
                    "~/scripts/bootstrap.js",
                    "~/scripts/respond.js"
                )
            );

            bundles.Add(
                new StyleBundle("~/Content/css").Include(
                    "~/Content/bootstrap.css",
                    "~/Styles/global.css"
                )
            );

            bundles.Add(
                new ScriptBundle("~/bundles/datepicker").Include(
                    "~/scripts/bootstrap-datepicker.js"
                )
            );

            bundles.Add(
                new ScriptBundle("~/bundles/serendipia").Include(
                    "~/scripts/SerendipiaAnimation.js"
                )
            );

            bundles.Add(
                new ScriptBundle("~/bundles/shapley").Include(
                    "~/scripts/Shapley.js"
                )
            );
        }
    }
}