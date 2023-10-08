using System;

namespace OZK.Domain.CQRS.Enums
{
    [Flags]
    public enum SearchLevel
    {
        Simple = 1,
        Full = 2
    }

}
