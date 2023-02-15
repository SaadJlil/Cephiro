using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Cephiro.Listings.Presentation.Api.Catalog.Endpoints;
using Cephiro.Listings.Application.Catalog.Contracts.Request;
using Cephiro.Listings.Application.Catalog.Contracts.Response;
using Microsoft.AspNetCore.Mvc;

namespace Cephiro.Api.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class ListingController : ControllerBase
    {
        
        /*
        private readonly CreateListingEndpoint? _endpoint;

        public ListingController(CreateListingEndpoint endpoint)
        {
            _endpoint = endpoint;
        }

        [HttpPost]
        public async Task<ActionResult<CreationResponse>> CreateListing(CreationRequest dto)
        {
            return await _endpoint.HandleAsync(dto);
        }
        */
    }
}