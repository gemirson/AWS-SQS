using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using AWS.SQS.Publish.Events.Order;
using AWS.SQS.Publish.Model;
using MediatR;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;

namespace AWS.SQS.Publish.Controllers
{
    [ApiController]
    [Route("[api/controller]")]
    public class OrderController : ControllerBase
    {
        
        private readonly ILogger<OrderController> _logger;
        private readonly IMediator _bus;

        /// <summary>
        /// 
        /// </summary>
        /// <param name="logger"></param>
        public OrderController(ILogger<OrderController> logger, IMediator bus)
        {
            _logger = logger;
            _bus = bus;
        }

        /// <summary>
        /// Criar pedido
        /// </summary>
        /// <remarks>
        /// Sample request:
        ///
        ///     POST /api/order/create
        ///     {
        ///        "matricula": "0009968",
        ///        "nome": "Victor Wilson",
        ///        "area": "Diretoria",
        ///        "cargo": "Diretor Financeiro",
        ///        "salario_bruto": "R$ 12.696,20",
        ///        "data_de_admissao": "2012-01-05"
        ///     }
        ///     
        /// </remarks>        
        /// <param name="orderRequest"></param>  
        [HttpPost("Create")]
        [ProducesResponseType(StatusCodes.Status201Created)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public async Task<ActionResult> CreatOrderAsync([FromBody] OrderRequest orderRequest)
        {
            if (!orderRequest.Notifications.Any())
            {
              var result =  await _bus.Send(new CreatedOrderEvent(orderRequest.Id,orderRequest.Total));
                return Created(string.Empty, result);
            }
            _logger.LogError(default(EventId), $"Found fails to {nameof(OrderController)} in BonusCreate {orderRequest.Notifications}");
           return BadRequest(orderRequest.Notifications);

        }


    }
}
