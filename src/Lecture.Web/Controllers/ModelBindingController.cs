using System;
using System.Linq;
using System.Web.Mvc;
using Lecture.Web.Models;

namespace Lecture.Web.Controllers
{
  public class ModelBindingController : Controller
  {
    public ActionResult Index()
    {
      var model = new FormModel
        {
          Integer = 10,
          String = "The String!",
          Date = DateTime.Today,
          Double = .5,
          Decimal = 10.2311m,
          SubForm = new SubFormModel {SubFormValue = "I'm in the Subform!"}
        };

      return View(model);
    }

    public ActionResult QueryStringParameters(int theValue)
    {
      return RedirectToAction("Index");
    }

    public ActionResult FormPostSimpleParameters(int? integer, string @string, decimal @decimal = 9.17232m)
    {
      return RedirectToAction("Index");
    }

    public ActionResult FormPostComplexParameter(FormModel model)
    {
      if (model.Integer % 2 != 0) ModelState.AddModelError("Integer", "Integer must be even.");

      if (!ModelState.IsValid) return View("Index");

      return View("Index", model);
    }
  }
}