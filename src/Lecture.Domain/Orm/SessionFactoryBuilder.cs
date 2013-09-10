#region usings

using Lecture.Domain.Entities;
using NHibernate;
using NHibernate.Bytecode;
using NHibernate.Cfg;
using NHibernate.Dialect;
using NHibernate.Mapping.ByCode;

#endregion

namespace Lecture.Domain.Orm
{
  public class SessionFactoryBuilder : ISessionFactoryBuilder
  {
    private readonly IConnectionStringProvider _connectionStringProvider;

    public SessionFactoryBuilder(IConnectionStringProvider connectionStringProvider)
    {
      _connectionStringProvider = connectionStringProvider;
    }

    public ISessionFactory BuildISessionFactory()
    {
      var mapper = new ModelMapper();
      mapper.AddMappings(typeof (User).Assembly.GetExportedTypes());
      var mapping = mapper.CompileMappingForAllExplicitlyAddedEntities();

      var configuration = new Configuration()
        .Proxy(p => p.ProxyFactoryFactory<DefaultProxyFactoryFactory>())
        .DataBaseIntegration(
          di =>
            {
              di.ConnectionString = _connectionStringProvider.ConnectionString;
              di.Dialect<MsSql2008Dialect>();
            });
      configuration.AddMapping(mapping);

      var sessionFactory = configuration.BuildSessionFactory();
      return sessionFactory;
    }
  }
}