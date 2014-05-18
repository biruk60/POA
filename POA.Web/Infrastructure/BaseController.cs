using Microsoft.Web.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Web;
using System.Web.Mvc;
using POA.Web.Filters;

namespace POA.Web.Controllers
{
	[CountrySelectListPopulator]
	public abstract class BaseController : Controller
	{
		protected ActionResult RedirectToAction<TController>(
			 Expression<Action<TController>> action)
			 where TController : Controller
		{
			return ControllerExtensions.RedirectToAction(this, action);
		}
	}
}