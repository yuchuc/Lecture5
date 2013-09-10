#region usings

using System.Web.Mvc;
using Lecture.Web.Infrastructure.Filters;

#endregion

namespace Lecture.Web.Controllers
{
  public abstract class BaseController : Controller
  {
    protected override void OnException(ExceptionContext exceptionContext)
    {
      if (exceptionContext.ExceptionHandled)
      {
        ElmahExceptionFilter.LogException(exceptionContext.Exception);
      }

      base.OnException(exceptionContext);
    }
  }
}