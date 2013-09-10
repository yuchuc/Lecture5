#region usings

using System.Collections.Generic;
using AutoMapper;

#endregion

namespace Lecture.Web.App_Start
{
  public static class AutoMapperConfig
  {
    public static void Configure(IEnumerable<Profile> profiles)
    {
      Mapper.Initialize(cfg =>
        {
          foreach (var profile in profiles) cfg.AddProfile(profile);
        });
    }
  }
}