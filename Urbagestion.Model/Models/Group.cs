using System.Collections.Generic;
using Urbagestion.Model.Common;

namespace Urbagestion.Model.Models
{
    public class Group : Entity
    {
        public string Name { get; set; }
        
        public virtual ICollection<UserGroup> UserGroup { get; set; } = new HashSet<UserGroup>();
    }
}