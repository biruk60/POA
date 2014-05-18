using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Principal;
using System.Web;
using POA.Domain;
using POA.Web.Models;
using Microsoft.AspNet.Identity;

using System.Web.UI;
using System.Web.Mvc;

namespace POA.Web.Infrastructure
{
    public class CurrentUser: Controller, ICurrentUser
    {
        private readonly IIdentity _identity;
        private readonly POADBEntities _context;
        private ApplicationUser _user;

        public CurrentUser(IIdentity identity, POADBEntities context)
        {
            _identity = identity;
            _context = context;
        }
        public ApplicationUser User
        {
           
           // get { return _user ?? (_user = _context.Users.Find(_identity.GetUserId())); }
            //get { return _user ?? (_user = User.UserName); }

            get { return _user; }
            
           
        }
    }
}