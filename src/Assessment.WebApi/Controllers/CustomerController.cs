using Assessment.Business.Contracts;
using Assessment.Domain.Contracts.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Net;
using System.Threading;
using System.Threading.Tasks;

namespace Assessment.WebApi.Controllers
{
    [Produces("application/json")]
    [Route("v1/[controller]")]
    [Authorize]
    public class CustomerController : Controller
    {
        private readonly ICustomerServices _customerServices;

        public CustomerController(ICustomerServices customerServices)
        {
            _customerServices = customerServices;
        }

        /// <summary>
        /// Retrieves a specific customer by name
        /// </summary>
        /// <remarks>Applies to all roles</remarks>
        /// <returns>Customer</returns>
        /// <response code="200">Found customer</response>
        /// <response code="204">Empty</response>
        /// <response code="400">Customer has missing/invalid values</response>
        /// <response code="401">Unauthorized</response>
        /// <response code="403">Forbidden</response>
        [HttpGet("{name}")]
        [Authorize(Policy = "AllRoles")]
        [ProducesResponseType(typeof(Customer), (int)HttpStatusCode.OK)]
        [ProducesResponseType(typeof(string), (int)HttpStatusCode.BadRequest)]
        [ProducesResponseType(typeof(void), (int)HttpStatusCode.Forbidden)]
        [ProducesResponseType(typeof(void), (int)HttpStatusCode.Unauthorized)]
        [ProducesResponseType(typeof(void), (int)HttpStatusCode.NoContent)]
        public async Task<IActionResult> CustomerByNameAsync([FromRoute]string name, CancellationToken cancellationToken)
        {
            var request = new CustomerRequest
            {
                Name = name
            };

            var customer = await _customerServices.GetCustomerByNameAsync(request, cancellationToken);

            if (customer == null)
            {
                return NoContent();
            }

            return Ok(customer);
        }

        /// <summary>
        /// Retrieves a specific customer by id
        /// </summary>
        /// <remarks>Applies to all roles</remarks>
        /// <returns>Customer</returns>
        /// <response code="200">Found customer</response>
        /// <response code="204">Empty</response>
        /// <response code="400">Customer has missing/invalid values</response>
        /// <response code="401">Unauthorized</response>
        /// <response code="403">Forbidden</response>
        [HttpGet]
        [Authorize(Policy = "AllRoles")]
        [ProducesResponseType(typeof(Customer), (int)HttpStatusCode.OK)]
        [ProducesResponseType(typeof(string), (int)HttpStatusCode.BadRequest)]
        [ProducesResponseType(typeof(void), (int)HttpStatusCode.Forbidden)]
        [ProducesResponseType(typeof(void), (int)HttpStatusCode.Unauthorized)]
        [ProducesResponseType(typeof(void), (int)HttpStatusCode.NoContent)]
        public async Task<IActionResult> CustomerByIdAsync([FromQuery]Guid id, CancellationToken cancellationToken)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            var request = new CustomerRequest
            {
                Id = id
            };

            var customer = await _customerServices.GetCustomerByIdAsync(request, cancellationToken);

            if (customer == null)
            {
                return NoContent();
            }

            return Ok(customer);
        }

        /// <summary>
        /// Retrieves a specific customer linked to specific policy
        /// </summary>
        /// <remarks>Applies only to the administrator role</remarks>
        /// <returns>Customer</returns>
        /// <response code="200">Found customer</response>
        /// <response code="204">Empty</response>
        /// <response code="400">Customer has missing/invalid values</response>
        /// <response code="401">Unauthorized</response>
        /// <response code="403">Forbidden</response>
        [HttpGet("linked/policy")]
        [Authorize(Policy = "AdminRole")]
        [ProducesResponseType(typeof(Customer), (int)HttpStatusCode.OK)]
        [ProducesResponseType(typeof(string), (int)HttpStatusCode.BadRequest)]
        [ProducesResponseType(typeof(void), (int)HttpStatusCode.Forbidden)]
        [ProducesResponseType(typeof(void), (int)HttpStatusCode.Unauthorized)]
        [ProducesResponseType(typeof(void), (int)HttpStatusCode.NoContent)]
        public async Task<IActionResult> CustomerLinkedToPolicieAsync([FromQuery]Guid id, CancellationToken cancellationToken)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            var request = new PolicyRequest
            {
                Id = id
            };

            var customer = await _customerServices.GetCustomerLinkedToPolicyByNumberAsync(request, cancellationToken);

            if (customer == null)
            {
                return NoContent();
            }

            return Ok(customer);
        }
    }
}