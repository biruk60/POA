using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Web.WebPages.Html;
using POA.Web.Filters;
using POA.Web.Infrastructure.Mapping;

namespace POA.Web.Models.Title
{
    public class NewTitleForm : IMapFrom<Domain.Title>
    {
        [HiddenInput]
        public int Title_ID { get; set; }
        [Required]
        public string Title_Name { get; set; }
        [DataType(DataType.MultilineText)]
        public string Title_Description { get; set; }

        
       // public Domain.Country Country { get; set; }
       // public System.Web.Mvc.SelectListItem[] AvailableCountries { get; set; }

        
    }
}