#region usings

using System.Web.Mvc;
using System.Web.Routing;

#endregion

namespace Lecture.Web.App_Start
{
  public static class RouteRegistrar
  {
    public static void Register(RouteCollection routes)
    {
      routes.IgnoreRoute("{resource}.axd/{*pathInfo}");

      routes.MapRoute(
        name: "SignIn",
        url: "signin",
        defaults: new {controller = "Authentication", action = "SignIn"});

      routes.MapRoute(
        name: "SignOut",
        url: "Signout",
        defaults: new {controller = "Authentication", action = "SignOut"});

      routes.MapRoute(
        name: "Default",
        url: "{controller}/{action}/{id}",
        defaults: new {controller = "Home", action = "Index", id = UrlParameter.Optional});
    }
  }
}