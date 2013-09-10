#region usings

using AutoMapper;
using NHibernate;

#endregion

namespace Lecture.Web.Mappings.TypeConverters
{
  public class IntegerIdToEntityTypeConverter<TEntity> : ITypeConverter<int, TEntity>
  {
    private readonly ISession _session;

    public IntegerIdToEntityTypeConverter(ISession session)
    {
      _session = session;
    }

    public TEntity Convert(ResolutionContext context)
    {
      var entity = _session.Get<TEntity>(context.SourceValue);
      return entity;
    }
  }
}