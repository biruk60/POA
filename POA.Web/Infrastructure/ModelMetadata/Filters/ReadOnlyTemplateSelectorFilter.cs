using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace POA.Web.Infrastructure.ModelMetadata.Filters
{
    public class ReadOnlyPOASelectorFilter : IModelMetadataFilter
    {
        public void TransformMetadata(System.Web.Mvc.ModelMetadata metadata,
            IEnumerable<Attribute> attributes)
        {
            if (metadata.IsReadOnly &&
                string.IsNullOrEmpty(metadata.DataTypeName))
            {
                metadata.DataTypeName = "ReadOnly";
            }
        }
    }
}