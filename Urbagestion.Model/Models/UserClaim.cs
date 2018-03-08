using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.AspNetCore.Identity;

namespace Urbagestion.Model.Models
{
    [Table("UserClaim")]
    public class UserClaim : IdentityUserClaim<int>
    {
    }
}