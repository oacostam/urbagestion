using System;
using System.ComponentModel.DataAnnotations;
using Microsoft.AspNetCore.Mvc;

namespace Urbagestion.UI.Web.Models
{
    [Serializable]
    public abstract class ModelBase
    {
        [HiddenInput(DisplayValue = false)]
        [Required] 
        public int Id { get; set; }

        [Required]
        [Display(ResourceType = typeof(Resource), Name = "ModelBase_IsActiveLabel")] 
        public bool IsActive { get; set; } = true;
        
        [DataType(DataType.DateTime)]
        [Display(Name = "Fecha de creación")]
        public DateTime? CreationdDate { get; set; } = DateTime.Now;
        
        [DataType(DataType.Text)]
        [Display(Name = "Creado por")]
        public string CreatedBy {get; set;}
        
        [DataType(DataType.DateTime)]
        [Display(Name = "Fecha de actualizacion")]
        public DateTime? UpdatedDate {get; set;} = DateTime.Now;
        
        [DataType(DataType.Text)]
        [Display(Name = "Actualizado por")]
        public string UpdatedBy {get; set;}
        
    }
}