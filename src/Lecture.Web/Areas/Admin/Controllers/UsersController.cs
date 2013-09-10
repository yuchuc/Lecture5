#region usings

using System.Collections.Generic;
using System.Linq;
using System.Web.Mvc;
using AutoMapper;
using Lecture.Domain.Entities;
using Lecture.Web.Areas.Admin.Models;
using Lecture.Web.Areas.Admin.Models.GridDataBuilders;
using Lecture.Web.Controllers;
using Lecture.Web.Infrastructure.Authentication;
using Lecture.Web.Models;
using NHibernate;

#endregion

namespace Lecture.Web.Areas.Admin.Controllers
{
  public class UsersController : BaseNHibernateController
  {
    private readonly IWebSecurity _webSecurity;
    private readonly IMappingEngine _mapper;
    private readonly UsersGridDataBuilder _usersGridDataBuilder;

    public UsersController(
      ISession nhSession,
      IWebSecurity webSecurity,
      IMappingEngine mapper,
      UsersGridDataBuilder usersGridDataBuilder)
      : base(nhSession)
    {
      _webSecurity = webSecurity;
      _mapper = mapper;
      _usersGridDataBuilder = usersGridDataBuilder;
    }

    [HttpGet]
    public ActionResult Index()
    {
      if (HttpContext.Request.IsAjaxRequest())
      {
        var gridData = _usersGridDataBuilder.BuildGridData();
        return Json(gridData, JsonRequestBehavior.AllowGet);
      }

      return View();
    }

    [HttpGet]
    public ActionResult Add()
    {
      var form = new UserForm
        {
          RoleSelectListItems = _mapper.Map<IEnumerable<string>, IEnumerable<SelectListItem>>(SystemRoles.Roles.OrderBy(r => r))
        };

      return View("AddEdit", form);
    }

    [HttpGet]
    public ActionResult Edit(int id)
    {
      var user = NhSession.Get<User>(id);
      var form = _mapper.Map<User, UserForm>(user, opts => opts.ConstructServicesUsing(DependencyResolver.Current.GetService));
      form.RoleSelectListItems = _mapper.Map<IEnumerable<string>, IEnumerable<SelectListItem>>(SystemRoles.Roles.OrderBy(r => r));

      return View("AddEdit", form);
    }

    [HttpPost, ValidateAntiForgeryToken]
    public ActionResult Save(UserForm form)
    {
      if (!ModelState.IsValid)
      {
        form.RoleSelectListItems = _mapper.Map<IEnumerable<string>, IEnumerable<SelectListItem>>(SystemRoles.Roles.OrderBy(r => r));
        return View("AddEdit", form);
      }

      if (form.Id != null && !string.IsNullOrEmpty(form.Password))
      {
        _webSecurity.ResetPasswordWithoutToken(form.Email, form.Password);
      }
      else if (form.Id == null)
      {
        var user = _mapper.Map<UserForm, User>(form, opts => opts.ConstructServicesUsing(DependencyResolver.Current.GetService));
        NhSession.Save(user);
        NhSession.Transaction.Commit();
        _webSecurity.CreateAccount(form.Email, form.Password);
      }

      _webSecurity.SetRolesForUser(form.Email, form.Roles);

      return RedirectToAction("Index");
    }

    [HttpDelete, ValidateAntiForgeryToken]
    public JsonResult Delete(int id)
    {
      var user = NhSession.Get<User>(id);
      if (user == null)
      {
        return Json(AjaxFormModalResponse.Error("The specified user no longer exists."));
      }

      NhSession.Delete(user);

      return Json(AjaxFormModalResponse.Success());
    }
  }
}