using System;
using System.Collections.Generic;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using DDD.Light.Realtor.Models;
using DDD.Light.Repo.Contracts;

namespace DDD.Light.Realtor.Controllers
{
    public class ListingsController : ApiController
    {
        private readonly IRepository<Listing> _listingRepository;

        public ListingsController(IRepository<Listing> listingRepository)
        {
            _listingRepository = listingRepository;
        }

        // GET api/values
        public IEnumerable<Listing> Get()
        {
            return _listingRepository.GetAll();
        }

        // GET api/values/5
        public Listing Get(Guid id)
        {
            return _listingRepository.GetById(id);
        }

        // POST api/values
        public HttpResponseMessage Post(Listing listing)
        {
            listing.Id = Guid.NewGuid();

            try
            {
                _listingRepository.Save(listing);
            }
            catch (Exception ex)
            {
                return Request.CreateResponse(HttpStatusCode.BadRequest, ex);
            }
            return Request.CreateResponse(HttpStatusCode.Created);
        }

        // PUT api/values/5
        public void Put(Listing listing)
        {
            if (listing.Id == null)
                return;
            _listingRepository.Save(listing);
        }

        // DELETE api/values/5
        public void Delete(int id)
        {
        }
    }
}
