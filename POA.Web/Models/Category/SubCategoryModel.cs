using POA.Domain;
using System;
using System.Collections.Generic;
using System.Data.Entity.SqlServer;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace POA.Web.Models.Category
{
    public class SubCategoryModel
    {
        private readonly POADBEntities _context;
        // This property contains the available options
        public SelectList SubCategorySources { get; set; } 

        // This property contains the selected options
        public IEnumerable<string> SelectedSources { get; set; }

               
        public SubCategoryModel(POADBEntities context, string strCategoryID)
        {
            _context = context;
           

                var availableSubCategories = _context.SubCategories.Select(u => new SelectListItem
            {
                Text = u.Name,
                Value = SqlFunctions.StringConvert((double)u.SubCategory_ID).Trim()

            }).ToArray();

                SubCategorySources = new SelectList(availableSubCategories, "Value", "Text");

                if (!String.IsNullOrEmpty(strCategoryID))
            {
                var checkedSubs = from cs in _context.Category_SubCategory
                                  where SqlFunctions.StringConvert((double)cs.Category_ID).Trim() == strCategoryID
                                  select new
                                  {
                                      Value = SqlFunctions.StringConvert((double)cs.SubCategory_ID).Trim()
                                  };

                checkedSubs.Select( x => x.Value).ToList();

                SelectedSources = checkedSubs.Select(x => x.Value).ToList();
            }
           
        }
         
    }
}