#region usings

using System.Collections.Generic;
using Lecture.Domain.Entities;

#endregion

namespace Lecture.Web.Infrastructure.Authentication
{
  public interface IWebSecurity
  {
    int CurrentUserId { get; }
    string CurrentUsername { get; }
    User GetCurrentUser();
    IEnumerable<string> GetRolesForUser(string username);
    void SetRolesForUser(string username, IEnumerable<string> roles);
    bool SignIn(string username, string password, bool persistCookie = false);
    void SignOut();
    void CreateAccount(string username, string password, bool requireConfirmationToken = false);
    void ResetPasswordWithoutToken(string username, string newPassword);
  }
}