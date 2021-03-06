﻿using System.Web;
using System.Web.Optimization;

namespace SceneOfCustoms
{
    public class BundleConfig
    {
        // 有关绑定的详细信息，请访问 http://go.microsoft.com/fwlink/?LinkId=301862
        public static void RegisterBundles(BundleCollection bundles)
        {
            bundles.Add(new ScriptBundle("~/bundles/jquery").Include(
                        "~/Scripts/jquery-{version}.js"));

            bundles.Add(new ScriptBundle("~/bundles/jqueryval").Include(
                        "~/Scripts/jquery.validate*"));

            // 使用要用于开发和学习的 Modernizr 的开发版本。然后，当你做好
            // 生产准备时，请使用 http://modernizr.com 上的生成工具来仅选择所需的测试。
            bundles.Add(new ScriptBundle("~/bundles/modernizr").Include(
                        "~/Scripts/modernizr-*"));

            bundles.Add(new ScriptBundle("~/bundles/bootstrap").Include(
                      "~/Scripts/bootstrap.js",
                      "~/Scripts/respond.js"));

            bundles.Add(new StyleBundle("~/Content/css").Include(
                      "~/Content/bootstrap.css",
                      "~/Content/site.css"));

            bundles.Add(new StyleBundle("~/font-awesome/css").Include(
                      "~/fonts/font-awesome/css/font-awesome.min.css",

                      new CssRewriteUrlTransform()));

            bundles.Add(new StyleBundle("~/font-awesome3.2/css").Include(
                      "~/fonts/font-awesome.3.2/css/font-awesome.min.css",

                      new CssRewriteUrlTransform()));

            //easyui 1.4.5
            bundles.Add(new StyleBundle("~/easyui/css").Include(
                     "~/js/easyui/themes/metro/easyui.css"));

            bundles.Add(new ScriptBundle("~/easyui/js").Include(
                      "~/js/easyui/jquery.easyui.min.js",
                      "~/js/easyui/plugins/datagrid-filter.js",
                      "~/js/easyui/plugins/jquery.edatagrid.js",
                      "~/js/easyui/locale/easyui-lang-zh_CN.js",
                      "~/js/FileSaver.js",
                      "~/js/jQuery.print.js"
                      ));



        }
    }
}
