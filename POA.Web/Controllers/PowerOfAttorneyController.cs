using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace POA.Web.Controllers
{
    public class PowerOfAttorneyController : BaseController
    {
        // GET: PowerOfAttorney
        public ActionResult Index()
        {
            return View();
        }

        public ActionResult New()
        {            
            return View();
        }
    }
}