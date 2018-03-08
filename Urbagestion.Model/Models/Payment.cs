using System;
using Urbagestion.Model.Common;

namespace Urbagestion.Model.Models
{
    public class Payment : Entity
    {
        protected Payment()
        {
        }
        
        public Payment(int id, decimal amount, Facility facility)
        {
            if(amount <= 0)
                throw new BussinesException("No se permiten pagos con cantidad 0 o negativos.");
            Id = id;
            Amount = amount;
            Facility = facility ?? throw new ArgumentNullException(nameof(facility));
        }

        public decimal Amount { get; }

        public Facility Facility { get; }
        
    }
}