using Ambev.DeveloperEvaluation.Application.Orders.CreateOrder;
using Ambev.DeveloperEvaluation.Application.Orders.GetOrder;
using Ambev.DeveloperEvaluation.WebApi.Common;
using Ambev.DeveloperEvaluation.WebApi.Features.Orders.CreateOrder;
using Ambev.DeveloperEvaluation.WebApi.Features.Orders.GetAllOrders;
using AutoMapper;
using MediatR;
using Microsoft.AspNetCore.Mvc;

namespace Ambev.DeveloperEvaluation.WebApi.Features.Orders
{
    [ApiController]
    [Route("api/[controller]")]
    public class OrdersController : BaseController
    {
        private readonly IMediator _mediator;
        private readonly IMapper _mapper;

        public OrdersController(IMediator mediator, IMapper mapper)
        {
            _mediator = mediator;
            _mapper = mapper;
        }

        /// <summary>
        /// Creates a new Orders
        /// </summary>
        /// <param name="request">The order creation request</param>
        /// <param name="cancellationToken">Cancellation token</param>
        /// <returns>The created Order details</returns>
        [HttpPost]
        [ProducesResponseType(typeof(ApiResponseWithData<CreateOrderResponse>), StatusCodes.Status201Created)]
        [ProducesResponseType(typeof(ApiResponse), StatusCodes.Status400BadRequest)]
        public async Task<IActionResult> CreateOrder([FromBody] CreateOrderRequest request, CancellationToken cancellationToken)
        {
            var validator = new CreateOrderRequestValidator();
            var validationResult = await validator.ValidateAsync(request, cancellationToken);

            if (!validationResult.IsValid)
                return BadRequest(validationResult.Errors);

            var command = _mapper.Map<CreateOrderCommand>(request);
            var response = await _mediator.Send(command, cancellationToken);

            return Created(string.Empty, new ApiResponseWithData<CreateOrderResponse>
            {
                Success = true,
                Message = "Order created successfully",
                Data = _mapper.Map<CreateOrderResponse>(response)
            });
        }

        /// <summary>
        /// Retrieves all orders
        /// </summary>
        /// <param name="cancellationToken">Cancellation token</param>
        /// <returns>Retrieves all orders if found</returns>
        [HttpGet()]
        [ProducesResponseType(typeof(ApiResponseWithData<List<GetOrderListResponse>>), StatusCodes.Status200OK)]
        [ProducesResponseType(typeof(ApiResponse), StatusCodes.Status400BadRequest)]
        [ProducesResponseType(typeof(ApiResponse), StatusCodes.Status404NotFound)]
        public async Task<IActionResult> GetAllOrders(CancellationToken cancellationToken)
        {
            var query = new GetOrderListQuery();
            var response = await _mediator.Send(query, cancellationToken);

            return Ok(new ApiResponseWithData<List<GetOrderListResponse>>
            {
                Success = true,
                Message = "Orders retrieved successfully",
                Data = _mapper.Map<List<GetOrderListResponse>>(response)
            });
        }
    }
}
