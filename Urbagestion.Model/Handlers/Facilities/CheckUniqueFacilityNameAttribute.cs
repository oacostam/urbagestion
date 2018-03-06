using System;
using Paramore.Brighter;

namespace Urbagestion.Model.Handlers.Facilities
{
    public class CheckUniqueFacilityNameAttribute : RequestHandlerAttribute
    {
        public CheckUniqueFacilityNameAttribute(int step, HandlerTiming timing = HandlerTiming.Before) : base(step, timing)
        {
        }

        public override object[] InitializerParams()
        {
            return new object[] { Timing };
        }

        public override Type GetHandlerType()
        {
            return typeof(CheckUniqueFacilityNameHandler);
        }
    }
}