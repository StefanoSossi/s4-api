
namespace s4.Data
{
    public interface IUnitOfWork
    {
        void BeginTransaction();
        void CommitTransaction();
        void RollBackTransaction();
        void Save();
    }
}
