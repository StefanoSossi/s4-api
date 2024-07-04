using AutoMapper;
using Logic.Managers.Interfaces;
using s4.Data;
using s4.Data.Models;
using s4.Logic.Managers.Interfaces;
using s4.Logic.Models;

namespace s4.Logic.Managers
{
    public class StudentManager(IUnitOfWork uow, IMapper mapper, IClassesManager _classesManager) : IStudentManager
    {
        private readonly IUnitOfWork _uow = uow;
        private readonly IMapper _mapper = mapper;

        public async Task<IEnumerable<StudentDto>> GetAll()
        {
            IEnumerable<Student> student = await _uow.StudentRepository.GetAllAsync();
            IEnumerable<StudentDto> studentsDto = _mapper.Map<IEnumerable<StudentDto>>(student);
            return studentsDto;
        }

        public async Task<StudentDto> GetById(Guid id)
        {
            Student student = await _uow.StudentRepository.GetByIdAsync(id)
                ?? throw new Exception($"Student with ID {id} not found");
            StudentDto studentDto = _mapper.Map<StudentDto>(student);
            return studentDto;
        }

        public async Task<StudentDto> Create(StudentDto studentDto)
        {
            if(studentDto == null) throw new Exception("Fields should not be empty");
            Student newStudent = _mapper.Map<Student>(studentDto);
            Student createResponse = await _uow.StudentRepository.CreateAsync(newStudent);
            StudentDto createdStudentDto = _mapper.Map<StudentDto>(createResponse);
            IEnumerable<StudentClass> studentClasses = await _uow.StudentClassRepository.GetClassesOfStudent(createdStudentDto.Id);
            List<ClassDto> classes = [];
            foreach (StudentClass studentClass in studentClasses)
            {
                ClassDto classdto = await _classesManager.GetById(studentClass.ClassId);
                classes.Add(classdto);
            }
            createdStudentDto.Classes = classes;
            return createdStudentDto;
        }

        public async Task<StudentDto> Update(StudentDto studentDto, Guid id)
        {
            if(studentDto == null) throw new Exception("Fields should not be empty");
            if (!studentDto.IsValid()) throw new Exception("Invalid Name");

            Student studentToEdit = await _uow.StudentRepository.GetByIdAsync(id)
                ?? throw new Exception($"Student with id {id} not found");
            StudentDto editedstudent = _mapper.Map<StudentDto>(studentDto);
            studentToEdit.FirstName = editedstudent.FirstName;
            studentToEdit.LastName = editedstudent.LastName;
            studentToEdit = await _uow.StudentRepository.UpdateAsync(studentToEdit);

            StudentDto studentEdited = _mapper.Map<StudentDto>(studentToEdit);
            IEnumerable<StudentClass> studentClasses = await _uow.StudentClassRepository.GetClassesOfStudent(studentEdited.Id);
            List<ClassDto> classes = [];
            foreach (StudentClass studentClass in studentClasses)
            {
                ClassDto classdto = await _classesManager.GetById(studentClass.ClassId);
                classes.Add(classdto);
            }
            studentEdited.Classes = classes;
            return studentEdited;
        }

        public async Task<bool> Delete(Guid studentId)
        {
            Student student = await _uow.StudentRepository.GetByIdAsync(studentId)
                ?? throw new Exception($"Student with ID {studentId} not found");
            await _uow.StudentClassRepository.RemoveStudentOfAllClasses(studentId);
            await _uow.StudentRepository.DeleteAsync(student);
            return await _uow.StudentRepository.GetByIdAsync(studentId) == null;
        }

        public Task<StudentDto> AddClass(Guid classId, Guid studentId)
        {
            return null;
        }
        public Task<StudentDto> RemoveClass(Guid classId, Guid studentId)
        {
            return null;
        }
        public async Task<IEnumerable<ClassDto>> GetAllClasses(Guid studentId)
        {
            Student student = await _uow.StudentRepository.GetByIdAsync(studentId)
                ?? throw new Exception($"Student with ID {studentId} not found");
            IEnumerable<StudentClass> studentClasses = await _uow.StudentClassRepository.GetClassesOfStudent(studentId);
            List<ClassDto> classes = [];
            foreach (StudentClass studentClass in studentClasses)
            {
                ClassDto classdto = await _classesManager.GetById(studentClass.ClassId);
                classes.Add(classdto);
            }
            return classes;
        }
    }
}
