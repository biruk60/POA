using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using POA.Web.Filters;
using POA.Web.Infrastructure.Mapping;
using StructureMap.TypeRules;
using Microsoft.AspNet.Identity.EntityFramework;
using System.Data.Entity;
using System.Web.Mvc;
using System.ComponentModel.DataAnnotations;

namespace POA.Web.Models.Template
{
    public class TemplateSummaryViewModel : IMapFrom<Domain.Template>
    {

        public int Template_ID { get; set; }
        public string Name { get; set; }
        public string Content { get; set; }
        public string Title { get; set; }
        public string Header { get; set; }
    }
}