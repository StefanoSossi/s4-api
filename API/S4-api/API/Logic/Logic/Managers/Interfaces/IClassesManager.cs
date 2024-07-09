using s4.Logic.Models;

namespace s4.Logic.Managers.Interfaces
{
    public interface IClassesManager
    {
        Task<IEnumerable<ClassDto>> GetAll();
        Task<ClassDto> GetById(Guid id);
        Task<ClassDto> Create(ClassDto newClassDto);
        Task<ClassDto> Update(ClassDto clasUpdatedDto, Guid id);
        Task<bool> Delete(Guid classId);
        Task<ClassDto> AddStudent(Guid classId, Guid studentId);
        Task<ClassDto> RemoveStudent(Guid classId, Guid studentId);
        Task<IEnumerable<StudentDto>> GetAllStudents(Guid classId);

    }
}
