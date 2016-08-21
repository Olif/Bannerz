using System;

namespace Domain
{

    /// <summary>
    /// Generates a valid id. Makes it possible to assign a id to an entity upfront which in turn makes it possible 
    /// to save an entity to a db using only one db-transaction.
    /// </summary>
    public interface IIdGenerator
    {
        int Next();
    }
}