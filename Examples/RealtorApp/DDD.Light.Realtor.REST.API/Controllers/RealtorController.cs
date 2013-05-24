using System;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using AttributeRouting.Web.Http;
using AutoMapper;
using DDD.Light.Messaging;
using DDD.Light.Messaging.InProcess;
using DDD.Light.Realtor.API.Command.Realtor;
using DDD.Light.Realtor.API.Query;
using DDD.Light.Realtor.API.Query.Contract;
using DDD.Light.Realtor.REST.API.Resources;

namespace DDD.Light.Realtor.REST.API.Controllers
{
    public class RealtorController : ApiController
    {
        private readonly IActiveListings _activeListings;

        public RealtorController(IActiveListings activeListings)
        {
            _activeListings = activeListings;
        }

        [POST("api/realtor/listings")]
        public HttpResponseMessage PostListing([FromBody]RealtorListing realtorListing)
        {
            var postListing = Mapper.Map<RealtorListing, PostListing>(realtorListing);
            postListing.ListingId = Guid.NewGuid();
            try
            {
                CommandBus.Instance.Dispatch(postListing);
            }
            catch (Exception ex)
            {
                return Request.CreateResponse(HttpStatusCode.InternalServerError, ex);
            }
            return Request.CreateResponse(HttpStatusCode.Created, postListing);
        }
        
        [GET("api/realtor/listings")]
        public HttpResponseMessage GetListings()
        {
            try
            {
                 var listings = _activeListings.All();
                 return Request.CreateResponse(HttpStatusCode.OK, listings);
            }
            catch (Exception ex)
            {
                return Request.CreateResponse(HttpStatusCode.InternalServerError, ex);
            }
            
        }
    }
}
