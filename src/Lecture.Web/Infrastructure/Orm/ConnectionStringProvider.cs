#region usings

using System.Configuration;
using Lecture.Domain.Orm;

#endregion

namespace Lecture.Web.Infrastructure.Orm
{
  public class ConnectionStringProvider : IConnectionStringProvider
  {
    public string ConnectionString
    {
      get { return ConfigurationManager.ConnectionStrings["Primary"].ConnectionString; }
    }

    public string ProviderName
    {
      get { return "System.Data.SqlClient"; }
    }
  }
}