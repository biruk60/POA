using POA.Web.Infrastructure.Mapping;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Web;

namespace POA.Web.Models.PowerOfAttorney
{
    public class PrincipalModel: IMapFrom<Domain.Agent>
    {

        [DisplayName("Principal")]
        public String Principal_ID { get; set; }  
    }
   
}