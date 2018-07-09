using Assessment.Domain.Contracts.Models;
using System.Threading;
using System.Threading.Tasks;

namespace Assessment.Business.Contracts
{
    public interface ICustomerServices
    {
        Task<Customer> GetCustomerByIdAsync(CustomerRequest request, CancellationToken cancellationToken);

        Task<Customer> GetCustomerByNameAsync(CustomerRequest request, CancellationToken cancellationToken);

        Task<Customer> GetCustomerLinkedToPolicyByNumberAsync(PolicyRequest request, CancellationToken cancellationToken);
    }
}
