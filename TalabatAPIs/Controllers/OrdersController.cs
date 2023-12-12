using AutoMapper;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System.Security.Claims;
using Talabat.Core.Entities.OrderAggregate;
using Talabat.Core.Service;
using Talabat.Service;
using TalabatAPIs.DTOs;
using TalabatAPIs.Errors;

namespace TalabatAPIs.Controllers
{

    public class OrdersController : APIBaceController
    {

        private readonly IOrderService _orderService;
        private readonly IMapper _mapper;

        public OrdersController(IOrderService orderService, IMapper mapper)
        {
            _orderService = orderService;
            _mapper = mapper;
        }

        //create Order
        [ProducesResponseType(typeof(Order), 200)]
        [ProducesResponseType(typeof(ApiResponse), StatusCodes.Status400BadRequest)]
        [HttpPost]
        [Authorize]
        public async Task<ActionResult<Order>> CreateOrder(OrderDto orderDto)
        {
            var buyerEmail = User.FindFirstValue(ClaimTypes.Email);
            var address = _mapper.Map<AddressDto, Address>(orderDto.ShippingAddress);

            var order = await _orderService.CreateOrderAsync(buyerEmail, orderDto.BasketId, orderDto.DeliveryMethodId, address);

            if(order is null)
            return BadRequest(new ApiResponse(400, "There is a Problem with your Order"));

            return Ok(order);
        }




        [HttpGet]  //Get ://api/orders
        [Authorize]
        [ProducesResponseType(typeof(IReadOnlyList<OrderToReturnDto>), 200)]
        [ProducesResponseType(typeof(ApiResponse), StatusCodes.Status404NotFound)]

        public async Task<ActionResult<IReadOnlyList<OrderToReturnDto>>> GetOrdersForUser()
        {
            var buyerEmail = User.FindFirstValue(ClaimTypes.Email);
            var orders = await _orderService.GetOrdersForSpecUserAsync(buyerEmail);
            if (orders is null) return NotFound(new ApiResponse(404,"there is no orders for this user"));

            var OrdersDto = _mapper.Map<IReadOnlyList<Order>, IReadOnlyList<OrderToReturnDto>>(orders);

            return Ok(OrdersDto);

        }



        [HttpGet("{id}")] //Get ://api/orders/id
        [Authorize]
        [ProducesResponseType(typeof(OrderToReturnDto), 200)]
        [ProducesResponseType(typeof(ApiResponse), StatusCodes.Status404NotFound)]
        public async Task<ActionResult<Order>> GetOrderForUser(int id)
        {

            var email = User.FindFirstValue(ClaimTypes.Email);

            var order = await _orderService.GetOrderByIdForSpecUserAsync(email, id);
            if (order is null) return NotFound(new ApiResponse(404,$"there is no order with this Id ={id} . for this User"));

            var orderDto= _mapper.Map<Order,OrderToReturnDto>(order);

            return Ok(orderDto);

        }

        [HttpGet("DeliveryMethods")]  //Get "/api/orders/deliverymethods
        [ProducesResponseType(typeof(DeliveryMethod), 200)]
        [ProducesResponseType(typeof(ApiResponse), StatusCodes.Status404NotFound)]
        public async Task<ActionResult<IReadOnlyList<DeliveryMethod>>> GetDeliveyMethods()
        {
            var deliveryMethods = await _orderService.GetDeliveryMethodAsync();
            if (deliveryMethods is null) return NotFound(new ApiResponse(404, "you not have any Delivery Method"));

            return Ok(deliveryMethods);

        }






    }
}
