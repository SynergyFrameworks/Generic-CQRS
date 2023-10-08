using System.Threading.Tasks;

namespace OZK.Infrastructure.Messages
{
    public interface IHandler<in T> where T : IMessage
    {
        Task Handle(T message);
    }
}
