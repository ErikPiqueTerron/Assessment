using System.Collections.Generic;

namespace Assessment.Domain.Contracts.Models
{
    public class PolicyList
    {
        public IEnumerable<Policy> Policies { get; set; }
    }
}
