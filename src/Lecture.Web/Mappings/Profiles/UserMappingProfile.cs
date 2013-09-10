#region usings

using Lecture.Domain.Entities;
using Lecture.Web.Areas.Admin.Models;
using Lecture.Web.Mappings.TypeConverters;
using Lecture.Web.Mappings.ValueResolvers;

#endregion

namespace Lecture.Web.Mappings.Profiles
{
  public class UserMappingProfile : EntityMappingProfile
  {
    protected override void Configure()
    {
      CreateMap<UserForm, User>()
        .ConstructUsing(rc => GetOrCreateEntity<UserForm, User>(rc, uf => uf.Id));

      CreateMap<User, UserForm>()
        .ForMember(uf => uf.Roles, mce => mce.ResolveUsing<UserRolesResolver>())
        .ForMember(uf => uf.Password, mce => mce.Ignore())
        .ForMember(uf => uf.ConfirmPassword, mce => mce.Ignore())
        .ForMember(uf => uf.RoleSelectListItems, mce => mce.Ignore());

      CreateMap<int?, User>().ConvertUsing<NullableIntegerIdToEntityTypeConverter<User>>();
      CreateMap<int, User>().ConvertUsing<IntegerIdToEntityTypeConverter<User>>();
      CreateMap<User, int?>().ConvertUsing(u => u != null ? u.Id : (int?) null);
      CreateMap<User, int>().ConvertUsing(u => u.Id);
    }
  }
}