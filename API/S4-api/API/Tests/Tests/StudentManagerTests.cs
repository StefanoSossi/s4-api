using AutoMapper;
using Moq;
using s4.Data;
using s4.Data.Models;
using s4.Logic.Managers;
using s4.Logic.Managers.Interfaces;
using s4.Logic.Models;

namespace s4.Tests
{
    public class StudentManagerTests
    {
        private readonly Mock<IUnitOfWork> _mockUow;
        private readonly Mock<IMapper> _mockMapper;
        private readonly Mock<IClassesManager> _mockClassesManager;
        private readonly StudentsManager _studentManager;
        public StudentManagerTests()
        {
            _mockUow = new Mock<IUnitOfWork>();
            _mockMapper = new Mock<IMapper>();
            _mockClassesManager = new Mock<IClassesManager>();
            _studentManager = new StudentsManager(_mockUow.Object, _mockMapper.Object, _mockClassesManager.Object);

        }

        [Fact]
        public async Task GetAll_ShouldReturnAllStudents()
        {
            var studentId1 = Guid.NewGuid();
            var studentId2 = Guid.NewGuid();
            var students = new List<Student>
            {
                new Student { Id = studentId1, FirstName = "John", LastName = "Doe" },
                new Student { Id = studentId2, FirstName = "Jane", LastName = "Smith" }
            };

            var studentsDto = new List<StudentDto>
            {
                new StudentDto { Id = studentId1, FirstName = "John", LastName = "Doe", Classes = [] },
                new StudentDto { Id = studentId2, FirstName = "Jane", LastName = "Smith", Classes = [] }
            };

            _mockUow.Setup(uow => uow.StudentRepository.GetAllAsync()).ReturnsAsync(students);
            _mockMapper.Setup(m => m.Map<IEnumerable<StudentDto>>(It.IsAny<IEnumerable<Student>>())).Returns(studentsDto);
            
            var result = await _studentManager.GetAll();

            Assert.NotNull(result);
            Assert.Equal(2, result.ToList().Count());
            Assert.Equal("John", result.First().FirstName);
        }

        [Fact]
        public async Task GetById_ShouldReturnStudent_WhenStudentExists()
        {
            var studentId = Guid.NewGuid();
            var student = new Student { Id = studentId, FirstName = "John", LastName = "Doe" };
            var studentDto = new StudentDto { Id = studentId, FirstName = "John", LastName = "Doe", Classes = [] };

            _mockUow.Setup(uow => uow.StudentRepository.GetByIdAsync(studentId)).ReturnsAsync(student);
            _mockMapper.Setup(m => m.Map<StudentDto>(It.IsAny<Student>()))
                .Returns(studentDto);

            var result = await _studentManager.GetById(studentId);

            Assert.NotNull(result);
            Assert.Equal(studentId, result.Id);
            Assert.Equal("John", result.FirstName);
        }

        [Fact]
        public async Task GetById_ShouldThrowException_WhenStudentDoesNotExist()
        {
            var studentId = Guid.NewGuid();

            _mockUow.Setup(uow => uow.StudentRepository.GetByIdAsync(studentId)).ReturnsAsync((Student)null);

            var exception = await Assert.ThrowsAsync<Exception>(() => _studentManager.GetById(studentId));
            Assert.Equal($"Student with ID {studentId} not found", exception.Message);
        }

    }
}

