#region usings

using System;
using System.Linq;
using System.Web.Security;
using Lecture.Domain.Entities;
using Lecture.Domain.Orm;
using WebMatrix.WebData;

#endregion

namespace Lecture.Web.App_Start
{
  public static class AuthConfig
  {
    public static void Configure(IConnectionStringProvider connectionStringProvider)
    {
      WebSecurity.InitializeDatabaseConnection(connectionStringProvider.ConnectionString, connectionStringProvider.ProviderName, "User", "Id", "Email", false);

      SynchronizeRoles();
    }

    private static void SynchronizeRoles()
    {
      var newRoles = SystemRoles.Roles.Where(role => !Roles.RoleExists(role));
      var deletedRoles = Roles.GetAllRoles().Where(role => !SystemRoles.Roles.Contains(role, StringComparer.OrdinalIgnoreCase));

      foreach (var role in newRoles) Roles.CreateRole(role);
      foreach (var role in deletedRoles) Roles.DeleteRole(role, true);
    }
  }
}