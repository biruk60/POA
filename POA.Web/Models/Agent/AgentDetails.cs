using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace POA.Web.Models.Agent
{
    public class AgentDetails
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
        public string BirthName { get; set; }
        public string CitizenName { get; set; }
        public string ResidenceName { get; set; }
        public string TitleName { get; set; }
        public string Email { get; set; }
        public string Phone_No { get; set; }
    }
}