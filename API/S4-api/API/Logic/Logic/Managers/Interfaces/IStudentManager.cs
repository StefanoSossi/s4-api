using s4.Logic.Models;

namespace Logic.Managers.Interfaces
{
    public interface IStudentManager
    {
        Task<IEnumerable<StudentDto>> GetAll();
        Task<StudentDto> GetById(Guid id);
        Task<StudentDto> Create(StudentDto studentDto);
        Task<StudentDto> Update(StudentDto studentDto, Guid id);
        Task<bool> Delete(Guid studentId);
        Task<StudentDto> AddClass(Guid classId, Guid studentId);
        Task<StudentDto> RemoveClass(Guid classId, Guid studentId);
        Task<IEnumerable<ClassDto>> GetAllClasses(Guid studentId);
    }
}
