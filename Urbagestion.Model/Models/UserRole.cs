using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.AspNetCore.Identity;

namespace Urbagestion.Model.Models
{
    [Table("UserRole")]
    public class UserRole : IdentityUserRole<int>
    {
    }
}