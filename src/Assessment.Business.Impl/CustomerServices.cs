using Assessment.Business.Contracts;
using Assessment.Domain.Contracts.Models;
using Assessment.Infrastructure.Contracts;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;

namespace Assessment.Business.Impl
{
    public class CustomerServices : ICustomerServices
    {
        private readonly IHttpProxy _httpProxy;

        public CustomerServices(IHttpProxy httpProxy)
        {
            _httpProxy = httpProxy;
        }

        public async Task<Customer> GetCustomerByIdAsync(CustomerRequest request, CancellationToken cancellationToken)
        {
            var client = await _httpProxy.GetCustomersAsync(cancellationToken);

            return client.Clients.FirstOrDefault(customer => customer.Id == request.Id);
        }

        public async Task<Customer> GetCustomerByNameAsync(CustomerRequest request, CancellationToken cancellationToken)
        {
            var client = await _httpProxy.GetCustomersAsync(cancellationToken);

            return client.Clients.FirstOrDefault(customer => customer.Name == request.Name);
        }

        public async Task<Customer> GetCustomerLinkedToPolicyByNumberAsync(PolicyRequest request, CancellationToken cancellationToken)
        {
            var client = await _httpProxy.GetCustomersAsync(cancellationToken);
            var policies = await _httpProxy.GetPoliciesAsync(cancellationToken);

            var policySelect = policies.Policies.FirstOrDefault(policy => policy.Id == request.Id);

            if (policySelect != null)
            {
                return client.Clients.FirstOrDefault(customer => customer.Id == policySelect.ClientId);
            }

            return null;
        }
    }
}
