﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using StructureMap;
using System.Web.Mvc;

namespace POA.Web.Infrastructure
{
    public class StructureMapDependencyResolver: IDependencyResolver
    {
        private readonly Func<IContainer> _containerFactory;
        public StructureMapDependencyResolver(Func<IContainer> containerFactory)
        {
            _containerFactory = containerFactory;
        }
        public object GetService(Type serviceType)
        {
           if(serviceType == null)
           {
               return null;
           }

           var container = _containerFactory();

           return serviceType.IsAbstract || serviceType.IsInterface
               ? container.TryGetInstance(serviceType)
               : container.GetInstance(serviceType);
        }

        public IEnumerable<object> GetServices(Type serviceType)
        {
            return _containerFactory().GetAllInstances(serviceType).Cast<object>();
        }
    }
}