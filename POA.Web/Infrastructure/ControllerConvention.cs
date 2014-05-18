using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using StructureMap;
using StructureMap.Graph;
using StructureMap.Configuration.DSL;
using System.Web.Mvc;
using StructureMap.Pipeline;
using StructureMap.TypeRules;

namespace POA.Web.Infrastructure
{
    public class ControllerConvention: IRegistrationConvention
    {
        public void Process(Type type, Registry registry)
        {
            if(type.CanBeCastTo(typeof(Controller))&& !type.IsAbstract)
            {
                registry.For(type).LifecycleIs(new UniquePerRequestLifecycle());
            }
        }
    }
}