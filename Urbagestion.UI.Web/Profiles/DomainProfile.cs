using AutoMapper;
using Urbagestion.Model.Models;
using Urbagestion.UI.Web.Models.FacilityViewModels;

namespace Urbagestion.UI.Web.Profiles
{
    public class DomainProfile : Profile
    {
        public DomainProfile()
        {
            CreateMap<FacilityIndexViewModel, Facility>();
            CreateMap<Facility, FacilityIndexViewModel>();
        }
    }
}