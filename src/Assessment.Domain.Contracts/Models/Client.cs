using System.Collections.Generic;

namespace Assessment.Domain.Contracts.Models
{
    public class Client
    {
        public IEnumerable<Customer> Clients { get; set; }
    }
}
