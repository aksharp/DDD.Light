using DDD.Light.Repo.Contracts;

namespace DDD.Light.Repo.MongoDB.Example
{
    public class Person : Entity
    {
        public string FirstName { get; set; }
        public string LastName { get; set; }
    }
}
