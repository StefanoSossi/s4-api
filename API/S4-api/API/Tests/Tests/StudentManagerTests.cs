using AutoMapper;
using Moq;
using s4.Data;
using s4.Data.Models;
using s4.Data.Repository.Interfaces;
using s4.Data.Repository.Generic;
using s4.Logic.Managers;
using s4.Logic.Managers.Interfaces;
using s4.Logic.Models;
using s4.Data.Repository;

namespace s4.Tests
{
    public class StudentManagerTests
    {
        private readonly Mock<IUnitOfWork> _mockUow;
        private readonly Mock<IMapper> _mockMapper;
        private readonly Mock<IClassesManager> _mockClassesManager;
        private readonly Mock<IStudentRepository> _mockStudentRepository;
        private readonly Mock<IStudentClassRepository> _mockStudentClassRepository;
        private readonly StudentManager _studentManager;
        public StudentManagerTests()
        {
            _mockUow = new Mock<IUnitOfWork>();
            _mockMapper = new Mock<IMapper>();
            _mockClassesManager = new Mock<IClassesManager>();
            _studentManager = new StudentManager(_mockUow.Object, _mockMapper.Object, _mockClassesManager.Object);
            _mockStudentRepository = new Mock<IStudentRepository>();
            _mockStudentClassRepository = new Mock<IStudentClassRepository>();

        }

        [Fact]
        public async Task GetAll_ShouldReturnAllStudents()
        {
            var students = new List<Student>
            {
                new Student { Id = Guid.NewGuid(), FirstName = "John", LastName = "Doe" },
                new Student { Id = Guid.NewGuid(), FirstName = "Jane", LastName = "Smith" }
            };

            _mockUow.Setup(uow => uow.StudentRepository.GetAllAsync()).ReturnsAsync(students);
            _mockMapper.Setup(m => m.Map<IEnumerable<StudentDto>>(It.IsAny<IEnumerable<Student>>()))
                .Returns((IEnumerable<Student> source) => source.Select(s => new StudentDto { Id = s.Id, FirstName = s.FirstName, LastName = s.LastName }));

            var result = await _studentManager.GetAll();

            Assert.NotNull(result);
            Assert.Equal(2, result.Count());
            Assert.Equal("John", result.First().FirstName);
        }

        [Fact]
        public async Task GetById_ShouldReturnStudent_WhenStudentExists()
        {
            var studentId = Guid.NewGuid();
            var student = new Student { Id = studentId, FirstName = "John", LastName = "Doe" };

            _mockUow.Setup(uow => uow.StudentRepository.GetByIdAsync(studentId)).ReturnsAsync(student);
            _mockMapper.Setup(m => m.Map<StudentDto>(It.IsAny<Student>()))
                .Returns((Student source) => new StudentDto { Id = source.Id, FirstName = source.FirstName, LastName = source.LastName });

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

