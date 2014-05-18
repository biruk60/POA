using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using POA.Web.Infrastructure.Mapping;

namespace POA.Web.Models.Agent
{
    public class AgentSummaryViewModel : IMapFrom<Domain.Agent>
    {
        public int Agent_ID { get; set; }
        public string First_Name { get; set; }
        public string Last_Name { get; set; }
        public string Middle_Name { get; set; }
        public string City { get; set; }
        public string KifleKetema { get; set; }
        public string Wereda { get; set; }
        public string House_No { get; set; }
        public string ID_No { get; set; }
        public int Country_Of_Birth_ID { get; set; }
        public int Country_Of_CitizenShip_ID { get; set; }
        public int Country_Of_Residence_ID { get; set; }
        public int Title_ID { get; set; }
        public string Email { get; set; }
        public string Phone_No { get; set; }
    }
}