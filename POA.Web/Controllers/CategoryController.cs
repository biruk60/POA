using POA.Domain;
using POA.Web.Models.Category;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using AutoMapper.QueryableExtensions;
using POA.Web.Filters;
using Microsoft.Web.Mvc;
using POA.Web.Infrastructure.Alerts;

namespace POA.Web.Controllers
{
    public class CategoryController  : BaseController
    {
        private readonly POADBEntities _context;
        private Category _category { get; set; }
       // private Category_SubCategory _categorySubCategory { get; set; }

        public CategoryController(POADBEntities context, Category category)
        {
            _context = context;
            _category = category;
        }


        //
        // GET: /Category/
        [ChildActionOnly]
        public ActionResult All()
        {

            var models = _context.Categories
                  .Project().To<CategorySummaryViewModel>();

            return PartialView(models.ToArray());
        }

        //[Log("Viewed Category {id}")]
        public ActionResult View(int id)
        {
            var model = _context.Categories
                .SingleOrDefault(i => i.Category_ID == id);

            if (model == null)
            {
                return RedirectToAction<CategoryController>(c => c.Index())
                        .WithError("Unable to find the Category.  Maybe it was deleted?");

            }

            return View(new CategoryDetailsViewModel
            {
                Category_ID = model.Category_ID,
                Name = model.Name,
                Description = model.Description
            });
        }

        public ActionResult New()
        {
            var form = new NewCategoryForm
            {
                
            };
            return View(form);
        }

        public ActionResult Index()
        {
            return View();
        }

        [HttpPost, ValidateAntiForgeryToken, Log("Created Category")]
        public ActionResult New(NewCategoryForm form, FormCollection fc)
        {
            if (!ModelState.IsValid)
            {
                return View(form);
            }


            _category.Name = form.Name;
            _category.Description = form.Description;
            _context.Categories.Add(_category);

            var ptIdArray = fc.GetValues("SubCat.SelectedSources");

            string strSubCategoryID = "";

            for (int i = 0; i < ptIdArray.Count(); i++)
            {

                strSubCategoryID = ptIdArray[i].ToString();
                Category_SubCategory _categorySubCategory = new Category_SubCategory();
                _categorySubCategory.Category_ID = form.Category_ID;
                _categorySubCategory.SubCategory_ID = Convert.ToInt32(strSubCategoryID);
                _context.Category_SubCategory.Add(_categorySubCategory);

            }

            _context.SaveChanges();

            //return RedirectToAction("Index", "Home");
            return RedirectToAction<CategoryController>(c => c.Index()).WithSuccess("Category created!");
        }

        [Log("Started to edit Category {id}")]
        public ActionResult Edit(int id)
        {
            var category = _context.Categories
                .Project().To<EditCategoryForm>()
                .SingleOrDefault(i => i.Category_ID == id);

            if (category == null)
            {
                return RedirectToAction<CategoryController>(c => c.Index())
                        .WithError("Unable to find the Category.  Maybe it was deleted?");
            }

            
            //int intCobID = Convert.ToInt32(form.Country_Of_Birth_ID);

            return View(category);
        }

        [HttpPost, Log("Saving changes")]
        public ActionResult Edit(EditCategoryForm form, FormCollection fc)
        {
            if (!ModelState.IsValid)
            {
                return View(form);
            }

            var category = _context.Categories.SingleOrDefault(i => i.Category_ID == form.Category_ID);

            if (category == null)
            {
                return RedirectToAction<CategoryController>(c => c.Index())
                        .WithError("Unable to find the Category.  Maybe it was deleted?");
            }


            var ptIdArray = fc.GetValues("SubCat.SelectedSources");

            string strSubCategoryID = "";

            IEnumerable<Category_SubCategory> entities = _context.Category_SubCategory.Where(c => c.Category_ID == form.Category_ID);
            _context.Category_SubCategory.RemoveRange(entities);

            for (int i = 0; i < ptIdArray.Count(); i++)
            {

                strSubCategoryID = ptIdArray[i].ToString();
                Category_SubCategory _categorySubCategory = new Category_SubCategory();
                _categorySubCategory.Category_ID = form.Category_ID;
                _categorySubCategory.SubCategory_ID = Convert.ToInt32(strSubCategoryID);
                _context.Category_SubCategory.Add(_categorySubCategory);

            }
            category.Name = form.Name;
            category.Description = form.Description;
            _context.SaveChanges();            

            return this.RedirectToAction(c => c.View(form.Category_ID))
                  .WithSuccess("Changes saved!");

        }

        [HttpPost, ValidateAntiForgeryToken, Log("Deleted Category {id}")]
        public ActionResult Delete(int id)
        {
            var category = _context.Categories.Find(id);

            if (category == null)
            {
                return this.RedirectToAction<CategoryController>(c => c.Index())
                       .WithError("Unable to find the Category.  Maybe it was deleted?");
            }

            IEnumerable<Category_SubCategory> entities = _context.Category_SubCategory.Where(c => c.Category_ID == id);
            _context.Category_SubCategory.RemoveRange(entities);
            _context.Categories.Remove(category);

            _context.SaveChanges();

            return RedirectToAction<CategoryController>(c => c.Index()).WithSuccess("Category deleted!");
        }

        public ActionResult Cancel()
        {
            return RedirectToAction<CategoryController>(c => c.Index());
        }
    }
}