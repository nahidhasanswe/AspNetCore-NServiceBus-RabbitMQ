using NServiceBus.Common.Messages;
using System;
using System.Threading.Tasks;

namespace NServiceBus.Server
{
    public class RequestHandler : IHandleMessages<RequestMessage>
    {
        public async Task Handle(RequestMessage message, IMessageHandlerContext context)
        {
            Console.WriteLine(message.Message);

            var reply = new ResponseMessage()
            {
                Message = "This message from server reply against " + message.Message
            };

            await context.Reply(reply);
        }
    }
}
