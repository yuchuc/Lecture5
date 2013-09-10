#region usings

using System.Collections.Generic;
using System.Linq;
using System.Web.Mvc;

#endregion

namespace Lecture.Web.Models
{
  public class AjaxFormModalResponse
  {
    public enum ResponseType
    {
      Invalid = 0,
      Success,
      ValidationError,
      Error
    }

    private AjaxFormModalResponse(ResponseType type, string message)
    {
      Type = type;
      Message = message;
      ValidationErrors = Enumerable.Empty<string>();
    }

    private AjaxFormModalResponse(ResponseType type, IDictionary<string, ModelState> modelState)
    {
      Type = type;
      ValidationErrors = modelState.Values.SelectMany(item => item.Errors).Select(x => x.ErrorMessage).ToList();
    }

    // ReSharper disable MemberCanBePrivate.Global
    // ReSharper disable UnusedAutoPropertyAccessor.Global
    // ReSharper disable UnusedMember.Global
    public ResponseType Type { get; private set; }
    public string Message { get; private set; }
    public IEnumerable<string> ValidationErrors { get; private set; }
    public object Payload { get; set; }
    // ReSharper restore UnusedMember.Global
    // ReSharper restore UnusedAutoPropertyAccessor.Global
    // ReSharper restore MemberCanBePrivate.Global

    public static AjaxFormModalResponse Success(string message = null)
    {
      return new AjaxFormModalResponse(ResponseType.Success, message);
    }

    public static AjaxFormModalResponse ValidationError(IDictionary<string, ModelState> modelState)
    {
      return new AjaxFormModalResponse(ResponseType.ValidationError, modelState);
    }

    public static AjaxFormModalResponse Error(string message = null)
    {
      return new AjaxFormModalResponse(ResponseType.Error, message);
    }
  }
}