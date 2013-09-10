#region usings

using NHibernate;

#endregion

namespace Lecture.Domain.Orm
{
  public interface ISessionFactoryBuilder
  {
    ISessionFactory BuildISessionFactory();
  }
}