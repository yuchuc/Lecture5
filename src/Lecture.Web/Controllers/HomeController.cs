#region usings

using System.Linq;
using System.Web.Mvc;
using Lecture.Domain.Queries;
using Lecture.Domain.Queries.Users;
using NHibernate;

#endregion

namespace Lecture.Web.Controllers
{
  public class HomeController : BaseNHibernateController
  {
    private readonly IQueryExecutor _queryExecutor;

    public HomeController(ISession nhSession, IQueryExecutor queryExecutor)
      : base(nhSession)
    {
      _queryExecutor = queryExecutor;
    }

    [HttpGet]
    public ActionResult Index()
    {
      return View();
    }
  }
}
