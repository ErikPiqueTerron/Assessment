using System;

namespace Assessment.Domain.Contracts.Models
{
    public class CustomerRequest
    {
        public Guid Id { get; set; }

        public string Name { get; set; }
    }
}
