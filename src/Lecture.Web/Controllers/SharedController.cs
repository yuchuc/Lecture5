#region usings

using System.Web.Mvc;

#endregion

namespace Lecture.Web.Controllers
{
  public class SharedController : BaseController
  {
    [HttpGet]
    public PartialViewResult ConfirmDelete(string url)
    {
      ViewBag.Url = url;
      return PartialView("_ConfirmDelete");
    }
  }
}