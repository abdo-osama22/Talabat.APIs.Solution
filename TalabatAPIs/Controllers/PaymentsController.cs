using AutoMapper;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Talabat.Core.Entities;
using Talabat.Core.Service;
using TalabatAPIs.DTOs;
using TalabatAPIs.Errors;

namespace TalabatAPIs.Controllers
{
    [Authorize]
    public class PaymentsController : APIBaceController
    {
        private readonly IPaymentService _paymentService;
        private readonly IMapper _mapper;

        public PaymentsController(IPaymentService paymentService, IMapper mapper)
        {
            _paymentService = paymentService;
            _mapper = mapper;
        }


        [HttpPost]
        [ProducesResponseType(typeof(CustomerBasketDto),200 )]
        [ProducesResponseType(typeof(ApiResponse),400 )]
        public async Task<ActionResult<CustomerBasketDto>> CreateOrUpdatePaymentIntent(string basketId)
        {
            var customerBasket = await _paymentService.CreateOrUpdatePaymentIntent(basketId);
            if (customerBasket is null) return BadRequest(new ApiResponse(400, "There is a Problem with Your Basket"));


            var cusBasketDto = _mapper.Map<CustomerBasket, CustomerBasketDto>(customerBasket);

            return Ok(cusBasketDto);
        }


    }
}
