#region usings

using System.Web.Mvc;
using NHibernate;

#endregion

namespace Lecture.Web.Infrastructure.Filters
{
  public class NHibernateTransactionFilter : IActionFilter
  {
    private readonly ISession _nhSession;

    public NHibernateTransactionFilter(ISession nhSession)
    {
      _nhSession = nhSession;
    }

    public void OnActionExecuting(ActionExecutingContext filterContext)
    {
      _nhSession.Transaction.Begin();
    }

    public void OnActionExecuted(ActionExecutedContext filterContext)
    {
      if (filterContext.Exception == null && _nhSession.Transaction.IsActive)
      {
        _nhSession.Transaction.Commit();
      }
    }
  }
}