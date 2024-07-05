using s4.Data.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace s4.Data.Models
{
    public class StudentClass : Entity
    {
        public Guid StudentId { get; set; }
        public Student? Student { get; set; }

        public Guid ClassId { get; set; }
        public Class? Class { get; set; }
    }
}
