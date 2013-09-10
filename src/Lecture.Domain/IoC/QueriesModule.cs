#region usings

using Autofac;
using Lecture.Domain.Queries;

#endregion

namespace Lecture.Domain.IoC
{
  public class QueriesModule : Module
  {
    protected override void Load(ContainerBuilder builder)
    {
      builder.RegisterType<QueryExecutor>().As<IQueryExecutor>();
    }
  }
}