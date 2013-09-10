#region usings

using System.Collections.Generic;
using AutoMapper;
using Lecture.Domain.Entities;
using Lecture.Web.Infrastructure.Authentication;

#endregion

namespace Lecture.Web.Mappings.ValueResolvers
{
  public class UserRolesResolver : ValueResolver<User, IEnumerable<string>>
  {
    private readonly IWebSecurity _webSecurity;

    public UserRolesResolver(IWebSecurity webSecurity)
    {
      _webSecurity = webSecurity;
    }

    protected override IEnumerable<string> ResolveCore(User source)
    {
      return _webSecurity.GetRolesForUser(source.Email);
    }
  }
}