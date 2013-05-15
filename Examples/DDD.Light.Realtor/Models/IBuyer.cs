using System.Collections.Generic;
using DDD.Light.Repo.Contracts;

namespace DDD.Light.Realtor.Models
{
    public interface IBuyer : IEntity
    {
        IEnumerable<Offer> Offers { get; set; }
        string Email { get; set; }
        Prospect Prospect { get; set; }
    }
}