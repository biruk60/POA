using POA.Web.Infrastructure.Mapping;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace POA.Web.Models.Category
{
    public class NewCategoryForm : IMapFrom<Domain.Category>
    {
        [HiddenInput]
        public int Category_ID { get; set; }

        [Required]
        public string Name { get; set; }

        [DataType(DataType.MultilineText)]
        public string Description { get; set; }

        [DisplayName("SubCategories")]
        public string SubCategories { get; set; }
    }
}