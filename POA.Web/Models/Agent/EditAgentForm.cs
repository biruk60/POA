using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using POA.Web.Infrastructure.Mapping;

namespace POA.Web.Models.Agent
{
    public class EditAgentForm : IMapFrom<Domain.Agent>
    {
        [DisplayName("Title")]
        public string Title_ID { get; set; }

        [HiddenInput]
        public int Agent_ID { get; set; }

        [Required, DisplayName("First Name")]
        public string First_Name { get; set; }

        [Required, DisplayName("Last Name")]
        public string Last_Name { get; set; }

        [Required, DisplayName("Middle Name")]
        public string Middle_Name { get; set; }

        [Required]
        public string City { get; set; }

        [Required]
        public string KifleKetema { get; set; }

        [Required]
        public string Wereda { get; set; }

        [Required, DisplayName("House No.")]
        public string House_No { get; set; }

        [Required, DisplayName("ID No.")]
        public string ID_No { get; set; }

        [DisplayName("Country Of Birth")]
        public string Country_Of_Birth_ID { get; set; }

        [DisplayName("Country Of CitizenShip")]
        public string Country_Of_CitizenShip_ID { get; set; }

        [DisplayName("Country Of Residence")]
        public string Country_Of_Residence_ID { get; set; }


        [DataType(DataType.EmailAddress)]
        public string Email { get; set; }
        [DisplayName("Phone")]
        public string Phone_No { get; set; }
    }
}