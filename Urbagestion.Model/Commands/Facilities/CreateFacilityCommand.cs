using System;
using Paramore.Brighter;
using Urbagestion.Model.Models;

namespace Urbagestion.Model.Commands.Facilities
{
    public class CreateFacilityCommand : IRequest
    {
        private Facility facility;

        public CreateFacilityCommand(Facility facility)
        {
            this.facility = facility;
            Id = Guid.NewGuid();
        }

        public Guid Id { get; set; }

        public Facility Facility
        {
            get => facility;
            set => facility = value ?? throw new ArgumentNullException(nameof(Facility));
        }
    }
}