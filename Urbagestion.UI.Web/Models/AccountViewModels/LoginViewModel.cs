using System.ComponentModel.DataAnnotations;

namespace Urbagestion.UI.Web.Models.AccountViewModels
{
    public class LoginViewModel
    {
        [Required] [EmailAddress] public string Email { get; set; }

        [Required]
        [DataType(DataType.Password)]
        public string Password { get; set; }

        [Display(Name = "Recordarme?")] public bool RememberMe { get; set; }
    }
}