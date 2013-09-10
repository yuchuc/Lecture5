#region usings

using Autofac;
using Autofac.Integration.Mvc;
using Lecture.Web.Controllers;
using Lecture.Web.Extensions;
using Lecture.Web.Infrastructure.Filters;

#endregion

namespace Lecture.Web.IoC
{
  public class MvcModule : Module
  {
    protected override void Load(ContainerBuilder builder)
    {
      builder.RegisterModule<AutofacWebTypesModule>();
      RegisterControllers(builder);
      RegisterFilters(builder);
      RegisterModelBinders(builder);
      RegisterModelValidators(builder);
      RegisterGridDataBuilders(builder);
    }

    private void RegisterControllers(ContainerBuilder builder)
    {
      builder.RegisterControllers(ThisAssembly);
    }

    private static void RegisterFilters(ContainerBuilder builder)
    {
      builder.RegisterType<NHibernateTransactionFilter>()
             .AsActionFilterFor<BaseNHibernateController>(order: 0)
             .InstancePerHttpRequest();

      builder.RegisterFilterProvider();
    }

    private void RegisterModelBinders(ContainerBuilder builder)
    {
      builder.RegisterModelBinders(ThisAssembly);
      builder.RegisterModelBinderProvider();
    }

    private void RegisterModelValidators(ContainerBuilder builder)
    {
      builder.RegisterModelValidators(ThisAssembly);
      builder.RegisterModelValidatorProvider();
    }

    private void RegisterGridDataBuilders(ContainerBuilder builder)
    {
      builder.RegisterAssemblyTypes(ThisAssembly)
             .Where(t => t.Name.EndsWith("GridDataBuilder"));
    }
  }
}