#region usings

using System.Web.Optimization;
using BundleTransformer.Core.Bundles;

#endregion

namespace Lecture.Web.App_Start
{
  public static class BundleRegistrar
  {
    public static void Register(BundleCollection bundles)
    {
      var siteCss = new CustomStyleBundle("~/bundles/site_css")
        .Include("~/Content/Css/lib/bootstrap/bootstrap.less")
        .Include("~/Content/Css/modifiers.less");
      bundles.Add(siteCss);

      var datatablesCss = new CustomStyleBundle("~/bundles/datatables_css")
        .Include("~/Content/Css/lib/datatables/datatables.less");
      bundles.Add(datatablesCss);

      var siteJs = new CustomScriptBundle("~/bundles/site_js")
        .Include("~/Scripts/require.js")
        .Include("~/Scripts/require.config.js")
        .Include("~/Scripts/site.init.js");
      bundles.Add(siteJs);
    }
  }
}