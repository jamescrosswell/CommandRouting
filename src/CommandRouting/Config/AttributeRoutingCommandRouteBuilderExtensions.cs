using System;
using System.Reflection;
using CommandRouting.Router.AttributeRouting;

namespace CommandRouting.Config
{
    public static class AttributeRoutingCommandRouteBuilderExtensions
    {
        public static void AddAttributeRouting(this ICommandRouteBuilder builder)
        {
            var attributeLocator = new RouteRequestAttributeLocator();

            var declarations = attributeLocator.GetRequestRouteAttribute();
            foreach (var routeRequestDeclaration in declarations)
            {
                var addRoute = builder.GetType()
                            .GetMethod(nameof(builder.AddRoute), BindingFlags.Public | BindingFlags.Instance)
                            ?.MakeGenericMethod(routeRequestDeclaration.RequestType);
                if (addRoute == null)
                    throw new ArgumentException($"No matching { nameof(builder.AddRoute)} method found", nameof(builder));

                foreach (var routeRequestAttribute in routeRequestDeclaration.RouteRequestAttributes)
                {
                    addRoute.Invoke(builder, new object[]
                    {
                        routeRequestAttribute.HttpVerb,
                        routeRequestAttribute.RouteTemplate,
                        routeRequestAttribute.CommandHandlers
                    });
                }
            }
        }
    }
}
