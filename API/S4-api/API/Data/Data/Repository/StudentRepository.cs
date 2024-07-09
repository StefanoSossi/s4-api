using s4.Data.Models;
using s4.Data.Repository.Generic;
using s4.Data.Repository.Interfaces;

namespace s4.Data.Repository
{
    public class StudentRepository : GenericRepository<Student>, IStudentRepository
    {
        public StudentRepository(S4DBContext s4DbContext) : base(s4DbContext) { }
    }
}
