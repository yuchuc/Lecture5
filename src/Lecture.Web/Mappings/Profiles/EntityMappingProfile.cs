#region usings

using System;
using AutoMapper;
using NHibernate;

#endregion

namespace Lecture.Web.Mappings.Profiles
{
  public abstract class EntityMappingProfile : Profile
  {
    internal static TEntity GetOrCreateEntity<TSource, TEntity>(ResolutionContext context, Func<TSource, object> getId) where TEntity : new()
    {
      var id = getId((TSource) context.SourceValue);
      if (id == null) return new TEntity();

      var session = (ISession) context.Options.ServiceCtor(typeof (ISession));
      var entity = session.Get<TEntity>(id);
      return entity;
    }
  }
}