#region usings

using Autofac;
using Lecture.Domain.Orm;
using Lecture.Web.Infrastructure.Authentication;
using Lecture.Web.Infrastructure.Orm;

#endregion

namespace Lecture.Web.IoC
{
  public class InfrastructureModule : Module
  {
    protected override void Load(ContainerBuilder builder)
    {
      RegisterAuthenticationComponents(builder);
      RegisterOrmComponents(builder);
    }

    private static void RegisterAuthenticationComponents(ContainerBuilder builder)
    {
      builder.RegisterType<WebSecurityWrapper>().As<IWebSecurity>();
    }

    private static void RegisterOrmComponents(ContainerBuilder builder)
    {
      builder.RegisterType<ConnectionStringProvider>().As<IConnectionStringProvider>();
    }
  }
}