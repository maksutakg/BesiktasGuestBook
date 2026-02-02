using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.Json.Serialization;
using System.Threading.Tasks;

namespace Domain.Entities
{
    public class Note
    {
        public int Id { get; set; }
        public string text { get; set; }
        public DateTime DateTime { get; set; } = DateTime.UtcNow;
            
        public int UserId { get; set; }

        [JsonIgnore]
        public User user { get; set; } = null!;

        public int MahalleId { get; set; }

        [JsonIgnore]
        public Mahalle mahalle { get; set; }
   
    }
}
