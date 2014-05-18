using POA.Domain;
using System;
using System.Collections.Generic;
using System.Data.Entity.SqlServer;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace POA.Web.Models.PowerOfAttorney
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
            strCategoryID = !string.IsNullOrEmpty(strCategoryID) ? strCategoryID : "2";

           var availableSubCategories = from cs in _context.Category_SubCategory
                                  join sc in _context.SubCategories on cs.SubCategory_ID equals sc.SubCategory_ID
                                  where SqlFunctions.StringConvert((double)cs.Category_ID).Trim() == strCategoryID
                                  select new
                                  {
                                      Text = sc.Name,
                                      Value = SqlFunctions.StringConvert((double)cs.SubCategory_ID).Trim()
                                  };
            

            SubCategorySources = new SelectList(availableSubCategories.ToArray(), "Value", "Text");            

        }
    }
}