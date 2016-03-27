using System.Reflection;
using CommandRouting.Router.AttributeRouting;

namespace CommandRouting.Config
{
    public static class AttributeRoutingCommandRouteBuilderExtensions
    {
        public static void AddAttributeRouting(this CommandRouteBuilder builder)
        {
            var attributeLocator = new RouteRequestAttributeLocator();

            var declarations = attributeLocator.GetRequestRouteAttribute();
            foreach (var routeRequestDeclaration in declarations)
            {
                var addRoute = builder.GetType()
                            .GetMethod(nameof(builder.AddRoute), BindingFlags.NonPublic | BindingFlags.Instance)
                            .MakeGenericMethod(routeRequestDeclaration.RequestType);

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
