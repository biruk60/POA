using POA.Web.Infrastructure.Mapping;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace POA.Web.Models.Category
{
    public class CategoryDetailsViewModel : IMapFrom<Domain.Category>
    {
        
        public int Category_ID { get; set; }        
        public string Name { get; set; }        
        public string Description { get; set; }
    }
}