#region usings

using AutoMapper;
using NHibernate;

#endregion

namespace Lecture.Web.Mappings.TypeConverters
{
  public class NullableIntegerIdToEntityTypeConverter<TEntity> : ITypeConverter<int?, TEntity>
    where TEntity : class, new()
  {
    private readonly ISession _session;

    public NullableIntegerIdToEntityTypeConverter(ISession session)
    {
      _session = session;
    }

    public TEntity Convert(ResolutionContext context)
    {
      if (context.IsSourceValueNull) return null;

      var entity = _session.Get<TEntity>(context.SourceValue);
      return entity;
    }
  }
}