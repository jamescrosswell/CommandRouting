using System;
using Microsoft.AspNetCore.Mvc.ModelBinding;

namespace CommandRouting
{
    public static class TypeExtensions
    {
        private static readonly EmptyModelMetadataProvider Provider = new EmptyModelMetadataProvider();

        public static ModelMetadata MetaData(this Type type)
        {
            return Provider.GetMetadataForType(type);
        }
    }
}
