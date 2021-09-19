using AutoMapper;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Threading;
using System.Threading.Tasks;
using TaxCalc.Api.Dtos;
using TaxCalc.Business;
using TaxCalc.Domain;
using TaxCalc.Domain.Models;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace TaxCalc.Api.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class TaxCalculatorsController : ControllerBase
    {
        private readonly IMapper mapper;
        private readonly ITaxCalcBusiness taxCalcBusiness;

        public TaxCalculatorsController(IMapper mapper, ITaxCalcBusiness taxCalcBusiness)
        {
            this.mapper = mapper;
            this.taxCalcBusiness = taxCalcBusiness;
        }

        [HttpGet]
        public async Task<IActionResult> Get(string zip, string street, string city, string state, string country, CancellationToken cancellationToken)
        {
            if (string.IsNullOrEmpty(zip))
            {
                return BadRequest("The parameter zip is required");
            }

            try
            {
                var result = await taxCalcBusiness.GetTaskRateForLocationAsync(zip, new OptionalAddress
                {
                    Street = street,
                    City = city,
                    State = state,
                    Country = country
                }, cancellationToken);

                return Ok(result);
            }
            catch (TaxCalcProviderException ex)
            {
                return BadRequest(ex.Detail);
            }
            catch (ArgumentException ex)
            {
                return BadRequest(ex.Message);
            }
            catch (Exception)
            {
                return StatusCode(StatusCodes.Status500InternalServerError);
            }
        }

        [HttpPost]
        public async Task<IActionResult> Post([FromBody] OrderDto orderDto, CancellationToken cancellationToken)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState.Values);
            }

            var order = mapper.Map<Order>(orderDto);

            try
            {
                var result = await taxCalcBusiness.CalculateTaxForAnOrderAsync(order, cancellationToken);

                return Ok(result);
            }
            catch (TaxCalcProviderException ex)
            {
                return BadRequest(ex.Detail);
            }
            catch (ArgumentException ex)
            {
                return BadRequest(ex.Message);
            }
            catch (Exception)
            {
                return StatusCode(StatusCodes.Status500InternalServerError);
            }
        }
    }
}
