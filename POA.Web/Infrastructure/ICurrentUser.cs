using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using POA.Web.Models;

namespace POA.Web.Infrastructure
{
  public  interface ICurrentUser
    {
      ApplicationUser User { get;  }
    }
}
