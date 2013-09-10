#region usings

using System.Web.Mvc;
using Lecture.Web.Infrastructure.Authentication;
using Lecture.Web.Models;
using NHibernate;

#endregion

namespace Lecture.Web.Controllers
{
  public class AuthenticationController : BaseNHibernateController
  {
    private readonly IWebSecurity _webSecurity;

    public AuthenticationController(ISession nhSession, IWebSecurity webSecurity)
      : base(nhSession)
    {
      _webSecurity = webSecurity;
    }

    [HttpGet]
    public ActionResult SignIn()
    {
      return View();
    }

    [HttpPost, ValidateAntiForgeryToken]
    public ActionResult SignIn(SignInForm form, string returnUrl)
    {
      if (ModelState.IsValid)
      {
        if (_webSecurity.SignIn(form.Email, form.Password))
        {
          return Url.IsLocalUrl(returnUrl) ? (ActionResult) Redirect(returnUrl) : RedirectToAction("Index", "Home");
        }

        ModelState.AddModelError("", "The username or password provided is incorrect.");
      }

      return View(form);
    }

    [AcceptVerbs(HttpVerbs.Get | HttpVerbs.Delete)]
    public RedirectToRouteResult SignOut()
    {
      _webSecurity.SignOut();
      return RedirectToAction("Index", "Home");
    }
  }
}