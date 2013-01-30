using System;

namespace HomeManager.Domain.Entities.Core
{
    public interface IEntity
    {
        Guid Key { get; set; }
    }
}