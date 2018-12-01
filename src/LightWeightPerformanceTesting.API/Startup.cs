using FluentValidation.AspNetCore;
using LightWeightPerformanceTesting.Core;
using LightWeightPerformanceTesting.Core.Behaviours;
using LightWeightPerformanceTesting.Core.Extensions;
using LightWeightPerformanceTesting.Core.Interfaces;
using LightWeightPerformanceTesting.Core.Identity;
using LightWeightPerformanceTesting.Infrastructure.Extensions;
using LightWeightPerformanceTesting.Infrastructure;
using LightWeightPerformanceTesting.Infrastructure.Data;
using MediatR;
using Microsoft.AspNetCore.Builder;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using LightWeightPerformanceTesting.Core.Common;
using System;
using System.Threading;

namespace LightWeightPerformanceTesting.API
{
    public class Startup
    {
        public Startup(IConfiguration configuration)
            => Configuration = configuration;
        
        public IConfiguration Configuration { get; }

        public void ConfigureServices(IServiceCollection services)
        {
            services.AddSingleton<IDateTime, MachineDateTime>();
            services.AddTransient<IEventStore, EventStore>();
            services.AddHttpContextAccessor();
            services.AddSingleton<IRepository, Repository>();
            services.AddSingleton<ICommandPreProcessor, CommandPreProcessor>();
            services.AddSingleton<ICommandRegistry, CommandRegistry>();
            services.AddHostedService<QueuedHostedService>();
            services.AddSingleton<IBackgroundTaskQueue, BackgroundTaskQueue>();

            services.AddCustomMvc()
                .AddFluentValidation(cfg => { cfg.RegisterValidatorsFromAssemblyContaining<Startup>(); });

            services
                .AddCustomSecurity(Configuration)
                .AddCustomSignalR()
                .AddCustomSwagger()
                .AddDataStore(Configuration["Data:DefaultConnection:ConnectionString"],Configuration.GetValue<bool>("isTest"))
                .AddTransient(typeof(IPipelineBehavior<,>), typeof(ValidationBehavior<,>))
                .AddMediatR(typeof(Startup).Assembly);
        }

        public void Configure(IApplicationBuilder app)
        {
            app.UseMiddleware<ByPassAuthMiddleware>();
                    
            app.UseAuthentication()            
                .UseCors(CorsDefaults.Policy)            
                .UseMvc()
                .UseSignalR(routes => routes.MapHub<IntegrationEventsHub>("/hub"))
                .UseSwagger()
                .UseSwaggerUI(options =>
                {
                    options.SwaggerEndpoint("/swagger/v1/swagger.json", "LightWeightPerformanceTesting API");
                    options.RoutePrefix = string.Empty;
                });
            
            //if (Configuration.GetValue<bool>("isCI"))                
                new Timer((Object stateInfo) => { Environment.Exit(0); }, null, 10000, 10000);
            
        }        
    }


}
