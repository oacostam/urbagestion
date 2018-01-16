using System.ComponentModel.DataAnnotations;

namespace Urbagestion.UI.Web.Models.AccountViewModels
{
    public class RegisterViewModel
    {
        [Required(ErrorMessage = "El {0} es obligatorio")]
        [StringLength(100, ErrorMessage = "El {0} debe tener al menos {2} y un máximo de {1} caractéres.", MinimumLength = 6)]
        [DataType(DataType.Text)]
        [Display(Name = "Nombre")]
        public string Name { get; set; }

        [Required(ErrorMessage = "El {0} es obligatorio")]
        [StringLength(100, ErrorMessage = "El {0} debe tener al menos {2} y un máximo de {1} caractéres.", MinimumLength = 6)]
        [DataType(DataType.Text)]
        [Display(Name = "Primer Apellido")]
        public string FirstLastName { get; set; }

        [Required(ErrorMessage = "El {0} es obligatorio")]
        [StringLength(100, ErrorMessage = "El {0} debe tener al menos {2} y un máximo de {1} caractéres.", MinimumLength = 6)]
        [DataType(DataType.Text)]
        [Display(Name = "Segundo Apellido")]
        public string SecondLastName { get; set; }

        [Required(ErrorMessage = "El {0} es obligatorio")]
        [EmailAddress]
        [Display(Name = "Email")]
        public string Email { get; set; }

        [Required(ErrorMessage = "La {0} es obligatorio")]
        [StringLength(100, ErrorMessage = "La {0} debe tener al menos {2} y un máximo de {1} caractéres.", MinimumLength = 6)]
        [DataType(DataType.Password)]
        [Display(Name = "Contraseña")]
        public string Password { get; set; }

        [DataType(DataType.Password)]
        [Display(Name = "Confirmar contraseña")]
        [Compare("Password", ErrorMessage = "La contraseña y la confirmación no coinciden.")]
        public string ConfirmPassword { get; set; }

        [Required(ErrorMessage = "La {0} es obligatorio")]
        [StringLength(100, ErrorMessage = "La {0} debe tener al menos {2} y un máximo de {1} caractéres.", MinimumLength = 3)]
        [DataType(DataType.Text)]
        [Display(Name = "Calle")]
        public string Street { get; set; }

        
        [Display(Name = "Número")]
        [Range(0, int.MaxValue, ErrorMessage = "Por favor, introduzca un número válido.")]
        public int? StreetNumber{ get; set; }
        
        [StringLength(100, ErrorMessage = "El {0} debe tener al menos {2} y un máximo de {1} caractéres.", MinimumLength = 3)]
        [DataType(DataType.Text)]
        [Display(Name = "Apartamento")]
        public string Apartment { get; set; }

        [Range(-10, 100, ErrorMessage = "Por favor, introduzca un número válido.")]
        [Display(Name = "Piso")]
        public int? Floor { get; set; }
    }
}