using AutoMapper;
using Urbagestion.Model.Common;
using Urbagestion.Model.Models;
using Urbagestion.UI.Web.Models.FacilityViewModels;

namespace Urbagestion.UI.Web.Automapper
{
    public class WebUiMappingProfile : Profile
    {
        public WebUiMappingProfile()
        {
            CreateMap<FacilityIndexViewModel, Facility>().ReverseMap();
            CreateMap<Entity, Entity>()
                .ForMember(des => des.CreatedBy, opts => opts.Ignore())
                .ForMember(des => des.CreationdDate, opts => opts.Ignore())
                .ForMember(des => des.UpdatedBy, opts => opts.Ignore())
                .ForMember(des => des.UpdatedDate, opts => opts.Ignore());
        }
    }
}