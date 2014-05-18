using AutoMapper;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using POA.Domain;
using POA.Web.Filters;
using POA.Web.Models;
using POA.Web.Models.Title;
using Microsoft.Web.Mvc;
using POA.Web.Infrastructure.Alerts;
using AutoMapper.QueryableExtensions;


namespace POA.Web.Controllers
{
    public class TitleController : BaseController
    {

        private readonly POADBEntities _context;
        private Title _title { get; set; }

        public TitleController(POADBEntities context, Title title)
        {
            _context = context;
            _title = title;
        }


        //
        // GET: /Title/
        [ChildActionOnly]
        public ActionResult All()
        {

            var models = _context.Titles
                  .Project().To<TitleSummaryViewModel>();

            return PartialView(models.ToArray());
        }

        //[Log("Viewed title {id}")]
        public ActionResult View(int id)
        {
            var model = _context.Titles
                .SingleOrDefault(i => i.Title_ID == id);

            if (model == null)
            {
                return RedirectToAction<HomeController>(c => c.Index())
                        .WithError("Unable to find the Title.  Maybe it was deleted?");

            }

            return View(new TitleDetailsViewModel
            {
                Title_ID = model.Title_ID,
                Title_Name = model.Title_Name,
                Title_Description = model.Title_Description
            });
        }

        public ActionResult New()
        {
            var form = new NewTitleForm
            {
                //AvailableUsers = GetAvailableUsers(),
                //AvailableIssueTypes = GetAvailableIssueTypes()
            };
            return View(form);
        }

        public ActionResult Index()
        {
            return View();
        }

        [HttpPost, ValidateAntiForgeryToken, Log("Created title")]
        public ActionResult New(NewTitleForm form)
        {
            if (!ModelState.IsValid)
            {
                return View(form);
            }


            _title.Title_Name = form.Title_Name;
            _title.Title_Description = form.Title_Description;
            _context.Titles.Add(_title);
            _context.SaveChanges();

            //return RedirectToAction("Index", "Home");
            return RedirectToAction<TitleController>(c => c.Index()).WithSuccess("Title created!");
        }

        [Log("Started to edit title {id}")]
        public ActionResult Edit(int id)
        {
            var title = _context.Titles
                .Project().To<EditTitleForm>()
                .SingleOrDefault(i => i.Title_ID == id);

            if (title == null)
            {
                return RedirectToAction<TitleController>(c => c.Index())
                        .WithError("Unable to find the Title.  Maybe it was deleted?");
            }

            return View(title);
        }

        [HttpPost, Log("Saving changes")]
        public ActionResult Edit(EditTitleForm form)
        {
            if (!ModelState.IsValid)
            {
                return View(form);
            }

            var title = _context.Titles.SingleOrDefault(i => i.Title_ID == form.Title_ID);

            if (title == null)
            {
                return RedirectToAction<TitleController>(c => c.Index())
                        .WithError("Unable to find the Title.  Maybe it was deleted?");
            }


            title.Title_Name = form.Title_Name;
            title.Title_Description = form.Title_Description;
            _context.SaveChanges();

            return this.RedirectToAction(c => c.View(form.Title_ID))
                  .WithSuccess("Changes saved!");

        }

        [HttpPost, ValidateAntiForgeryToken, Log("Deleted title {id}")]
        public ActionResult Delete(int id)
        {
            var title = _context.Titles.Find(id);

            if (title == null)
            {
                return this.RedirectToAction<TitleController>(c => c.Index())
                       .WithError("Unable to find the Title.  Maybe it was deleted?");
            }

            _context.Titles.Remove(title);

            _context.SaveChanges();

            return RedirectToAction<TitleController>(c => c.Index()).WithSuccess("Title deleted!");
        }

        public ActionResult Cancel()
        {
            return RedirectToAction<TitleController>(c => c.Index());
        }

    }
}