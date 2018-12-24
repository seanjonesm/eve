﻿using System.Web;
using System.Web.Optimization;

namespace Eve4SalesApp
{
    public class BundleConfig
    {
        // For more information on bundling, visit http://go.microsoft.com/fwlink/?LinkId=301862
        public static void RegisterBundles(BundleCollection bundles)
        {

            bundles.Add(new ScriptBundle("~/bundles/eve").Include(
                    "~/Scripts/jquery-{version}.js",
                    "~/Scripts/jquery-ui-{version}.js",
                    "~/Scripts/jquery.unobtrusive*",
                    "~/Scripts/jquery.validate*",
                    "~/Scripts/simple-skillbar.js",
                    "~/Scripts/eve.js")); 




            bundles.Add(new ScriptBundle("~/bundles/jquery").Include(
                        "~/Scripts/jquery-{version}.js"));

            bundles.Add(new ScriptBundle("~/bundles/jqueryval").Include(
                        "~/Scripts/jquery.validate*"));

            // Use the development version of Modernizr to develop with and learn from. Then, when you're
            // ready for production, use the build tool at http://modernizr.com to pick only the tests you need.
            bundles.Add(new ScriptBundle("~/bundles/modernizr").Include(
                        "~/Scripts/modernizr-*"));

            bundles.Add(new ScriptBundle("~/bundles/bootstrap").Include(
                      "~/Scripts/bootstrap.js",
                      "~/Scripts/respond.js",
                       "~/Scripts/bootstrap.js"));

            bundles.Add(new StyleBundle("~/Content/css").Include(
                      "~/Content/bootstrap.css",
                      "~/Content/Site.css",
                      "~/Content/checkbox-bootstrap.css",
                      "~/Content/checkbox-bootstrap-na.css"
                      ));
                               }
    }
}
