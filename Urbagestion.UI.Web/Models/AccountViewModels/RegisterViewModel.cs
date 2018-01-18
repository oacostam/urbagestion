using System.ComponentModel.DataAnnotations;

namespace Urbagestion.UI.Web.Models.AccountViewModels
{
    public class RegisterViewModel
    {
        [Required(ErrorMessage = "El {0} es obligatorio")]
        [StringLength(100, ErrorMessage = "El {0} debe tener al menos {2} y un máximo de {1} caractéres.", MinimumLength = 1)]
        [DataType(DataType.Text)]
        [Display(Name = "Nombre")]
        public string Name { get; set; }

        [Required(ErrorMessage = "El {0} es obligatorio")]
        [StringLength(100, ErrorMessage = "El {0} debe tener al menos {2} y un máximo de {1} caractéres.", MinimumLength = 1)]
        [DataType(DataType.Text)]
        [Display(Name = "Primer Apellido")]
        public string FirstLastName { get; set; }

        [Required(ErrorMessage = "El {0} es obligatorio")]
        [EmailAddress(ErrorMessage = "Introduzca una dirección de correo válida.")]
        [Display(Name = "Email")]
        [StringLength(100, ErrorMessage = "La {0} debe tener al menos {2} y un máximo de {1} caractéres.", MinimumLength = 6)]
        public string Email { get; set; }

        [Required(ErrorMessage = "La {0} es obligatorio")]
        [StringLength(100, ErrorMessage = "La {0} debe tener al menos {2} y un máximo de {1} caractéres.", MinimumLength = 6)]
        [DataType(DataType.Password, ErrorMessage = "Contraseña inválida.")]
        [Display(Name = "Contraseña")]
        public string Password { get; set; }

        [DataType(DataType.Password)]
        [Display(Name = "Confirmar contraseña")]
        [Compare(nameof(Password), ErrorMessage = "La contraseña y la confirmación no coinciden.")]
        public string ConfirmPassword { get; set; }

        [Required(ErrorMessage = "La {0} es obligatoria")]
        [StringLength(100, ErrorMessage = "La {0} debe tener al menos {2} y un máximo de {1} caractéres.", MinimumLength = 3)]
        [DataType(DataType.Text)]
        [Display(Name = "Dirección")]
        public string Address { get; set; }

    }
}