using System;

namespace DDD.Light.Realtor.API.Command.Realtor
{
    public class SetUpRealtor
    {
        public Guid RealtorId { get; private set; }

        public SetUpRealtor(Guid realtorId)
        {
            RealtorId = realtorId;
        }
    }
}
