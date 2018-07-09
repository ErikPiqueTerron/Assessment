using Assessment.Domain.Contracts.Models;
using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;

namespace Assessment.Business.Contracts
{
    public interface IPolicyServices
    {
        Task<IEnumerable<Policy>> GetPoliciesLinkedToCustomerByNameAsync(CustomerRequest request, CancellationToken cancellationToken);
    }
}
