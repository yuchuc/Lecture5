#region usings

using NHibernate;

#endregion

namespace Lecture.Domain.Queries
{
  public interface IQuery<out TReturn>
  {
    TReturn Execute(ISession session);
  }
}