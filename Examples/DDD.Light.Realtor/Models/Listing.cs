using DDD.Light.Repo.Contracts;

namespace DDD.Light.Realtor.Models
{
    // aggregate root (AR)
    public class Listing : Entity
    {
        public Listing()
        {
            Address = new Address();
            PropertyInfo = new PropertyInfo();
        }

        public Address Address { get; set; }
        public PropertyInfo PropertyInfo { get; set; }
    }

}