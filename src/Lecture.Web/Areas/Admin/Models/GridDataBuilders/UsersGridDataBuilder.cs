#region usings

using System;
using System.Web.Mvc;
using Lecture.Domain.Entities;
using Lecture.Domain.Queries;
using Lecture.Domain.Queries.Users;
using Lecture.Web.Extensions;
using Lecture.Web.Models;

#endregion

namespace Lecture.Web.Areas.Admin.Models.GridDataBuilders
{
  public class UsersGridDataBuilder
  {
    private readonly UrlHelper _url;
    private readonly IQueryExecutor _queryExecutor;

    public UsersGridDataBuilder(UrlHelper url, IQueryExecutor queryExecutor)
    {
      _url = url;
      _queryExecutor = queryExecutor;
    }

    public DataTablesGridData BuildGridData()
    {
      var users = _queryExecutor.ExecuteQuery(new AllUsers());
      return users.BuildGridData(new Func<User, object>[]
        {
          x => x.Email,
          x => new
            {
              EditUrl = _url.Action("Edit", "Users", new {id = x.Id}),
              DeleteUrl = _url.Action("Delete", "Users", new {id = x.Id})
            }
        });
    }
  }
}