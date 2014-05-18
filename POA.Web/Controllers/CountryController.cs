using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using AutoMapper.QueryableExtensions;
using POA.Web.Filters;
using Microsoft.Web.Mvc;
using POA.Web.Infrastructure.Alerts;
using POA.Domain;
using POA.Web.Models.Country;

namespace POA.Web.Controllers
{
    public class CountryController : BaseController
    {
       private readonly POADBEntities _context;
        private Country _country { get; set; }

        public CountryController(POADBEntities context, Country country)
        {
            _context = context;
            _country = country;
        }


        //
        // GET: /Country/
        [ChildActionOnly]
        public ActionResult All()
        {

            var models = _context.Countries
                  .Project().To<CountrySummaryViewModel>();

            return PartialView(models.ToArray());
        }

        //[Log("Viewed Country {id}")]
        public ActionResult View(int id)
        {
            var model = _context.Countries
                .SingleOrDefault(i => i.Country_ID == id);

            if (model == null)
            {
                return RedirectToAction<CountryController>(c => c.Index())
                        .WithError("Unable to find the Country.  Maybe it was deleted?");

            }

            return View(new CountryDetailsViewModel
            {
                Country_ID = model.Country_ID,
                Name = model.Name,
                Description = model.Description
            });
        }

        public ActionResult New()
        {
            var form = new NewCountryForm
            {
                
            };
            return View(form);
        }

        public ActionResult Index()
        {
            return View();
        }

        [HttpPost, ValidateAntiForgeryToken, Log("Created Country")]
        public ActionResult New(NewCountryForm form)
        {
            if (!ModelState.IsValid)
            {
                return View(form);
            }


            _country.Name = form.Name;
            _country.Description = form.Description;
            _context.Countries.Add(_country);
            _context.SaveChanges();

            //return RedirectToAction("Index", "Home");
            return RedirectToAction<CountryController>(c => c.Index()).WithSuccess("Country created!");
        }

        [Log("Started to edit Country {id}")]
        public ActionResult Edit(int id)
        {
            var country = _context.Countries
                .Project().To<EditCountryForm>()
                .SingleOrDefault(i => i.Country_ID == id);

            if (country == null)
            {
                return RedirectToAction<CountryController>(c => c.Index())
                        .WithError("Unable to find the Country.  Maybe it was deleted?");
            }

            return View(country);
        }

        [HttpPost, Log("Saving changes")]
        public ActionResult Edit(EditCountryForm form)
        {
            if (!ModelState.IsValid)
            {
                return View(form);
            }

            var country = _context.Countries.SingleOrDefault(i => i.Country_ID == form.Country_ID);

            if (country == null)
            {
                return RedirectToAction<CountryController>(c => c.Index())
                        .WithError("Unable to find the Country.  Maybe it was deleted?");
            }


            country.Name = form.Name;
            country.Description = form.Description;
            _context.SaveChanges();

            return this.RedirectToAction(c => c.View(form.Country_ID))
                  .WithSuccess("Changes saved!");

        }

        [HttpPost, ValidateAntiForgeryToken, Log("Deleted Country {id}")]
        public ActionResult Delete(int id)
        {
            var country = _context.Countries.Find(id);

            if (country == null)
            {
                return this.RedirectToAction<CountryController>(c => c.Index())
                       .WithError("Unable to find the Country.  Maybe it was deleted?");
            }

            _context.Countries.Remove(country);

            _context.SaveChanges();

            return RedirectToAction<CountryController>(c => c.Index()).WithSuccess("Country deleted!");
        }

        public ActionResult Cancel()
        {
            return RedirectToAction<CountryController>(c => c.Index());
        }
    }
}