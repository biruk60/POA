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
using POA.Web.Models.SubCategory;

namespace POA.Web.Controllers
{
    public class SubCategoryController : BaseController
    {
        private readonly POADBEntities _context;
        private SubCategory _subCategory { get; set; }

        public SubCategoryController(POADBEntities context, SubCategory subCategory)
        {
            _context = context;
            _subCategory = subCategory;
        }


        //
        // GET: /SubCategory/
        [ChildActionOnly]
        public ActionResult All()
        {

            var models = _context.SubCategories
                  .Project().To<SubCategorySummaryViewModel>();

            return PartialView(models.ToArray());
        }

        //[Log("Viewed SubCategory {id}")]
        public ActionResult View(int id)
        {
            var model = _context.SubCategories
                .SingleOrDefault(i => i.SubCategory_ID == id);

            if (model == null)
            {
                return RedirectToAction<SubCategoryController>(c => c.Index())
                        .WithError("Unable to find the SubCategory.  Maybe it was deleted?");

            }

            return View(new SubCategoryDetailsViewModel
            {
                SubCategory_ID = model.SubCategory_ID,
                Name = model.Name,
                Description = model.Description
            });
        }

        public ActionResult New()
        {
            var form = new NewSubCategoryForm
            {
                
            };
            return View(form);
        }

        public ActionResult Index()
        {
            return View();
        }

        [HttpPost, ValidateAntiForgeryToken, Log("Created SubCategory")]
        public ActionResult New(NewSubCategoryForm form, FormCollection fc)
        {
            if (!ModelState.IsValid)
            {
                return View(form);
            }


            _subCategory.Name = form.Name;
            _subCategory.Description = form.Description;
            _context.SubCategories.Add(_subCategory);

            var ptIdArray = fc.GetValues("Cat.SelectedSources");

            string strCategoryID = "";

            for (int i = 0; i < ptIdArray.Count(); i++)
            {

                strCategoryID = ptIdArray[i].ToString();
                Category_SubCategory _categorySubCategory = new Category_SubCategory();
                _categorySubCategory.Category_ID = Convert.ToInt32(strCategoryID);
                _categorySubCategory.SubCategory_ID = form.SubCategory_ID;
                _context.Category_SubCategory.Add(_categorySubCategory);

            }
            _context.SaveChanges();

            //return RedirectToAction("Index", "Home");
            return RedirectToAction<SubCategoryController>(c => c.Index()).WithSuccess("SubCategory created!");
        }

        [Log("Started to edit SubCategory {id}")]
        public ActionResult Edit(int id)
        {
            var subCategory = _context.SubCategories
                .Project().To<EditSubCategoryForm>()
                .SingleOrDefault(i => i.SubCategory_ID == id);

            if (subCategory == null)
            {
                return RedirectToAction<SubCategoryController>(c => c.Index())
                        .WithError("Unable to find the SubCategory.  Maybe it was deleted?");
            }

            return View(subCategory);
        }

        [HttpPost, Log("Saving changes")]
        public ActionResult Edit(EditSubCategoryForm form, FormCollection fc)
        {
            if (!ModelState.IsValid)
            {
                return View(form);
            }

            var subCategory = _context.SubCategories.SingleOrDefault(i => i.SubCategory_ID == form.SubCategory_ID);

            if (subCategory == null)
            {
                return RedirectToAction<SubCategoryController>(c => c.Index())
                        .WithError("Unable to find the SubCategory.  Maybe it was deleted?");
            }


            subCategory.Name = form.Name;
            subCategory.Description = form.Description;

            IEnumerable<Category_SubCategory> entities = _context.Category_SubCategory.Where(c => c.SubCategory_ID == form.SubCategory_ID);
            _context.Category_SubCategory.RemoveRange(entities);

            var ptIdArray = fc.GetValues("Cat.SelectedSources");

            string strCategoryID = "";

            for (int i = 0; i < ptIdArray.Count(); i++)
            {

                strCategoryID = ptIdArray[i].ToString();
                Category_SubCategory _categorySubCategory = new Category_SubCategory();
                _categorySubCategory.Category_ID = Convert.ToInt32(strCategoryID);
                _categorySubCategory.SubCategory_ID = form.SubCategory_ID;
                _context.Category_SubCategory.Add(_categorySubCategory);

            }
            _context.SaveChanges();

            return this.RedirectToAction(c => c.View(form.SubCategory_ID))
                  .WithSuccess("Changes saved!");

        }

        [HttpPost, ValidateAntiForgeryToken, Log("Deleted SubCategory {id}")]
        public ActionResult Delete(int id)
        {
            var subCategory = _context.SubCategories.Find(id);

            if (subCategory == null)
            {
                return this.RedirectToAction<SubCategoryController>(c => c.Index())
                       .WithError("Unable to find the SubCategory.  Maybe it was deleted?");
            }

            IEnumerable<Category_SubCategory> entities = _context.Category_SubCategory.Where(c => c.SubCategory_ID == id);
            _context.Category_SubCategory.RemoveRange(entities);
            _context.SubCategories.Remove(subCategory);

            _context.SaveChanges();

            return RedirectToAction<SubCategoryController>(c => c.Index()).WithSuccess("SubCategory deleted!");
        }

        public ActionResult Cancel()
        {
            return RedirectToAction<SubCategoryController>(c => c.Index());
        }
    }
}