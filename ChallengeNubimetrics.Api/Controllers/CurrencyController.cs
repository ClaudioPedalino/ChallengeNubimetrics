using ChallengeNubimetrics.Application.Models.Common;
using ChallengeNubimetrics.Application.Queries.Currencies.GetAll;
using MediatR;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;


namespace ChallengeNubimetrics.Api.Controllers
{
    [Authorize(AuthenticationSchemes = JwtBearerDefaults.AuthenticationScheme)]
    [Route("api/[controller]")]
    [ApiController]
    public class CurrencyController : ControllerBase
    {
        private readonly IMediator _mediator;

        public CurrencyController(IMediator mediator)
        {
            _mediator = mediator;
        }


        [HttpGet]
        [ProducesResponseType(typeof(GetAllCurrencyResponse), 200)]
        [ProducesResponseType(typeof(BadRequestResult), 400)]
        [ProducesResponseType(typeof(UnauthorizedObjectResult), 401)]
        [ProducesResponseType(typeof(Result), 500)]
        [Authorize]
        public async Task<IActionResult> GetAll()
        {
            return Ok(await _mediator.Send(new GetAllCurrencyQuery()));
        }

    }
}
