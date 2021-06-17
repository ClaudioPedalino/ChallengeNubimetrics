using ChallengeNubimetrics.Application.Models.Common;
using ChallengeNubimetrics.Application.Queries.Countries.GetAll;
using ChallengeNubimetrics.Application.Queries.Countries.GetById;
using ChallengeNubimetrics.Application.Queries.Search;
using MediatR;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace ChallengeNubimetrics.Api.Controllers
{
    [Authorize(AuthenticationSchemes = JwtBearerDefaults.AuthenticationScheme)]
    [Route("api/[controller]")]
    [ApiController]
    public class MyRestfulAppController : ControllerBase
    {
        private readonly IMediator _mediator;
        private readonly IConfiguration _config;

        public MyRestfulAppController(IMediator mediator, IConfiguration config)
        {
            _mediator = mediator;
            _config = config;
        }


        [HttpGet("paises")]
        [ProducesResponseType(typeof(GetAllCountryResponse), 200)]
        [ProducesResponseType(typeof(BadRequestResult), 400)]
        [ProducesResponseType(typeof(UnauthorizedObjectResult), 401)]
        [ProducesResponseType(typeof(Result), 500)]
        [Authorize]
        public async Task<IActionResult> GetAll()
            => Ok(await _mediator.Send(new GetAllCountryQuery()));


        [HttpGet("paises/{paisId}")]
        [ProducesResponseType(typeof(GetCountryByIdResponse), 200)]
        [ProducesResponseType(typeof(BadRequestResult), 400)]
        [ProducesResponseType(typeof(UnauthorizedObjectResult), 401)]
        [ProducesResponseType(typeof(Result), 500)]
        [Authorize]
        public async Task<IActionResult> Get([FromRoute] string paisId)
        {
            paisId = paisId.ToUpper();

            var specialCases = new List<string>();
            _config.GetSection("Consideraciones").Bind(specialCases);

            return specialCases.Any(x => x == paisId)
                ? Unauthorized("Desafío 1: Consideraciones => Error 401 unauthorized de http para casos \"BR\" y \"CO\"")
                : Ok(await _mediator.Send(new GetCountryByIdQuery(paisId)));
        }


        [HttpGet("busqueda/{termino}")]
        [ProducesResponseType(typeof(GetCountryByIdResponse), 200)]
        [ProducesResponseType(typeof(BadRequestResult), 400)]
        [ProducesResponseType(typeof(UnauthorizedObjectResult), 401)]
        [ProducesResponseType(typeof(Result), 500)]
        [Authorize]
        public async Task<IActionResult> GetBusqueda([FromRoute] string termino)
        {
            return Ok(await _mediator.Send(new GetBySearchQuery(termino)));
        }
    }
}
