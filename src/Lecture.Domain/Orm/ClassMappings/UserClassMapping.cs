#region usings

using Lecture.Domain.Entities;
using NHibernate.Mapping.ByCode;
using NHibernate.Mapping.ByCode.Conformist;

#endregion

namespace Lecture.Domain.Orm.ClassMappings
{
  public class UserClassMapping : ClassMapping<User>
  {
    public UserClassMapping()
    {
      Table("`User`");
      Id(x => x.Id, x => x.Generator(Generators.HighLow, gm => gm.Params(new {max_lo = 256})));
      Property(x => x.Email);
      Property(x => x.Age);
    }
  }
}