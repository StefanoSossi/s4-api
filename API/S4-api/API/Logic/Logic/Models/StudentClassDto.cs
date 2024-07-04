using s4.Data.Models;
using s4.Logic.Models.Validation;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace s4.Logic.Models
{
    public class StudentClassDto
    {
        public Guid Id { get; set; }
        public Guid StudentId { get; set; }
        public Guid ClassId { get; set; }
    }
}
