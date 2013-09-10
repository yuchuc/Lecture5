#region usings

using System;
using System.Collections.Generic;
using System.Linq;
using System.Web.Mvc;
using Autofac;
using Autofac.Features.Metadata;
using Autofac.Integration.Mvc;

#endregion

namespace Lecture.Web.Models.Validation.IoC
{
  public class AutofacModelValidatorProvider : ModelValidatorProvider
  {
    internal const string MetadataKey = "SupportedModelTypes";

    public override IEnumerable<ModelValidator> GetValidators(ModelMetadata metadata, ControllerContext context)
    {
      if (metadata.ContainerType != null && !string.IsNullOrEmpty(metadata.PropertyName)) return Enumerable.Empty<ModelValidator>();

      var modelValidators = AutofacDependencyResolver.Current.RequestLifetimeScope.Resolve<IEnumerable<Meta<Lazy<ModelValidator>>>>(
        new TypedParameter(typeof (ModelMetadata), metadata), new TypedParameter(typeof (ControllerContext), context));

      var matchingModelValidators =
        from mv in modelValidators
        where mv.Metadata.ContainsKey(MetadataKey) && ((IEnumerable<Type>) mv.Metadata[MetadataKey]).Contains(metadata.ModelType)
        select mv.Value.Value;

      return matchingModelValidators.ToList();
    }
  }
}