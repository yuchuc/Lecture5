#region usings

using System.Web.Mvc;
using AutoMapper;

#endregion

namespace Lecture.Web.Mappings.Profiles
{
  public class StringMappingProfile : Profile
  {
    protected override void Configure()
    {
      CreateMap<string, SelectListItem>().ConvertUsing(s => new SelectListItem {Text = s, Value = s});
    }
  }
}