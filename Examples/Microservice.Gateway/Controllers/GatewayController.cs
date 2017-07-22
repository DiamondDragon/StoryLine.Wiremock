using System;
using Microservice.Gateway.Constants;
using Microservice.Gateway.Contracts;
using Microservice.Gateway.Services;
using Microsoft.AspNetCore.Mvc;

namespace Microservice.Gateway.Controllers
{
    [Route("v1/users")]
    public class GatewayController : Controller
    {
        private const string UserDetailsRoute = "userDetails";

        private readonly IGatewayResource _gatewayResource;

        public GatewayController(IGatewayResource gatewayResource)
        {
            _gatewayResource = gatewayResource ?? throw new ArgumentNullException(nameof(gatewayResource));
        }

        [HttpGet]
        public IActionResult GetAllUsers(int skip = 0, int take = Collections.MaxTake)
        {
            if (skip < 0)
                throw new ArgumentOutOfRangeException(nameof(skip));
            if (take <= 0)
                throw new ArgumentOutOfRangeException(nameof(take));
            if (take > Collections.MaxTake)
                throw new ArgumentOutOfRangeException(nameof(take));

            return Ok(_gatewayResource.GetAllUsers(skip, take));
        }

        [HttpGet("{id}", Name = UserDetailsRoute)]
        public IActionResult GetUser(long id)
        {
            if (id <= 0)
                throw new ArgumentOutOfRangeException(nameof(id));

            var user = _gatewayResource.GetUser(id);
            if (user == null)
                return NotFound();

            return Ok(user);
        }

        [HttpPost]
        public IActionResult AddUser([FromBody]User user)
        {
            if (user == null)
                throw new ArgumentNullException(nameof(user));

            var createdUser = _gatewayResource.AddUser(user);

            return Created(Url.RouteUrl(UserDetailsRoute, new { createdUser.Id }), createdUser);
        }

        [HttpGet("{id}/documents")]
        public IActionResult GetUserDocuments(long id, int skip = 0, int take = Collections.MaxTake)
        {
            if (id <= 0)
                throw new ArgumentOutOfRangeException(nameof(id));
            if (skip < 0)
                throw new ArgumentOutOfRangeException(nameof(skip));
            if (take <= 0)
                throw new ArgumentOutOfRangeException(nameof(take));
            if (take > Collections.MaxTake)
                throw new ArgumentOutOfRangeException(nameof(take));

            return Ok(_gatewayResource.GetUserDocuments(id, skip, take));
        }

        [HttpGet("{id}/documents/{documentId}")]
        public IActionResult GetUserDocument(long id, int documentId)
        {
            if (id <= 0)
                throw new ArgumentOutOfRangeException(nameof(id));
            if (documentId <= 0)
                throw new ArgumentOutOfRangeException(nameof(documentId));

            var user = _gatewayResource.GetUserDocument(id, documentId);
            if (user == null)
                return NotFound();

            return Ok(user);
        }
    }
}
