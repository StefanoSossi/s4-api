using AutoMapper;
using s4.Data;
using s4.Logic.Managers.Interfaces;

namespace s4.Logic.Managers
{
    public class ClassesManager(IUnitOfWork uow, IMapper mapper, IStudentManager _studentManager) : IClassesManager
    {
    }
}
