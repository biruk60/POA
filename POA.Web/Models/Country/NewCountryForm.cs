using POA.Web.Infrastructure.Mapping;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace POA.Web.Models.Country
{
    public class NewCountryForm : IMapFrom<Domain.Country>
    {
        [HiddenInput]
        public int Country_ID { get; set; }

        [Required]
        public string Name { get; set; }

        [DataType(DataType.MultilineText)]
        public string Description { get; set; }
    }
}