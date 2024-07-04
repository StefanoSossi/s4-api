using s4.Logic.Models;

namespace s4.Logic.Managers.Interfaces
{
    internal interface IClassesManager
    {
        Task<IEnumerable<StudentDto>> GetAll();
        Task<StudentDto> GetById(Guid id);
        Task<StudentDto> Create(StudentDto newClass);
        Task<StudentDto> Update(StudentDto clasUpdated, Guid id);
        Task<bool> Delete(Guid classId);
        Task<StudentDto> AddStudent(Guid classId, Guid studentId);
        Task<StudentDto> RemoveStudent(Guid classId, Guid studentId);
        Task<StudentDto> GetAllStudents(Guid classId, Guid studentId);

    }
}
