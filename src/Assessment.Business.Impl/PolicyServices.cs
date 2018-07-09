using Assessment.Business.Contracts;
using Assessment.Domain.Contracts.Models;
using Assessment.Infrastructure.Contracts;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;

namespace Assessment.Business.Impl
{
    public class PolicyServices : IPolicyServices
    {
        private readonly IHttpProxy _httpProxy;

        public PolicyServices(IHttpProxy httpProxy)
        {
            _httpProxy = httpProxy;
        }

        public async Task<IEnumerable<Policy>> GetPoliciesLinkedToCustomerByNameAsync(CustomerRequest request, CancellationToken cancellationToken)
        {
            var client = await _httpProxy.GetCustomersAsync(cancellationToken);
            var policies = await _httpProxy.GetPoliciesAsync(cancellationToken);

            var customerSelect = client.Clients.FirstOrDefault(customer => customer.Name == request.Name);

            if (customerSelect != null)
            {
                return policies.Policies.Where(policy => policy.ClientId == customerSelect.Id).ToList();
            }

            return null;
        }
    }
}
