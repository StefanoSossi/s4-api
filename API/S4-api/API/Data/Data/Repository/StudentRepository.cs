using s4.Data.Models;
using s4.Data.Repository.Generic;

namespace s4.Data.Repository
{
    internal class StudentRepository : GenericRepository<Student>, IGenericRepository<Student>
    {
        public StudentRepository(S4DBContext s4DbContext) : base(s4DbContext) { }
    }
}
