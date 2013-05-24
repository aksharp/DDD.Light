namespace DDD.Light.Realtor.Core.Domain.Model.Listing
{
    // value object
    public class Description
    {
        public int NumberOfBedrooms { get; private set; }
        public int NumberOfBathrooms { get; private set; }
        public int YearBuilt { get; private set; }

        public Description(int numberOfBedrooms, int numberOfBathrooms, int yearBuilt)
        {
            NumberOfBedrooms = numberOfBedrooms;
            NumberOfBathrooms = numberOfBathrooms;
            YearBuilt = yearBuilt;
        }

    }
}