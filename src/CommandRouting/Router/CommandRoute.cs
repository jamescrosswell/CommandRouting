using System;
using System.Threading.Tasks;
using CommandRouting.Helpers;
using CommandRouting.Router;
using Microsoft.AspNet.Http;
using Microsoft.AspNet.Routing;

namespace CommandRouting
{
    public class CommandRoute<TCommand> : IRouter
        where TCommand: ICommand
    {
        public async Task RouteAsync(RouteContext context)
        {                        
            // Work out what kind of request model we should be dealing with
            Type requestType = CommandHelper.GetCommandRequestType<TCommand>();

            // Build a request model from the request request body
            RequestModelParser modelParser = new RequestModelParser(context.HttpContext);
            object requestModel = modelParser.CreateRequestModel(requestType);

            // TODO: Override request model properties from any route template parameters in the request uri

            // TODO: Build a command pipeline from the command and any additional command handlers

            // TODO: Execute the command on the command pipeline

            // TODO: Serialize the appropriate response

            var name = context.RouteData.Values["name"] as string;
            if (String.IsNullOrEmpty(name))
            {
                return;
            }
            await context.HttpContext.Response.WriteAsync($"Hi {name}!");
            context.IsHandled = true;
        }

        public VirtualPathData GetVirtualPath(VirtualPathContext context)
        {
            return null;
        }
    }
}