#region usings

using System.Collections.Generic;
using System.Web.Mvc;
using Lecture.Domain.Queries;
using Lecture.Domain.Queries.Users;
using Lecture.Web.Models.Validation.IoC;
using NHibernate;

#endregion

namespace Lecture.Web.Areas.Admin.Models.Validators
{
  [ModelValidatorType(typeof (UserForm))]
  public class UserFormValidator : ModelValidator
  {
    private readonly ISession _nhSession;
    private readonly IQueryExecutor _queryExecutor;

    public UserFormValidator(ModelMetadata modelMetadata, ControllerContext controllerContext, ISession _nhSession, IQueryExecutor queryExecutor)
      : base(modelMetadata, controllerContext)
    {
      this._nhSession = _nhSession;
      _queryExecutor = queryExecutor;
    }

    public override IEnumerable<ModelValidationResult> Validate(object container)
    {
      if (Metadata.Model == null) yield break;

      var model = (UserForm) Metadata.Model;
      using (_nhSession.BeginTransaction())
      {
        if (!string.IsNullOrEmpty(model.Email) && _queryExecutor.ExecuteQuery(new UserWithEmailAlreadyExists(model.Email, model.Id)))
          yield return new ModelValidationResult {MemberName = "Name", Message = "A user with this email address already exists."};
      }
    }
  }
}