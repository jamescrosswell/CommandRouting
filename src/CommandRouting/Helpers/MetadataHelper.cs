using System;
using Microsoft.AspNet.Mvc.ModelBinding;

namespace CommandRouting.Helpers
{
    public static class MetadataHelper
    {
        private static readonly EmptyModelMetadataProvider Provider = new EmptyModelMetadataProvider();

        public static ModelMetadata MetaData(this Type type)
        {
            return Provider.GetMetadataForType(type);
        }
    }
}
