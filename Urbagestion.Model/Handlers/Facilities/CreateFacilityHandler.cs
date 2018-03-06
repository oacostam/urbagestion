using System;
using System.Security.Principal;
using Paramore.Brighter;
using Paramore.Brighter.Logging.Attributes;
using Urbagestion.Model.Commands.Facilities;
using Urbagestion.Model.Extensions;
using Urbagestion.Model.Interfaces;

namespace Urbagestion.Model.Handlers.Facilities
{
    public class CreateFacilityHandler : RequestHandler<CreateFacilityCommand>, IDisposable
    {
        private readonly IPrincipal principal;
        private readonly IUnitOfWork unitOfWork;

        public CreateFacilityHandler(IUnitOfWork unitOfWork, IPrincipal principal)
        {
            if (unitOfWork != null) this.unitOfWork = unitOfWork;
            if (principal != null) this.principal = principal;
        }

        public void Dispose()
        {
            unitOfWork?.Dispose();
            GC.SuppressFinalize(this);
        }

        [CheckUniqueFacilityName(int.MaxValue, HandlerTiming.After)]
        public override CreateFacilityCommand Handle(CreateFacilityCommand command)
        {
            principal.SetAuditFields(command.Facility);
            unitOfWork.Add(command.Facility);
            return base.Handle(command);
        }
    }
}