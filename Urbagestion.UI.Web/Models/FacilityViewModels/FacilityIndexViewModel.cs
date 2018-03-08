using System;
using System.ComponentModel.DataAnnotations;

namespace Urbagestion.UI.Web.Models.FacilityViewModels
{
    [Serializable]
    public class FacilityIndexViewModel : ModelBase
    {
        [Required(ErrorMessage = "Debe especificar un {0}.")]
        [MinLength(5, ErrorMessageResourceName = "Facility_NameMinLen", ErrorMessageResourceType = typeof(Resource))]
        [Display(Name = "Nombre")]
        public string Name { get; set; }

        [Display(Name = "Precio")]
        [DataType(DataType.Currency, ErrorMessage = "Por favor, especifique un {0} válido.")]
        public decimal? Price { get; set; } = 0;

        [Display(Name = "Apertura")]
        [Required(ErrorMessage = "Debe especificar una hora de {0}.")]
        [DisplayFormat(DataFormatString = "{0:hh\\:mm}", ApplyFormatInEditMode = true)]
        [Range(typeof(TimeSpan), "00:00", "23:59", ErrorMessage =
            "Debe introducir un valor entre las 00:00 y las 23:59.")]
        public TimeSpan OpensAt { get; set; } = new TimeSpan(9, 0, 0);

        [Display(Name = "Cierre")]
        [Required(ErrorMessage = "Debe especificar una hora de {0}.")]
        [DisplayFormat(DataFormatString = "{0:hh\\:mm}", ApplyFormatInEditMode = true)]
        [Range(typeof(TimeSpan), "00:00", "23:59", ErrorMessage =
            "Debe introducir un valor entre las 00:00 y las 23:59.")]
        public TimeSpan CloseAt { get; set; } = new TimeSpan(22, 0, 0);
    }
}