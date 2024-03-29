﻿using StructureMap.Configuration.DSL;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace POA.Web.Infrastructure
{
    public class ControllerRegistry: Registry
    {
        public ControllerRegistry()
        {
            Scan(scan => {
                scan.TheCallingAssembly();
                scan.With(new ControllerConvention());
            });
        }
    }
}