using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Domain.Common
{
   public class EntityBase
    {
        public int Id { get; set; }
        public DateTime DateTime { get; set; } = DateTime.UtcNow;
        public bool IsDeleted { get; set; } = false;
        public bool IsActive { get; set; } = true;
        public string Role { get; set; } = "User";
    }
}
