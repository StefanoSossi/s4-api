using AutoMapper;
using Moq;
using s4.Data;
using s4.Data.Models;
using s4.Logic.Managers;
using s4.Logic.Managers.Interfaces;
using s4.Logic.Models;

namespace s4.Tests
{
    public class ClassesManagerTests
    {
        private readonly Mock<IUnitOfWork> _mockUow;
        private readonly Mock<IMapper> _mockMapper;
        private readonly Mock<IStudentManager> _mockStudentManager;
        private readonly ClassesManager _classesManager;

        public ClassesManagerTests()
        {
            _mockUow = new Mock<IUnitOfWork>();
            _mockMapper = new Mock<IMapper>();
            _mockStudentManager = new Mock<IStudentManager>();
            _classesManager = new ClassesManager(_mockUow.Object, _mockMapper.Object, _mockStudentManager.Object);
        }

        [Fact]
        public async Task GetAll_ShouldReturnAllClassesWithStudents()
        {
            var classId1 = Guid.NewGuid();
            var classId2 = Guid.NewGuid();
            var classes = new List<Class>
            {
                new Class { Id = classId1, Title = "Title1", Code = "classId1" },
                new Class { Id = classId2, Title = "Title2", Code = "classId2" },
            };

            var classesDto = new List<ClassDto>
            {
                new ClassDto { Id = classId1, Title = "Title1", Code = "classId1" },
                new ClassDto { Id = classId2, Title = "Title2", Code = "classId2" },
            };

            _mockUow.Setup(uow => uow.ClassRepository.GetAllAsync()).ReturnsAsync(classes);
            _mockMapper.Setup(m => m.Map<IEnumerable<ClassDto>>(classes)).Returns(classesDto);

            var result = await _classesManager.GetAll();

            Assert.NotNull(result);
            Assert.Single(result);
        }

        [Fact]
        public async Task GetById_ShouldReturnClassWithStudents_WhenClassExists()
        {
            var classId = Guid.NewGuid();
            var classItem = new Class { Id = classId, Title = "Title2", Code = "classId" };
            var classDto = new ClassDto { Id = classId, Title = "Title1", Code = "classId" };
         
            _mockUow.Setup(uow => uow.ClassRepository.GetByIdAsync(classId)).ReturnsAsync(classItem);
            _mockMapper.Setup(m => m.Map<ClassDto>(classItem)).Returns(classDto);

            var result = await _classesManager.GetById(classId);

            Assert.NotNull(result);
            Assert.Equal(classId, result.Id);
        }

        [Fact]
        public async Task GetById_ShouldThrowException_WhenClassDoesNotExist()
        {
            var classId = Guid.NewGuid();

            _mockUow.Setup(uow => uow.ClassRepository.GetByIdAsync(classId)).ReturnsAsync((Class)null);

            var exception = await Assert.ThrowsAsync<Exception>(() => _classesManager.GetById(classId));
            Assert.Equal($"Class with ID {classId} not found", exception.Message);
        }
    }
}