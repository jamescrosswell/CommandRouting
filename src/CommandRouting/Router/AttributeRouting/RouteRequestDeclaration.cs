using CommandRouting.Helpers;
using System;
using System.Collections.Generic;

namespace CommandRouting.Router.AttributeRouting
{
    internal sealed class RouteRequestDeclaration
    {
        public Type RequestType { get; }
        public IEnumerable<RouteRequestAttribute> RouteRequestAttributes { get; }

        public RouteRequestDeclaration(Type requestType, IEnumerable<RouteRequestAttribute> routeRequestAttributes)
        {
            if (requestType == null) throw new ArgumentNullException(nameof(requestType));
            if (routeRequestAttributes == null) throw new ArgumentNullException(nameof(routeRequestAttributes));
            RequestType = requestType;
            RouteRequestAttributes = routeRequestAttributes;
        }
    }
}