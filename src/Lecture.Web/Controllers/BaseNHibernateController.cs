#region usings

using System.Web.Mvc;
using NHibernate;

#endregion

namespace Lecture.Web.Controllers
{
  public abstract class BaseNHibernateController : BaseController
  {
    protected readonly ISession NhSession;

    protected BaseNHibernateController(ISession nhSession)
    {
      NhSession = nhSession;
    }

    protected override void OnException(ExceptionContext exceptionContext)
    {
      base.OnException(exceptionContext);

      if (NhSession.Transaction.IsActive)
      {
        NhSession.Transaction.Rollback();
      }
    }
  }
}