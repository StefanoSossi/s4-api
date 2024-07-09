using AutoMapper;
using s4.Data;
using s4.Logic.Managers.Interfaces;
using s4.Logic.Models;

namespace s4.Logic.Managers
{
    public class ClassesManager(IUnitOfWork uow, IMapper mapper, IStudentManager _studentManager) : IClassesManager
    {
        public Task<IEnumerable<StudentDto>> GetAll(){ return null; }
        public Task<StudentDto> GetById(Guid id){ return null; }
        public Task<StudentDto> Create(StudentDto newClass){ return null; }
        public Task<StudentDto> Update(StudentDto clasUpdated, Guid id){ return null; }
        public Task<bool> Delete(Guid classId){ return null; }
        public Task<StudentDto> AddStudent(Guid classId, Guid studentId){ return null; }
        public Task<StudentDto> RemoveStudent(Guid classId, Guid studentId){ return null; }
        public Task<StudentDto> GetAllStudents(Guid classId, Guid studentId){ return null; }
    }
}
