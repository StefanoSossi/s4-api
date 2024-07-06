
using s4.Data.Repository.Interfaces;

namespace s4.Data
{
    public interface IUnitOfWork
    {
        IClassRepository ClassRepository { get; }
        IStudentRepository StudentRepository { get; }
        IStudentClassRepository StudentClassRepository { get; }
        IDataSeederRepository DataSeederRepository { get; }
        void BeginTransaction();
        void CommitTransaction();
        void RollBackTransaction();
        void Save();

    }
}
