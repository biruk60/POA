using StructureMap.Configuration.DSL;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using POA.Web.Infrastructure.Tasks;

namespace POA.Web.Infrastructure
{
    public class TaskRegistry : Registry
    {
        public TaskRegistry()
        {
            Scan(scan => {
                scan.AssembliesFromApplicationBaseDirectory(a => a.FullName.StartsWith("POA"));
                scan.AddAllTypesOf<IRunAtInit>();
                scan.AddAllTypesOf<IRunAfterEachRequest>();
                scan.AddAllTypesOf<IRunAtStartup>();
                scan.AddAllTypesOf<IRunOnEachRequest>();
                scan.AddAllTypesOf<IRunOnError>();
            }
                );

        }
    }
}