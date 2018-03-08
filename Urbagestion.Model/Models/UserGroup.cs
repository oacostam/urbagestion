using System.ComponentModel.DataAnnotations.Schema;

namespace Urbagestion.Model.Models
{
    [Table("UserGroup")]
    public class UserGroup
    {
        public User User { get; set; }

        public int UserId { get; set; }

        public Group Group { get; set; }

        public int GroupId { get; set; }
    }
}