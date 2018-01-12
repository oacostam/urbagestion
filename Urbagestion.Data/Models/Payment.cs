using Urbagestion.Model.Common;

namespace Urbagestion.Model.Models
{
    public class Payment : Entity
    {
        public decimal Amount { get; set; } = 0;

        public User User { get; set; }
        
    }
}