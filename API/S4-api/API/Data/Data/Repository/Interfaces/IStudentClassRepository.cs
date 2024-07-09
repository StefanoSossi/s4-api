using s4.Data.Repository.Generic;
using s4.Data.Models;

namespace s4.Data.Repository.Interfaces
{
    public interface IStudentClassRepository : IGenericRepository<StudentClass>
    {
        Task<IEnumerable<StudentClass>> GetStudentsOnClass(Guid classId);
        Task<IEnumerable<StudentClass>> GetClassesOfStudent(Guid studentId);
        Task RemoveStudentOfClass(Guid classId, Guid studentId);
        Task RemoveStudentOfAllClasses(Guid studentId);
    }
}
