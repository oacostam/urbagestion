using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.AspNetCore.Identity;

namespace Urbagestion.Model.Models
{
    [Table("UserToken")]
    public class UserToken : IdentityUserToken<int>
    {
    }
}