using POA.Domain;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using AutoMapper.QueryableExtensions;
using POA.Web.Filters;
using Microsoft.Web.Mvc;
using POA.Web.Infrastructure.Alerts;
using POA.Web.Models.Template;

namespace POA.Web.Controllers
{
    public class TemplateController : BaseController
    {
        private readonly POADBEntities _context;
        private Template _template { get; set; }

        public TemplateController(POADBEntities context, Template template)
        {
            _context = context;
            _template = template;
        }


        //
        // GET: /Template/
        [ChildActionOnly]
        public ActionResult All()
        {

            var models = _context.Templates
                  .Project().To<TemplateSummaryViewModel>();

            return PartialView(models.ToArray());
        }

        //[Log("Viewed Template {id}")]
        public ActionResult View(int id)
        {
            var model = _context.Templates
                .SingleOrDefault(i => i.Template_ID == id);

            if (model == null)
            {
                return RedirectToAction<TemplateController>(c => c.Index())
                        .WithError("Unable to find the Template.  Maybe it was deleted?");

            }

            return View(new TemplateDetailsViewModel
            {
                Template_ID = model.Template_ID,
                Name = model.Name,
                Content = model.Content 
            });
        }

        public ActionResult New()
        {
            var form = new NewTemplateForm
            {
                
            };
            return View(form);
        }

        public ActionResult Index()
        {
            return View();
        }

        [HttpPost, ValidateAntiForgeryToken, Log("Created Template")]
        public ActionResult New(NewTemplateForm form)
        {
            if (!ModelState.IsValid)
            {
                return View(form);
            }


            _template.Name = form.Name;
            _template.Content = form.Content;
            _context.Templates.Add(_template);
            _context.SaveChanges();

            //return RedirectToAction("Index", "Home");
            return RedirectToAction<TemplateController>(c => c.Index()).WithSuccess("Template created!");
        }

        [Log("Started to edit Template {id}")]
        public ActionResult Edit(int id)
        {
            var template = _context.Templates
                .Project().To<EditTemplateForm>()
                .SingleOrDefault(i => i.Template_ID == id);

            if (template == null)
            {
                return RedirectToAction<TemplateController>(c => c.Index())
                        .WithError("Unable to find the Template.  Maybe it was deleted?");
            }

            return View(template);
        }

        [HttpPost, Log("Saving changes")]
        public ActionResult Edit(EditTemplateForm form)
        {
            if (!ModelState.IsValid)
            {
                return View(form);
            }

            var template = _context.Templates.SingleOrDefault(i => i.Template_ID == form.Template_ID);

            if (template == null)
            {
                return RedirectToAction<TemplateController>(c => c.Index())
                        .WithError("Unable to find the Template.  Maybe it was deleted?");
            }


            template.Name = form.Name;
            template.Content = form.Content;
            template.Header = form.Header;
            template.Title = form.Title;
            _context.SaveChanges();

            return this.RedirectToAction(c => c.View(form.Template_ID))
                  .WithSuccess("Changes saved!");

        }

        [HttpPost, ValidateAntiForgeryToken, Log("Deleted Template {id}")]
        public ActionResult Delete(int id)
        {
            var template = _context.Templates.Find(id);

            if (template == null)
            {
                return this.RedirectToAction<TemplateController>(c => c.Index())
                       .WithError("Unable to find the Template.  Maybe it was deleted?");
            }

            _context.Templates.Remove(template);

            _context.SaveChanges();

            return RedirectToAction<TemplateController>(c => c.Index()).WithSuccess("Template deleted!");
        }

        public ActionResult Cancel()
        {
            return RedirectToAction<TemplateController>(c => c.Index());
        }
    }
}