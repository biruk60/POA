﻿using AutoMapper;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace POA.Web.Infrastructure.Mapping
{
   public interface IHaveCustomMappings
    {
       void CreateMappings(IConfiguration configuration);
    }
}
