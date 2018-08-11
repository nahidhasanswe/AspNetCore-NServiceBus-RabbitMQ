using System.Threading.Tasks;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using NServiceBus.Common.Messages;

namespace NServiceBus.Client
{
    public class Startup
    {
        public Startup(IConfiguration configuration)
        {
            Configuration = configuration;
        }

        public IConfiguration Configuration { get; }

        // This method gets called by the runtime. Use this method to add services to the container.
        public void ConfigureServices(IServiceCollection services)
        {
            services.AddMvc();

            SetupNServiceBus(services).GetAwaiter().GetResult();
        }

        private async Task SetupNServiceBus(IServiceCollection services)
        {
            var endpointConfiguration = new EndpointConfiguration("ClientUI");

            var transport = endpointConfiguration.UseTransport<RabbitMQTransport>();
            transport.ConnectionString("host = localhost; username = admin; password = a").UseDirectRoutingTopology();
            transport.UsePublisherConfirms(true);

            endpointConfiguration.EnableCallbacks();

            endpointConfiguration.MakeInstanceUniquelyAddressable("Client");

            var routing = transport.Routing();
            routing.RouteToEndpoint(typeof(RequestMessage), "ServerSide");

            var _endpointInstance = await Endpoint.Start(endpointConfiguration)
                .ConfigureAwait(false);

            services.AddSingleton(_endpointInstance);
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IHostingEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }

            app.UseMvc();
        }
    }
}
