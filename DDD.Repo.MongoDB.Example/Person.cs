using DDD.Repo.Contracts;

namespace DDD.Repo.MongoDB.Example
{
    public class Person : Entity
    {
        public string FirstName { get; set; }
        public string LastName { get; set; }
    }
}
