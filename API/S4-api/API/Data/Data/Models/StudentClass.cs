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
        public required Student Student { get; set; }

        public Guid ClassId { get; set; }
        public required Class Class { get; set; }
    }
}
