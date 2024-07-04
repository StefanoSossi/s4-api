using s4.Logic.Models;

namespace Logic.Managers.Interfaces
{
    internal interface IStudentManager
    {
        Task<IEnumerable<StudentDto>> GetAll();
        Task<StudentDto> GetById(Guid id);
        Task<StudentDto> Create(StudentDto newStudent);
        Task<StudentDto> Update(StudentDto clasUpdated, Guid id);
        Task<bool> Delete(Guid studentId);
        Task<StudentDto> AddClass(Guid classId, Guid studentId);
        Task<StudentDto> RemoveClass(Guid classId, Guid studentId);
        Task<StudentDto> GetAllClasses(Guid classId, Guid studentId);
    }
}
