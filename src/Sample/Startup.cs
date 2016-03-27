using System.Threading.Tasks;
using CommandRouting.Config;
using Microsoft.AspNet.Builder;
using Microsoft.AspNet.Hosting;
using Microsoft.AspNet.Http;
using Microsoft.Extensions.DependencyInjection;
using Sample.Commands.SayHello;

namespace Sample
{
    public class Startup
    {
        // This method gets called by the runtime and can be used to add services to the DI container.
        public void ConfigureServices(IServiceCollection services)
        {
            services.AddRouting();
            services.AddCommandRouting();
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app)
        {
            app.UseIISPlatformHandler();

            var commandRoutes = new CommandRouteBuilder(app.ApplicationServices);

            commandRoutes
                .Get("hello/{name:alpha}")
                .As<SayHelloRequest>()
                .RoutesTo<IgnoreBob, SayHello>();

            commandRoutes
                .Post("hello")
                .As<SayHelloRequest>()
                .RoutesTo<PostHello>();

            commandRoutes.AddAttributeRouting();

            app.UseRouter(commandRoutes.Build());

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
