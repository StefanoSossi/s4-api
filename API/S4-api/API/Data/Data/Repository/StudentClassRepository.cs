

using s4.Data.Models;
using s4.Data.Repository.Generic;
using System.Text.RegularExpressions;

namespace s4.Data.Repository
{
    public class StudentClassRepository : GenericRepository<StudentClass>, IGenericRepository<StudentClass>
    {
        public StudentClassRepository(S4DBContext s4DbContext) : base(s4DbContext) { }
        public async Task<IEnumerable<StudentClass>> GetStudentsOnClass(Guid classId)
        {
            var studentClass = await GetAllAsync();
            studentClass = studentClass.Where(sc => sc.ClassId == classId);
            return studentClass;
        }
        public async Task RemoveStudentOfClass(Guid classId, Guid studentId)
        {
            var studentClasses = await GetAllAsync();
            var studentClass = studentClasses.SingleOrDefault(ug => ug.StudentId == studentId && ug.ClassId == classId);
            await DeleteAsync(studentClass);
        }
    }
}
