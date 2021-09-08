using Discount.API.Entities;
using Discount.API.Repositories;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Threading.Tasks;

namespace Discount.API.Controllers
{
    [Route("/api/v1/[controller]")]
    [ApiController]
    public class DiscountController : ControllerBase
    {
        private readonly IDiscountRepository discountRepository;

        public DiscountController(IDiscountRepository discountRepository)
        {
            this.discountRepository = discountRepository ?? throw new ArgumentNullException(nameof(discountRepository));
        }

        [ProducesResponseType(typeof(Coupon),(int)HttpStatusCode.OK)]
        [HttpGet("{productName}",Name = "GetDiscount")]
        public async Task<ActionResult<Coupon>> GetDiscount(string productName)
        {
            return Ok(await discountRepository.GetDiscount(productName));
        }

        [ProducesResponseType(typeof(Coupon), (int)HttpStatusCode.OK)]
        [HttpPost]
        public async Task<ActionResult<Coupon>> CreateDiscount([FromBody] Coupon coupon)
        {
            await discountRepository.CreateDiscount(coupon);

            return CreatedAtRoute("GetDiscount", new { productName = coupon.ProductName }, coupon);
        }

        [ProducesResponseType(typeof(Coupon), (int)HttpStatusCode.OK)]
        [HttpPut]
        public async Task<ActionResult<Coupon>> UpdateDiscount([FromBody] Coupon coupon)
        {
            return Ok(await discountRepository.UpdateDiscount(coupon));
        }

        [ProducesResponseType(typeof(Coupon), (int)HttpStatusCode.OK)]
        [HttpDelete]
        public async Task<ActionResult<bool>> DeleteDiscount(string productName)
        {
            return Ok(await discountRepository.DeleteDiscount(productName));
        }
    }
}
