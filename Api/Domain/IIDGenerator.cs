using System;

namespace Api.Domain
{
    public interface IIdGenerator
    {
        int Next();
    }
}