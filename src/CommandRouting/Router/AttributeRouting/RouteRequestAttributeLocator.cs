//using System;
//using System.Collections.Generic;
//using System.Linq;
//using System.Reflection;

//namespace CommandRouting.Router.AttributeRouting
//{
//    internal interface IRouteRequestAttributeLocator
//    {
//        IEnumerable<RouteRequestDeclaration> GetRequestRouteAttribute();
//    }

//    internal sealed class RouteRequestAttributeLocator : IRouteRequestAttributeLocator
//    {
//        /// <summary>
//        /// Creates RouteRequestDeclarations by inspecting all non abstract classes annotated with RouteRequestAttribute
//        /// </summary>
//        /// <returns></returns>
//        public IEnumerable<RouteRequestDeclaration> GetRequestRouteAttribute()
//        {
//            return from asm in AppDomain.CurrentDomain.GetAssemblies().AsParallel()
//                   from type in asm.GetTypes()
//                   where type.IsClass && !type.IsAbstract
//                   let attrib = type.GetCustomAttributes(typeof(RouteRequestAttribute), true)
//                   where attrib != null && attrib.Length > 0
//                   select new RouteRequestDeclaration(type, attrib.Cast<RouteRequestAttribute>());
//        }
//    }
//}
