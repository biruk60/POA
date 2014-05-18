﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace POA.Web.Infrastructure.ModelMetadata.Filters
{
    public class PrincipalDropDownByNameFilter : IModelMetadataFilter
    {
        public void TransformMetadata(System.Web.Mvc.ModelMetadata metadata,
            IEnumerable<Attribute> attributes)
        {
            if (!string.IsNullOrEmpty(metadata.PropertyName) &&
                string.IsNullOrEmpty(metadata.DataTypeName) &&
                metadata.PropertyName.ToLower().Contains("principal"))
            {
                metadata.DataTypeName = "Principal";
            }
        }
    }
}