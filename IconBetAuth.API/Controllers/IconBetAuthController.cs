using IconBetAuth.Domain.DTO;
using IconBetAuth.Services;
using MediatR;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace IconBetAuth.API.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class IconBetAuthController : ControllerBase
    {
        private readonly IMediator _mediator;
        private readonly ILogger<IconBetAuthController> _logger;

        public IconBetAuthController(IMediator mediator, ILogger<IconBetAuthController> logger)
        {
            _mediator = mediator;
            _logger = logger;
        }
        [HttpGet]
        public string Get()
        {
            _logger.LogInformation($"Request Get");
            return "Icon Bet Auth API";
        }
        [HttpPost("GetInfo/{client}")]
        public async Task<IActionResult> GetInfoUser([FromBody] UserDTO userDTO, string client)
        {
            _logger.LogInformation($"Request Get GetUser {client} ");
            var userInfo = await _mediator.Send(new UserGetInfoServiceClient.Query(userDTO));
            return Ok(userInfo);
        }
        [HttpPost("GetBalance/{client}")]
        public async Task<IActionResult> GetBalanceUser([FromBody] UserDTO userDTO, string client)
        {
            _logger.LogInformation($"Request Get GetUser {client}");
            var userBalance = await _mediator.Send(new UserGetBalanceServiceClient.Query(userDTO));
            return Ok(userBalance);
        }
        [HttpPost("Transaction/{client}")]
        public async Task<IActionResult> TransactionUser([FromBody] TransactionDTO transactionDTO, string client)
        {
            _logger.LogInformation($"Request Get GetUser {client}");
            var ticket = await _mediator.Send(new UserTransactionServiceClient.Query(transactionDTO));
            return Ok(ticket);
        }
    }
}