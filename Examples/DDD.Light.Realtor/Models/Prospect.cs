using System.Collections.Generic;
using DDD.Light.Repo.Contracts;

namespace DDD.Light.Realtor.Models
{
    public class Prospect : Entity
    {
        public IEnumerable<Property> PropertiesViewed { get; set; }
    }
}