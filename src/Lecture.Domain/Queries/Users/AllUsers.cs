#region usings

using System.Collections.Generic;
using Lecture.Domain.Entities;
using NHibernate;

#endregion

namespace Lecture.Domain.Queries.Users
{
  public class AllUsers : IQuery<IEnumerable<User>>
  {
    public IEnumerable<User> Execute(ISession session)
    {
      return session.QueryOver<User>().List();
    }
  }
}