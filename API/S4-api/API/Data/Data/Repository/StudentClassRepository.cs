

using s4.Data.Models;
using s4.Data.Repository.Generic;
using s4.Data.Repository.Interfaces;
using System.Text.RegularExpressions;

namespace s4.Data.Repository
{
    public class StudentClassRepository : GenericRepository<StudentClass>, IStudentClassRepository
    {
        public StudentClassRepository(S4DBContext s4DbContext) : base(s4DbContext) { }
        public async Task<IEnumerable<StudentClass>> GetStudentsOnClass(Guid classId)
        {
            var studentClass = await GetAllAsync();
            studentClass = studentClass.Where(sc => sc.ClassId == classId);
            return studentClass;
        }

        public async Task<IEnumerable<StudentClass>> GetClassesOfStudent(Guid studentId)
        {
            var studentClass = await GetAllAsync();
            studentClass = studentClass.Where(sc => sc.StudentId == studentId);
            return studentClass;
        }

        public async Task RemoveStudentOfClass(Guid classId, Guid studentId)
        {
            var studentClasses = await GetAllAsync();
            var studentClass = studentClasses.SingleOrDefault(ug => ug.StudentId == studentId && ug.ClassId == classId);
            if(studentClass != null) await DeleteAsync(studentClass);

        }

        public async Task RemoveStudentOfAllClasses(Guid studentId)
        {
            var studentClasses = await GetAllAsync();
            var studentClass = studentClasses.SingleOrDefault(ug => ug.StudentId == studentId);
            if (studentClass != null) await DeleteAsync(studentClass);
        }
    }
}
