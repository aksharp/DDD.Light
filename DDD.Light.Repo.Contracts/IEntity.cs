using System;

namespace DDD.Light.Repo.Contracts
{
    public interface IEntity
    {
        Guid? Id { get; set; }
    }
}