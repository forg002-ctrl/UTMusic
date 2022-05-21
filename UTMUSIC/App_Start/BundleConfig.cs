using System.Web.Optimization;

namespace UTMUSIC.Web
{
    public static class BundleConfig
    {
        public static void RegisterBundles(BundleCollection bundles)
        {
            //CSS
            bundles.Add(new StyleBundle("~/bundles/bootstrap/css").Include(
                "~/Content/bootstrap.min.css", new CssRewriteUrlTransform()));

            bundles.Add(new StyleBundle("~/bundles/font-awesome/css").Include(
                "~/Content/font-awesome.min.css", new CssRewriteUrlTransform()));

            bundles.Add(new StyleBundle("~/bundles/owl.carousel/css").Include(
                "~/Content/owl.carousel.min.css", new CssRewriteUrlTransform()));

            bundles.Add(new StyleBundle("~/bundles/slicknav/css").Include(
                "~/Content/slicknav.min.css", new CssRewriteUrlTransform()));

            bundles.Add(new StyleBundle("~/bundles/main/css").Include(
                "~/Content/style.css", new CssRewriteUrlTransform()));

            //JS
            bundles.Add(new ScriptBundle("~/bundles/bootstrap/js").Include("~/Scripts/bootstrap.min.js"));

            bundles.Add(new ScriptBundle("~/bundles/jquery/js").Include("~/Scripts/jquery-3.2.1.min.js"));

            bundles.Add(new ScriptBundle("~/bundles/jquery-plagins/js").Include(
                "~/Scripts/jquery.slicknav.min.js",
                "~/Scripts/jquery.jplayer.min.js",
                "~/Scripts/jplayerInit.js"));

            bundles.Add(new ScriptBundle("~/bundles/mixitup/js").Include("~/Scripts/mixitup.min.js")); 

            bundles.Add(new ScriptBundle("~/bundles/owl.carousel/js").Include("~/Scripts/owl.carousel.min.js"));

            bundles.Add(new ScriptBundle("~/bundles/main/js").Include("~/Scripts/main.js"));


            BundleTable.EnableOptimizations = true;
        }
    }
}