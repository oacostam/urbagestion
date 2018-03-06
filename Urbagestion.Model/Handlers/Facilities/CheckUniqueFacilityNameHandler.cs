using System;
using System.Linq;
using Paramore.Brighter;
using Urbagestion.Model.Commands.Facilities;
using Urbagestion.Model.Common;
using Urbagestion.Model.Interfaces;
using Urbagestion.Model.Models;

namespace Urbagestion.Model.Handlers.Facilities
{
    public class CheckUniqueFacilityNameHandler : RequestHandler<CreateFacilityCommand>, IDisposable
    {
        private readonly IUnitOfWork unitOfWork;
        private bool disposed;

        public CheckUniqueFacilityNameHandler(IUnitOfWork unitOfWork)
        {
            this.unitOfWork = unitOfWork ?? throw new ArgumentNullException(nameof(unitOfWork));
        }

        public override CreateFacilityCommand Handle(CreateFacilityCommand command)
        {
            var otherWithSameName = unitOfWork.GetEntitySet<Facility>().FirstOrDefault(f => f.Name == command.Facility.Name);
            if (otherWithSameName != null)
                throw new BussinesException("Ya existe una instalación con ese nombre, por favor, elija un nombre único.");
            return base.Handle(command);
        }

        public void Dispose()
        {
            Dispose(true);
            GC.SuppressFinalize(this);
        }

        protected virtual void Dispose(bool isDisposing)
        {
            if (!disposed)
            {
                if (isDisposing)
                {
                    //cleanup managed resources
                    unitOfWork?.Dispose();
                }

                //cleanup unmanaged resources
                disposed = true;
            }
        }
    }
}