//using AutoMapper;
//
//namespace DDD.Light.Realtor.Application
//{
//    public class CoreDomainToQueryDomainMappings
//    {
//        public static void Configure()
//        {
//            Mapper.CreateMap<Core.Domain.Model.Listing.AggregateRoot.Listing, API.Query.Model.ActiveListing>()
//                  .ForMember(dest => dest.City, opt => opt.MapFrom(src => src.Location.City))
//                  .ForMember(dest => dest.NumberOfBathrooms, opt => opt.MapFrom(src => src.Description.NumberOfBathrooms))
//                  .ForMember(dest => dest.NumberOfBedrooms, opt => opt.MapFrom(src => src.Description.NumberOfBedrooms))
//                  .ForMember(dest => dest.State, opt => opt.MapFrom(src => src.Location.State))
//                  .ForMember(dest => dest.Street, opt => opt.MapFrom(src => src.Location.Street))
//                  .ForMember(dest => dest.YearBuilt, opt => opt.MapFrom(src => src.Description.YearBuilt))
//                  .ForMember(dest => dest.Zip, opt => opt.MapFrom(src => src.Location.Zip));
//        }
//    }
//}
