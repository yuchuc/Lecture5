#region usings

using System.ComponentModel.DataAnnotations;

#endregion

namespace Lecture.Web.Models
{
  public class SignInForm
  {
    [Required]
    public string Email { get; set; }

    [Required, DataType(DataType.Password)]
    public string Password { get; set; }
  }
}