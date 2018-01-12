using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.AspNetCore.Identity;

namespace Urbagestion.Model.Models
{
    [Table("UserLogin")]
    public class UserLogin : IdentityUserLogin<int>
    {
    }
}