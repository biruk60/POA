using POA.Web.Infrastructure.Mapping;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace POA.Web.Models.SubCategory
{
    public class SubCategoryDetailsViewModel : IMapFrom<Domain.SubCategory>
    {
        public int SubCategory_ID { get; set; }
        public string Name { get; set; }
        public string Description { get; set; }
    }
}