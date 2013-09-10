#region usings

using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Web.Mvc;

#endregion

namespace Lecture.Web.Areas.Admin.Models
{
  public class UserForm
  {
    public UserForm()
    {
      Roles = new string[0];
    }

    public int? Id { get; set; }

    [Required, StringLength(256), EmailAddress]
    public string Email { get; set; }

    [Required, RegularExpression(@".*\d+.*", ErrorMessage = "Passwords must contain at least one number.")]
    public string Password { get; set; }

    [Required, Range(13, int.MaxValue)]
    public int Age { get; set; }

    [Display(Name = "Confirm Password")]
    [Required, System.ComponentModel.DataAnnotations.Compare("Password", ErrorMessage = "The password and confirmation password do not match.")]
    public string ConfirmPassword { get; set; }

    public string[] Roles { get; set; }

    public IEnumerable<SelectListItem> RoleSelectListItems { get; set; }
  }
}