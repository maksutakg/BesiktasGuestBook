using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Domain.Entities
{
    public class UserDto
    {
        public string Name { get; set; }
        public string SurName { get; set; }
        public string Mail { get; set; }
        public DateTime DateTime { get; set; } = DateTime.UtcNow;
        public List<NoteDto> Notes { get; set; }

    }
}
