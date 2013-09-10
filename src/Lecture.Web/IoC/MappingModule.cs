#region usings

using System.Linq;
using AutoMapper;
using Autofac;
using Lecture.Web.Mappings.TypeConverters;

#endregion

namespace Lecture.Web.IoC
{
  public class MappingModule : Module
  {
    protected override void Load(ContainerBuilder builder)
    {
      builder.Register(c => Mapper.Engine).As<IMappingEngine>();
      RegisterProfiles(builder);
      RegisterTypeConverters(builder);
      RegisterValueResolvers(builder);
    }

    private void RegisterProfiles(ContainerBuilder builder)
    {
      builder.RegisterAssemblyTypes(ThisAssembly).As<Profile>();
    }

    private void RegisterTypeConverters(ContainerBuilder builder)
    {
      builder.RegisterAssemblyTypes(ThisAssembly)
             .Where(t => t.GetInterfaces().Any(it => it.IsGenericType && it.GetGenericTypeDefinition() == typeof (ITypeConverter<,>)))
             .AsImplementedInterfaces()
             .AsSelf();
      builder.RegisterGeneric(typeof (NullableIntegerIdToEntityTypeConverter<>));
      builder.RegisterGeneric(typeof (IntegerIdToEntityTypeConverter<>));
    }

    private void RegisterValueResolvers(ContainerBuilder builder)
    {
      builder.RegisterAssemblyTypes(ThisAssembly)
             .Where(t => t.IsAssignableTo<IValueResolver>())
             .As<IValueResolver>()
             .AsSelf();
    }
  }
}