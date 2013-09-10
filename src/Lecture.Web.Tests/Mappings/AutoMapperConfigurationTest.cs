#region usings

using System.Collections.Generic;
using AutoMapper;
using Autofac;
using NUnit.Framework;

#endregion

namespace Lecture.Web.Tests.Mappings
{
  [TestFixture]
  public class AutoMapperConfigurationTests
  {
    [Test]
    public void VerifyMappings()
    {
      var builder = new ContainerBuilder();
      builder.RegisterAssemblyTypes(typeof (MvcApplication).Assembly)
             .Where(type => type.IsAssignableTo<Profile>());
      var container = builder.Build();

      Mapper.Initialize(
        cfg =>
          {
            foreach (var mappingProfile in container.Resolve<IEnumerable<Profile>>())
            {
              cfg.AddProfile(mappingProfile);
            }
          });
      Mapper.AssertConfigurationIsValid();
    }
  }
}