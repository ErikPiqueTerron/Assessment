using Assessment.Domain.Contracts.Models;
using System.Threading;
using System.Threading.Tasks;

namespace Assessment.Infrastructure.Contracts
{
    public interface IHttpProxy
    {
        Task<Client> GetCustomersAsync(CancellationToken cancellationToken);

        Task<PolicyList> GetPoliciesAsync(CancellationToken cancellationToken);
    }
}
