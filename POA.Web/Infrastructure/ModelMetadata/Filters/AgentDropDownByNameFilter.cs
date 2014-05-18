using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace POA.Web.Infrastructure.ModelMetadata.Filters
{
    public class AgentDropDownByNameFilter : IModelMetadataFilter
    {
        public void TransformMetadata(System.Web.Mvc.ModelMetadata metadata,
            IEnumerable<Attribute> attributes)
        {
            if (!string.IsNullOrEmpty(metadata.PropertyName) &&
                string.IsNullOrEmpty(metadata.DataTypeName) &&
                metadata.PropertyName.ToLower().Contains("agent"))
            {
                metadata.DataTypeName = "Agent";
            }
        }
    }
}