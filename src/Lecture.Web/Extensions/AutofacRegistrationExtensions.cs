#region usings

using System;
using System.Linq;
using System.Reflection;
using System.Web.Mvc;
using Autofac;
using Autofac.Builder;
using Autofac.Features.Scanning;
using Lecture.Web.Models.Validation.IoC;

#endregion

namespace Lecture.Web.Extensions
{
  public static class AutofacRegistrationExtensions
  {
    public static void RegisterModelValidatorProvider(this ContainerBuilder builder)
    {
      builder.RegisterType<AutofacModelValidatorProvider>()
             .As<ModelValidatorProvider>()
             .SingleInstance();
    }

    public static IRegistrationBuilder<object, ScanningActivatorData, DynamicRegistrationStyle> RegisterModelValidators(this ContainerBuilder builder, params Assembly[] modelValidatorAssemblies)
    {
      return builder.RegisterAssemblyTypes(modelValidatorAssemblies)
                    .Where(type => typeof (ModelValidator).IsAssignableFrom(type) && type.GetCustomAttributes(typeof (ModelValidatorTypeAttribute), true).Length > 0)
                    .As<ModelValidator>()
                    .WithMetadata(AutofacModelValidatorProvider.MetadataKey,
                                  type => (from ModelValidatorTypeAttribute attribute in type.GetCustomAttributes(typeof (ModelValidatorTypeAttribute), true)
                                           from targetType in attribute.TargetTypes
                                           select targetType).ToList());
    }

    public static IRegistrationBuilder<TLimit, TActivatorData, TRegistrationStyle> AsModelValidatorForTypes<TLimit, TActivatorData, TRegistrationStyle>(this IRegistrationBuilder<TLimit, TActivatorData, TRegistrationStyle> registration, params Type[] types)
      where TActivatorData : IConcreteActivatorData
      where TRegistrationStyle : SingleRegistrationStyle
    {
      var typeList = types.Where(type => type != null).ToList();
      return registration.As<ModelValidator>().WithMetadata(AutofacModelValidatorProvider.MetadataKey, typeList);
    }
  }
}