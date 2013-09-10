#region usings

using System;
using Lecture.Domain.Entities;
using NHibernate;

#endregion

namespace Lecture.Domain.Queries.Users
{
  public class UserWithEmailAlreadyExists : IQuery<bool>
  {
    private readonly int? _id;
    private readonly string _email;

    public UserWithEmailAlreadyExists(string email, int? id = null)
    {
      _id = id;
      _email = email;
    }

    public Boolean Execute(ISession session)
    {
      var query = session.QueryOver<User>().Where(u => u.Email == _email);
      if (_id != null) query.And(u => u.Id != _id);
      return query.RowCount() > 0;
    }
  }
}