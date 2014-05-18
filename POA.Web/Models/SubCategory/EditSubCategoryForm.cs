using POA.Web.Infrastructure.Mapping;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace POA.Web.Models.SubCategory
{
    public class EditSubCategoryForm : IMapFrom<Domain.SubCategory>
    {
        [HiddenInput]
        public int SubCategory_ID { get; set; }

        [Required]
        public string Name { get; set; }

         [DataType(DataType.MultilineText)]
        public string Description { get; set; }

         [DisplayName("Category")]
         public string Category { get; set; }
    }
}