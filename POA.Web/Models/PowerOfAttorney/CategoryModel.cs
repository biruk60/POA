using POA.Web.Infrastructure.Mapping;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace POA.Web.Models.PowerOfAttorney
{
    public class CategoryModel : IMapFrom<Domain.Category>
    {
        [HiddenInput]
        public int Category_ID { get; set; }

        [DisplayName(" ")]
        public string POASub_Categories { get; set; }    
    }
}