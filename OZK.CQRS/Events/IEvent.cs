using OZK.Datalayer.Contracts;
using OZK.Infrastructure.Messages;
using System;

namespace OZK.Infrastructure.Events
{
    public interface IEvent : IEntity, IMessage
    {
        int Version { get; set; }
        DateTimeOffset TimeStamp { get; set; }
    }
}
