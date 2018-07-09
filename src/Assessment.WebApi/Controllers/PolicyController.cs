using Assessment.Business.Contracts;
using Assessment.Domain.Contracts.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Threading;
using System.Threading.Tasks;

namespace Assessment.WebApi.Controllers
{
    [Produces("application/json")]
    [Route("v1/[controller]")]
    [Authorize]
    public class PolicyController : Controller
    {
        private readonly IPolicyServices _policyServices;

        public PolicyController(IPolicyServices policyServices)
        {
            _policyServices = policyServices;
        }

        /// <summary>
        /// Retrieves a list of policies by user name
        /// </summary>
        /// <remarks>Applies only to the administrator role</remarks>
        /// <returns>List of policy</returns>
        /// <response code="200">Found list of policies</response>
        /// <response code="204">Empty</response>
        /// <response code="400">Policy has missing/invalid values</response>
        /// <response code="401">Unauthorized</response>
        /// <response code="403">Forbidden</response>
        [HttpGet("{name}")]
        [Authorize(Policy = "AdminRole")]
        [ProducesResponseType(typeof(IEnumerable<Policy>), (int)HttpStatusCode.OK)]
        [ProducesResponseType(typeof(string), (int)HttpStatusCode.BadRequest)]
        [ProducesResponseType(typeof(void), (int)HttpStatusCode.Forbidden)]
        [ProducesResponseType(typeof(void), (int)HttpStatusCode.Unauthorized)]
        [ProducesResponseType(typeof(void), (int)HttpStatusCode.NoContent)]
        public async Task<IActionResult> PoliciesLinkedToCustomerByNameAsync([FromRoute]string name, CancellationToken cancellationToken)
        {
            var request = new CustomerRequest
            {
                Name = name
            };

            var policies = await _policyServices.GetPoliciesLinkedToCustomerByNameAsync(request, cancellationToken);

            if (policies == null || !policies.Any())
            {
                return NoContent();
            }

            return Ok(policies);
        }
    }
}