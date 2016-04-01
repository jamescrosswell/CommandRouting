using System.Linq;
using System;
using CommandRouting.Handlers;
using CommandRouting.Router;
using CommandRouting.Router.AttributeRouting;
using Xunit;

namespace CommandRouting.UnitTests.Router.AttributeRouting
{
    public class RouteRequestAttributeLocatorTests
    {
        [GetRouteRequest("api/no-handler")]
        private class MissingHandlerRequest { }

        [GetRouteRequest("api/", typeof(ApiHandler))]
        private class SingleHandlerRequest { }

        [GetRouteRequest("api/route1", typeof(ApiHandler))]
        [GetRouteRequest("api/route2", typeof(ApiHandler))]
        private class TwoRoutesAppliedRequest { }

        [GetRouteRequest("api/done-twice", typeof(ApiHandler))]
        [GetRouteRequest("api/done-twice", typeof(ApiHandler))]
        private class DuplicateRoutesAppliedRequest { }

        private class ApiHandler : IRequestHandler<SingleHandlerRequest>
        {
            public HandlerResult Dispatch(SingleHandlerRequest request) { return new NotHandled(); }
        }

        private readonly RouteRequestAttributeLocator _target = new RouteRequestAttributeLocator();

        [Theory]
        [InlineData(typeof(MissingHandlerRequest))]
        [InlineData(typeof(SingleHandlerRequest))]
        [InlineData(typeof(TwoRoutesAppliedRequest))]
        [InlineData(typeof(DuplicateRoutesAppliedRequest))]
        public void GetRequestRouteAttribute_should_locate_RequestType(Type requestType)
        {
            var declarations = _target.GetRequestRouteAttribute();

            var actual = declarations.SingleOrDefault(d => d.RequestType == requestType);
            Assert.NotNull(actual);
        }
    }
}
