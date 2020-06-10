using MediatR;

namespace Chatter.Worker.Requests
{
    public class ChatMessageRequest : IRequest<RequestResult>
    {
        public string Message { get; set; }
    }
}
