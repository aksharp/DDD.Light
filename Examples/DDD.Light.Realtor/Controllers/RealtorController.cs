using System;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using AttributeRouting.Web.Http;
using AutoMapper;
using DDD.Light.Messaging;
using DDD.Light.Realtor.Application.Commands;
using DDD.Light.Realtor.Resources;

namespace DDD.Light.Realtor.Controllers
{
    public class RealtorController : ApiController
    {
        [POST("api/realtor/{realtorId}/listings")]
        public HttpResponseMessage PostListing([FromBody]RealtorListingResource realtorListingResource)
        {
            var postListingCommand = Mapper.Map<RealtorListingResource, PostListingCommand>(realtorListingResource);
            postListingCommand.ListingId = Guid.NewGuid();
            try
            {
                CommandBus.Instance.Dispatch(postListingCommand);
            }
            catch (Exception ex)
            {
                return Request.CreateResponse(HttpStatusCode.InternalServerError, ex);
            }
            return Request.CreateResponse(HttpStatusCode.Created, postListingCommand);
        }
    }
}
