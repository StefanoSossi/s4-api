using AutoMapper;
using s4.Logic.Managers.Interfaces;
using s4.Data;
using s4.Data.Models;
using s4.Logic.Models;
using s4.Logic.Exceptions;
using s4.Presentation.Exceptions;

namespace s4.Logic.Managers
{
    public class StudentsManager(IUnitOfWork uow, IMapper mapper) : IStudentManager
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
                ?? throw new NotFoundException($"Student with ID {id} not found");
            StudentDto studentDto = _mapper.Map<StudentDto>(student);
            return studentDto;
        }

        public async Task<StudentDto> Create(StudentDto studentDto)
        {
            if(studentDto == null) throw new BadRequestException("Fields should not be empty");
            Student newStudent = _mapper.Map<Student>(studentDto);
            Student createResponse = await _uow.StudentRepository.CreateAsync(newStudent);
            StudentDto createdStudentDto = _mapper.Map<StudentDto>(createResponse);
            return createdStudentDto;
        }

        public async Task<StudentDto> Update(StudentDto studentDto, Guid id)
        {
            if(studentDto == null) throw new BadRequestException("Fields should not be empty");
            if (!studentDto.IsValid()) throw new BadRequestException("Invalid Name");

            Student studentToEdit = await _uow.StudentRepository.GetByIdAsync(id)
                ?? throw new NotFoundException($"Student with id {id} not found");
            StudentDto editedstudent = _mapper.Map<StudentDto>(studentDto);
            studentToEdit.FirstName = editedstudent.FirstName;
            studentToEdit.LastName = editedstudent.LastName;
            studentToEdit = await _uow.StudentRepository.UpdateAsync(studentToEdit);
            StudentDto studentEditedDto = _mapper.Map<StudentDto>(studentToEdit);
            return studentEditedDto;
        }

        public async Task<bool> Delete(Guid studentId)
        {
            Student student = await _uow.StudentRepository.GetByIdAsync(studentId)
                ?? throw new NotFoundException($"Student with ID {studentId} not found");
            await _uow.StudentClassRepository.RemoveStudentOfAllClasses(studentId);
            await _uow.StudentRepository.DeleteAsync(student);
            return await _uow.StudentRepository.GetByIdAsync(studentId) == null;
        }

        public async Task<StudentDto> AddClass(Guid classId, Guid studentId)
        {
            Student student = await _uow.StudentRepository.GetByIdAsync(studentId)
                ?? throw new NotFoundException($"Student with ID {studentId} not found");
            _ = await _uow.ClassRepository.GetByIdAsync(classId)
                ?? throw new NotFoundException($"Class with ID {classId} not found");
            StudentClass newStudentClass = new()
            {
                StudentId = studentId,
                ClassId = classId
            };
            await _uow.StudentClassRepository.CreateAsync(newStudentClass);
            StudentDto studentEditedDto = _mapper.Map<StudentDto>(student);
            return studentEditedDto;
        }
        public async Task<StudentDto> RemoveClass(Guid classId, Guid studentId)
        {
            Student student = await _uow.StudentRepository.GetByIdAsync(studentId)
                ?? throw new NotFoundException($"Student with ID {studentId} not found");
            _ = await _uow.ClassRepository.GetByIdAsync(classId)
                ?? throw new NotFoundException($"Class with ID {classId} not found");
            await _uow.StudentClassRepository.RemoveStudentOfClass(classId, studentId);
            StudentDto studentEditedDto = _mapper.Map<StudentDto>(student);
            return studentEditedDto;
        }
    }
}
