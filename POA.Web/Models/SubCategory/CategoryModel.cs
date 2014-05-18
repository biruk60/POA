using POA.Domain;
using System;
using System.Collections.Generic;
using System.Data.Entity.SqlServer;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace POA.Web.Models.SubCategory
{
    public class CategoryModel
    {
        private readonly POADBEntities _context;
        // This property contains the available options
        public SelectList CategorySources { get; set; }
       

        // This property contains the selected options
        public IEnumerable<string> SelectedSources { get; set; }


        public CategoryModel(POADBEntities context, string strSubCategoryID)
        {
            _context = context;

            var availableCategories = _context.Categories.Select(u => new SelectListItem
            {
                Text = u.Name,
                Value = SqlFunctions.StringConvert((double)u.Category_ID).Trim()

            }).ToArray();

                CategorySources = new SelectList(availableCategories, "Value", "Text");

            if (!String.IsNullOrEmpty(strSubCategoryID))
            {
                var checkedCats = from cs in _context.Category_SubCategory
                                  where SqlFunctions.StringConvert((double)cs.SubCategory_ID).Trim() == strSubCategoryID
                                  select new
                                  {
                                      Value = SqlFunctions.StringConvert((double)cs.Category_ID).Trim()
                                  };

                checkedCats.Select(x => x.Value).ToList();

                SelectedSources = checkedCats.Select(x => x.Value).ToList();
            }
           
        }
    }
}