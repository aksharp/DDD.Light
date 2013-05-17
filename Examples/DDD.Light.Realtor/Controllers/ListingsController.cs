using System;
using System.Collections.Generic;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using DDD.Light.Realtor.Core.Domain.Model;
using DDD.Light.Repo.Contracts;

namespace DDD.Light.Realtor.REST.API.Controllers
{
    public class ListingsController : ApiController
    {
        private readonly IRepository<Listing> _listingRepository;

        public ListingsController(IRepository<Listing> listingRepository)
        {
            _listingRepository = listingRepository;
        }

        // GET api/listings
        public IEnumerable<Listing> Get()
        {
            return _listingRepository.GetAll();
        }

        // GET api/listings/ecf4dbf5-b8b2-4529-84bc-4117cf106227
        public Listing Get(Guid id)
        {
            return _listingRepository.GetById(id);
        }

        // POST api/listings
        public HttpResponseMessage Post([FromBody]Listing listing)
        {
            listing.Id = Guid.NewGuid();

            try
            {
                _listingRepository.Save(listing);
            }
            catch (Exception ex)
            {
                return Request.CreateResponse(HttpStatusCode.InternalServerError, ex);
            }
            return Request.CreateResponse(HttpStatusCode.Created, listing);
        }

        // PUT api/listings/ecf4dbf5-b8b2-4529-84bc-4117cf106227
        public HttpResponseMessage Put(Guid id, [FromBody]Listing listing)
        {
            try
            {
                listing.Id = id;
                _listingRepository.Save(listing);
            }
            catch (Exception ex)
            {
                return Request.CreateResponse(HttpStatusCode.InternalServerError, ex);
            }
            return Request.CreateResponse(HttpStatusCode.OK, listing);
        }

        // DELETE api/listings/ecf4dbf5-b8b2-4529-84bc-4117cf106227
        public HttpResponseMessage Delete(Guid id)
        {
            try
            {
                _listingRepository.Delete(id);
            }
            catch (Exception ex)
            {
                return Request.CreateResponse(HttpStatusCode.InternalServerError, ex);
            }
            return Request.CreateResponse(HttpStatusCode.NoContent);
        }

        public HttpResponseMessage Patch(Guid id, [FromBody]string street)
        {
            var listing = _listingRepository.GetById(id);
            try
            {                
                listing.Location.Street = street;
                _listingRepository.Save(listing);
                return Request.CreateResponse(HttpStatusCode.OK, listing);
            }
            catch (Exception ex)
            {
                return Request.CreateResponse(HttpStatusCode.InternalServerError, ex);
            }            
        }
    }
}
