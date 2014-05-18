using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using POA.Web.Infrastructure.Mapping;
using POA.Domain;

namespace POA.Web.Models.Title
{
    public class TitleSummaryViewModel : IMapFrom<POA.Domain.Title>
    {
        public int Title_ID { get; set; }
        public string Title_Name { get; set; }
        public string Title_Description { get; set; }
        
    }
}