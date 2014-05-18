using StructureMap.Configuration.DSL;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Optimization;
using System.Web.Routing;
using System.Security.Principal;

namespace POA.Web.Infrastructure
{
    public class MVCRegistry: Registry
    {
        public MVCRegistry()
        {
            For<BundleCollection>().Use(BundleTable.Bundles);
            For<RouteCollection>().Use(RouteTable.Routes);
            For<IIdentity>().Use(()=> HttpContext.Current.User.Identity);
            For<HttpSessionStateBase>().Use(() => new HttpSessionStateWrapper(HttpContext.Current.Session));
            For<HttpContextBase>().Use(()=> new HttpContextWrapper(HttpContext.Current));
            For<HttpServerUtilityBase>().Use(()=> new HttpServerUtilityWrapper(HttpContext.Current.Server));
        }
    }
}