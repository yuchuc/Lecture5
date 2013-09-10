#region usings

using System.Web;
using System.Web.Mvc;

#endregion

namespace Lecture.Web.HtmlHelpers
{
  public static class EmailHtmlHelpers
  {
    // An extension method must be a static method in a static class and it's first parameter is the class whose
    // instances will be extended. It is preceded by the this keyword. ASP.NET MVC uses extension methods pretty
    // much exclusively for Html Helpers.
    public static IHtmlString MailToLink(this HtmlHelper htmlHelper, string email, object htmlAttributes = null)
    {
      var a = new TagBuilder("a") {InnerHtml = htmlHelper.Encode(email)};
      a.MergeAttributes(HtmlHelper.AnonymousObjectToHtmlAttributes(htmlAttributes));
      a.MergeAttribute("href", "mailto:" + htmlHelper.Encode(email));
      return new HtmlString(a.ToString());
    }
  }
}