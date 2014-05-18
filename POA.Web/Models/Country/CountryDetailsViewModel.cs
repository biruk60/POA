using POA.Web.Infrastructure.Mapping;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace POA.Web.Models.Country
{
    public class CountryDetailsViewModel : IMapFrom<Domain.Country>
    {
        public int Country_ID { get; set; }
        public string Name { get; set; }
        public string Description { get; set; }
    }
}