#region usings

using System;
using System.Collections.Generic;

#endregion

namespace Lecture.Web.Models.Validation.IoC
{
  [AttributeUsage(AttributeTargets.Class, AllowMultiple = true, Inherited = false)]
  public sealed class ModelValidatorTypeAttribute : Attribute
  {
    /// <summary>
    ///   Initializes a new instance of the <see cref="ModelValidatorTypeAttribute" /> class.
    /// </summary>
    /// <param name="targetTypes">The target types.</param>
    public ModelValidatorTypeAttribute(params Type[] targetTypes)
    {
      TargetTypes = targetTypes;
    }

    public IEnumerable<Type> TargetTypes { get; private set; }
  }
}