using System;
using System.Collections.Generic;
using DDD.Light.Repo.Contracts;

namespace DDD.Light.Realtor.Models
{
    public class Buyer : Entity, IBuyer
    {
        public IEnumerable<Offer> Offers { get; set; }
        public string Email { get; set; }
        public Prospect Prospect { get; set; }
    }
}