using AWS.SQS.Publish.Events.Order;
using AWS.SQS.Publish.Model;
using AWS.SQS.Publish.SQS;
using MediatR;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using System.Linq;
using System.Threading.Tasks;

namespace AWS.SQS.Publish.Controllers
{
    public class OrderController : BaseController
    {
        
        private readonly ILogger<OrderController> _logger;
        private readonly IMediator _bus;
        private readonly IMessageBus _messageBus;
        

        /// <summary>
        /// 
        /// </summary>
        /// <param name="logger"></param>
        public OrderController(ILogger<OrderController> logger, IMediator bus, IMessageBus messageBus)
        {
            _logger = logger;
            _bus = bus;
            _messageBus = messageBus;
        }


        /// <summary>
        /// Read all messages
        /// </summary>
        /// <remarks>
        [HttpGet("Messages")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public async Task<ActionResult> GetMessagesAsync()
        {
            var result = await _messageBus.ReceiveMessagesAsync();

            if (result.Any())
            {
               return Ok(result);
            }
            _logger.LogError(default(EventId), $"Found fails to {nameof(OrderController)} in GetMessagesAsync");
            return BadRequest("Error read all messages");

        }
        
        /// <summary>
        /// Criar pedido
        /// </summary>
        /// <remarks>
        /// Sample request:
        ///
        ///     POST /api/order/bonus
        ///     {
        ///        "name": "Victor Wilson",
        ///        "total": "R$ 12.696,20"
        ///       
        ///     }
        ///     
        /// </remarks>        
        /// <param name="orderRequest"></param>  
        [HttpPost("Bonus")]
        [ProducesResponseType(StatusCodes.Status201Created)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public async Task<ActionResult> CreateOrderAsync([FromBody] OrderRequest orderRequest)
        {
            if (!orderRequest.Notifications.Any())
            { await _bus.Publish(new CreatedOrderEvent(orderRequest.Name,orderRequest.Total));
                return Created(string.Empty, "");
            }
            _logger.LogError(default(EventId), $"Found fails to {nameof(OrderController)} in CreateOrderAsync {orderRequest.Notifications}");
           return BadRequest(orderRequest.Notifications);

        }


    }
}
