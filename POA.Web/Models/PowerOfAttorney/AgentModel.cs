using POA.Web.Infrastructure.Mapping;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Web;

namespace POA.Web.Models.PowerOfAttorney
{
    public class AgentModel : IMapFrom<Domain.Agent>
    {

        [DisplayName("Agent")]
        public String Agent_ID { get; set; }  
    }
}