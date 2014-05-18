using System;
using System.Collections.Generic;
using System.Data.Entity.SqlServer;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using POA.Domain;

namespace POA.Web.Filters
{
	public class CountrySelectListPopulatorAttribute: ActionFilterAttribute
	{
		public POADBEntities Context { get; set; }
		private SelectListItem[] GetAvailableCountries()
		{

			return Context.Countries.Select(u => new SelectListItem
			{
				Text = u.Name,
				Value = SqlFunctions.StringConvert((double)u.Country_ID).Trim()
			   
			}).ToArray();
		}

		

		public override void OnActionExecuted(ActionExecutedContext filterContext)
		{
			var viewResult = filterContext.Result as ViewResult;

			if (viewResult != null && viewResult.Model is IHaveCountrySelectList)
			{
				((IHaveCountrySelectList)viewResult.Model).AvailableCountries =
					GetAvailableCountries();
			}
		}
	}

	public interface IHaveCountrySelectList
	{
		SelectListItem[] AvailableCountries { get; set; }
	}

	
}