#region usings

using System.Collections.Generic;
using System.Linq;
using System.Web.Security;
using Lecture.Domain.Entities;
using NHibernate;
using WebMatrix.WebData;

#endregion

namespace Lecture.Web.Infrastructure.Authentication
{
  public class WebSecurityWrapper : IWebSecurity
  {
    private readonly ISession _nhSession;

    public WebSecurityWrapper(ISession nhSession)
    {
      _nhSession = nhSession;
    }

    public int CurrentUserId
    {
      get { return WebSecurity.CurrentUserId; }
    }

    public string CurrentUsername
    {
      get { return WebSecurity.CurrentUserName; }
    }

    public User GetCurrentUser()
    {
      return WebSecurity.IsAuthenticated && WebSecurity.HasUserId ? _nhSession.Get<User>(WebSecurity.CurrentUserId) : null;
    }

    public IEnumerable<string> GetRolesForUser(string username)
    {
      return Roles.GetRolesForUser(username);
    }

    public void SetRolesForUser(string username, IEnumerable<string> roles)
    {
      var currentRoles = GetRolesForUser(username);

      var removedRoles = currentRoles.Except(roles);
      if (removedRoles.Any())
      {
        Roles.RemoveUserFromRoles(username, removedRoles.ToArray());
      }

      var addedRoles = roles.Except(currentRoles);
      if (addedRoles.Any())
      {
        Roles.AddUserToRoles(username, addedRoles.ToArray());
      }
    }

    public bool SignIn(string username, string password, bool persistCookie = false)
    {
      return WebSecurity.Login(username, password, persistCookie);
    }

    public void SignOut()
    {
      WebSecurity.Logout();
    }

    public void CreateAccount(string username, string password, bool requireConfirmationToken = false)
    {
      WebSecurity.CreateAccount(username, password, requireConfirmationToken);
    }

    public void ResetPassword(string passwordResetToken, string newPassword)
    {
      WebSecurity.ResetPassword(passwordResetToken, newPassword);
    }

    public void ResetPasswordWithoutToken(string username, string newPassword)
    {
      var passwordResetToken = WebSecurity.GeneratePasswordResetToken(username, 1);
      ResetPassword(passwordResetToken, newPassword);
    }
  }
}