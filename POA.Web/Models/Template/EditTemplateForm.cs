using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using POA.Domain;
using POA.Web.Filters;
using POA.Web.Infrastructure.Mapping;
using StructureMap.TypeRules;
using Microsoft.AspNet.Identity.EntityFramework;
using System.Data.Entity;
using System.Web.Mvc;
using System.ComponentModel.DataAnnotations;

namespace POA.Web.Models.Template
{
    public class EditTemplateForm : IMapFrom<Domain.Template>
    {
        [HiddenInput]
        public int Template_ID { get; set; }

        [Required]
        public string Name { get; set; }


        [DataType(DataType.MultilineText), Required]
        public string Content { get; set; }

        [Required]
        public string Title { get; set; }

        [Required]
        public string Header { get; set; }
    }
}