using System;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using AttributeRouting.Web.Http;
using DDD.Light.CQRS.Contracts;
using DDD.Light.Realtor.API.Query.Contract;

namespace DDD.Light.Realtor.REST.API.Controllers
{
    public class ListingsController : ApiController
    {
        private readonly ICommandBus _commandBus;
        private readonly IActiveListings _activeListings;

        public ListingsController(ICommandBus commandBus, IActiveListings activeListings)
        {
            _commandBus = commandBus;
            _activeListings = activeListings;
        }
        
        [GET("api/listings")]
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
