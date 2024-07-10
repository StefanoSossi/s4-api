using AutoMapper;
using s4.Data;
using s4.Data.Models;
using s4.Logic.Exceptions;
using s4.Logic.Managers.Interfaces;
using s4.Logic.Models;
using s4.Presentation.Exceptions;

namespace s4.Logic.Managers
{
    public class ClassesManager(IUnitOfWork uow, IMapper mapper, IStudentManager _studentManager) : IClassesManager
    {
        private readonly IUnitOfWork _uow = uow;
        private readonly IMapper _mapper = mapper;
        public async Task<IEnumerable<ClassDto>> GetAll()
        {
            IEnumerable<Class> classItem = await _uow.ClassRepository.GetAllAsync();
            IEnumerable<ClassDto> classesDto = _mapper.Map<IEnumerable<ClassDto>>(classItem);
            foreach (ClassDto classDto in classesDto)
            {
                classDto.Students = await RetriveStudentsOfClass(classDto.Id);
            }
            return classesDto;
        }
        public async Task<ClassDto> GetById(Guid id)
        {
            Class classItem = await _uow.ClassRepository.GetByIdAsync(id)
                ?? throw new NotFoundException($"Class with ID {id} not found");
            ClassDto classDto = _mapper.Map<ClassDto>(classItem);
            classDto.Students = await RetriveStudentsOfClass(classDto.Id);
            return classDto;
        }
        public async Task<ClassDto> Create(ClassDto newClassDto)
        {
            if (newClassDto == null) throw new BadRequestException("Fields should not be empty");
            Class newClass = _mapper.Map<Class>(newClassDto);
            Class createResponse = await _uow.ClassRepository.CreateAsync(newClass);
            ClassDto createdClassDto = _mapper.Map<ClassDto>(createResponse);
            createdClassDto.Students = await RetriveStudentsOfClass(createdClassDto.Id);
            return createdClassDto;
        }
        public async Task<ClassDto> Update(ClassDto clasUpdatedDto, Guid id)
        {
            if (clasUpdatedDto == null) throw new BadRequestException("Fields should not be empty");
            if (!clasUpdatedDto.IsValid()) throw new BadRequestException("Invalid Name");

            Class classToEdit = await _uow.ClassRepository.GetByIdAsync(id)
                ?? throw new NotFoundException($"Class with id {id} not found");
            ClassDto editedClass = _mapper.Map<ClassDto>(clasUpdatedDto);
            classToEdit.Code = editedClass.Code;
            classToEdit.Description = editedClass.Description;
            classToEdit.Title = editedClass.Title;
            classToEdit = await _uow.ClassRepository.UpdateAsync(classToEdit);
            ClassDto classEditedDto = _mapper.Map<ClassDto>(classToEdit);
            classEditedDto.Students = await RetriveStudentsOfClass(classEditedDto.Id);
            return classEditedDto;
        }
        public async Task<bool> Delete(Guid classId)
        {
            Class classItem = await _uow.ClassRepository.GetByIdAsync(classId)
                ?? throw new NotFoundException($"Student with ID {classId} not found");
            await _uow.StudentClassRepository.RemoveClassOfAllStudents(classId);
            await _uow.ClassRepository.DeleteAsync(classItem);
            return await _uow.ClassRepository.GetByIdAsync(classId) == null;
        }
        public async Task<ClassDto> AddStudent(Guid classId, Guid studentId)
        {
            Class classItem = await _uow.ClassRepository.GetByIdAsync(classId)
                ?? throw new NotFoundException($"Class with ID {classId} not found");
            _ = await _uow.StudentRepository.GetByIdAsync(studentId)
                ?? throw new NotFoundException($"Student with ID {studentId} not found");
            StudentClass newStudentClass = new()
            {
                StudentId = studentId,
                ClassId = classId
            };
            await _uow.StudentClassRepository.CreateAsync(newStudentClass);
            ClassDto classEditedDto = _mapper.Map<ClassDto>(classItem);
            classEditedDto.Students = await RetriveStudentsOfClass(classEditedDto.Id);
            return classEditedDto;
        }
        public async Task<ClassDto> RemoveStudent(Guid classId, Guid studentId)
        {
            Class classItem = await _uow.ClassRepository.GetByIdAsync(classId)
                ?? throw new NotFoundException($"Class with ID {classId} not found");
            _ = await _uow.StudentRepository.GetByIdAsync(studentId)
                ?? throw new NotFoundException($"Student with ID {studentId} not found");
            await _uow.StudentClassRepository.RemoveStudentOfClass(classId, studentId);
            ClassDto classEditedDto = _mapper.Map<ClassDto>(classItem);
            classEditedDto.Students = await RetriveStudentsOfClass(classEditedDto.Id);
            return classEditedDto;
        }
        public async Task<IEnumerable<StudentDto>> GetAllStudents(Guid classId)
        {
            _ = await _uow.ClassRepository.GetByIdAsync(classId)
               ?? throw new NotFoundException($"Class with ID {classId} not found");
            return await RetriveStudentsOfClass(classId);
        }

        private async Task<List<StudentDto>> RetriveStudentsOfClass(Guid classId)
        {
            IEnumerable<StudentClass> studentClasses = await _uow.StudentClassRepository.GetStudentsOnClass(classId);
            List<StudentDto> students = [];
            foreach (StudentClass studentClass in studentClasses)
            {
                StudentDto studentDto = await _studentManager.GetById(studentClass.StudentId);
                students.Add(studentDto);
            }
            return students;
        }
    }
}
