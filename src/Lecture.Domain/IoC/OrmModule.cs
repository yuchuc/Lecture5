#region usings

using Autofac;
using Lecture.Domain.Orm;
using NHibernate;

#endregion

namespace Lecture.Domain.IoC
{
  public class OrmModule : Module
  {
    protected override void Load(ContainerBuilder builder)
    {
      builder.RegisterType<SessionFactoryBuilder>().As<ISessionFactoryBuilder>();
      builder.Register(c => c.Resolve<ISessionFactoryBuilder>().BuildISessionFactory()).SingleInstance();
      builder.Register(c => c.Resolve<ISessionFactory>().OpenSession()).InstancePerLifetimeScope();
    }
  }
}