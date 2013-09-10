#region usings

using System;
using System.Web;
using System.Web.Mvc;
using Elmah;

#endregion

namespace Lecture.Web.Infrastructure.Filters
{
  public class ElmahExceptionFilter : HandleErrorAttribute
  {
    public override void OnException(ExceptionContext context)
    {
      base.OnException(context);
      if (context.ExceptionHandled)
      {
        LogException(context.Exception);
      }
    }

    public static void LogException(Exception exception)
    {
      var httpContext = HttpContext.Current;
      if (httpContext != null && !RaiseErrorSignal(exception) && !IsFiltered(exception))
      {
        ErrorLog.GetDefault(httpContext).Log(new Error(exception, httpContext));
      }
    }

    private static bool RaiseErrorSignal(Exception exception)
    {
      var httpContext = HttpContext.Current;
      var signal = ErrorSignal.FromContext(httpContext);
      if (signal == null) return false;

      signal.Raise(exception, httpContext);
      return true;
    }

    private static bool IsFiltered(Exception exception)
    {
      var httpContext = HttpContext.Current;
      var config = httpContext.GetSection("elmah/errorFilter") as ErrorFilterConfiguration;
      return config != null && config.Assertion.Test(new ErrorFilterModule.AssertionHelperContext(exception, httpContext));
    }
  }
}