using System;
using System.Threading.Tasks;

namespace NServiceBus.Server
{
    class Program
    {
        static void Main(string[] args)
        {
            Console.WriteLine("Hello World!");
            SetupNserviceBus().GetAwaiter().GetResult();
        }

        private static async Task SetupNserviceBus()
        {
            var endpointConfiguration = new EndpointConfiguration("ServerSide");

            var transport = endpointConfiguration.UseTransport<RabbitMQTransport>();
            transport.ConnectionString("host = localhost; username = admin; password = a").UseDirectRoutingTopology();
            transport.UsePublisherConfirms(true);

            var endpointInstance = await Endpoint.Start(endpointConfiguration)
                .ConfigureAwait(false);

            Console.WriteLine("Press Enter to exit.");
            Console.ReadLine();

            await endpointInstance.Stop()
                .ConfigureAwait(false);
        }
    }
}
