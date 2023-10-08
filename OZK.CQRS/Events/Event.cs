using System;

namespace OZK.Infrastructure.Events
{
    public abstract class Event : IEvent
    {
     
        public int Version { get; set; }
        public DateTimeOffset TimeStamp { get; set; }
        public Guid Id { get => throw new NotImplementedException(); set => throw new NotImplementedException(); }
    }
}
