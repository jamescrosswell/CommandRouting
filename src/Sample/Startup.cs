using System.Threading.Tasks;
using CommandRouting;
using CommandRouting.Config;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;
using Newtonsoft.Json;
using Sample.Commands.Account;
using Sample.Commands.Jump;
using Sample.Commands.Logo;
using Sample.Commands.SayHello;

namespace Sample
{
    public class Startup
    {
        public Startup(IHostingEnvironment env)
        {
            var builder = new ConfigurationBuilder()
                .SetBasePath(env.ContentRootPath)
                .AddJsonFile("appsettings.json", optional: true, reloadOnChange: true)
                .AddJsonFile($"appsettings.{env.EnvironmentName}.json", optional: true)
                .AddEnvironmentVariables();
            Configuration = builder.Build();
        }

        private IConfigurationRoot Configuration { get; }

        // This method gets called by the runtime and can be used to add services to the DI container.
        public void ConfigureServices(IServiceCollection services)
        {
            // Enable command routing
            services
                .AddMvcCore()
                .AddJsonFormatters(settings =>
                {
                    settings.Formatting = Formatting.Indented;
                });
            services.AddRouting();
            services.AddCommandRouting();

            // Configure context for route pipelines that depend on this
            services.AddScoped<JumpContext>();
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, ILoggerFactory loggerFactory)
        {
            var commandRoutes = new CommandRouteBuilder(app);

            commandRoutes
                .Get("hello/{name:alpha}")
                .As<SayHelloRequest>()
                .RoutesTo<IgnoreBob, SayHello>();

            commandRoutes
                .Post("hello")
                .As<SayHelloRequest>()
                .RoutesTo<PostHello>();

            commandRoutes
                .Get("logo")
                .As<Unit>()
                .RoutesTo<DownloadLogo>();

            commandRoutes.Map("account").To<AccountCommands>();
            commandRoutes.Map("jump").To<JumpCommands>();

            //commandRoutes.AddAttributeRouting();

            // Configure logging
            loggerFactory.AddConsole(Configuration.GetSection("Logging"));
            loggerFactory.AddDebug();

            // Finally configure MVC
            app.UseMvc()
               .UseRouter(commandRoutes.Build());
        }

        public async Task HelloWorld(HttpContext context)
        {
            await context.Response.WriteAsync("Nothing here...");
        }        
    }
}
