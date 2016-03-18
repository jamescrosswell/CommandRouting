using System.Threading.Tasks;
using CommandRouting.Configure;
using Microsoft.AspNet.Builder;
using Microsoft.AspNet.Hosting;
using Microsoft.AspNet.Http;
using Microsoft.AspNet.Routing;
using Microsoft.Extensions.DependencyInjection;
using Sample.Commands.SayHello;

namespace Sample
{
    public class Startup
    {
        // This method gets called by the runtime. Use this method to add services to the container.
        // For more information on how to configure your application, visit http://go.microsoft.com/fwlink/?LinkID=398940
        public void ConfigureServices(IServiceCollection services)
        {
            services.AddRouting();
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app)
        {
            app.UseIISPlatformHandler();

            RouteBuilder routeBuilder = new RouteBuilder {ServiceProvider = app.ApplicationServices};

            CommandRouteBuilder pipelines = new CommandRouteBuilder(routeBuilder);
            pipelines
                .Pipe("hello/{name:alpha}")
                .As<SayHelloRequest>()
                .To<IgnoreBob, SayHello>();

            app.UseRouter(routeBuilder.Build());

            app.Run(HelloWorld);
        }

        public async Task HelloWorld(HttpContext context)
        {
            await context.Response.WriteAsync("Nothing here...");
        }

        // Entry point for the application.
        public static void Main(string[] args) => WebApplication.Run<Startup>(args);
    }
}
