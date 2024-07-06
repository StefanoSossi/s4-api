using Microsoft.EntityFrameworkCore;
using s4.Data.Repository;
using s4.Data.Repository.Generic;
using Serilog;
using s4.Data.Repository.Interfaces;

namespace s4.Data
{
    public class UnitOfWork : IUnitOfWork
    {
        private readonly S4DBContext _S4DBContext;

        private readonly IClassRepository _classRepository;

        private readonly IStudentRepository _studentRepository; 

        private readonly IStudentClassRepository _studentClassRepository;

        private readonly IDataSeederRepository _dataSeederRepository;

        public UnitOfWork(S4DBContext dbContext)
        {
            _S4DBContext = dbContext;
            _classRepository = new ClassRepository(_S4DBContext);
            _studentRepository = new StudentRepository(_S4DBContext);
            _studentClassRepository = new StudentClassRepository(_S4DBContext);
            _dataSeederRepository = new DataSeederRepository(_S4DBContext);
        }
        public void BeginTransaction()
        {
            _S4DBContext.Database.BeginTransaction();
        }

        public void CommitTransaction()
        {
            _S4DBContext.Database.CommitTransaction();
        }

        public void RollBackTransaction()
        {
            _S4DBContext.Database.RollbackTransaction();
        }

        public void Save()
        {
            try
            {
                BeginTransaction();
                _S4DBContext.SaveChanges();
                CommitTransaction();
            }
            catch (DbUpdateConcurrencyException ex)
            {
                RollBackTransaction();
                string message = $"Error to save changes on Database -> Save() {Environment.NewLine}Message: {ex.Message}{Environment.NewLine}";
                Log.Error(ex, $"{message}{Environment.NewLine} Stack trace: {Environment.NewLine}");
                throw new DatabaseException("Can not save changes, error in Database", ex.InnerException);
            }
            catch (DbUpdateException ex)
            {
                RollBackTransaction();
                string message = $"Error to save changes on Database -> Save() {Environment.NewLine}Message: {ex.Message}{Environment.NewLine}";
                Log.Error(ex, $"{message}{Environment.NewLine} Stack trace: {Environment.NewLine}");
                throw new DatabaseException("Can not save changes, error in Database", ex.InnerException);
            }
            catch (Exception ex)
            {
                string message = $"Error to save changes on Database -> Save() {Environment.NewLine}Message: {ex.Message}{Environment.NewLine}";
                Log.Error(ex, $"{message}{Environment.NewLine} Stack trace: {Environment.NewLine}");
                throw new DatabaseException("Can not save changes, error in Database", ex.InnerException);
            }
        }

        public IStudentClassRepository StudentClassRepository
        {
            get { return _studentClassRepository; }
        }

        public IClassRepository ClassRepository
        {
            get { return _classRepository; }
        }

        public IStudentRepository StudentRepository
        {
            get { return _studentRepository; }
        }

        public IDataSeederRepository DataSeederRepository
        {
            get { return _dataSeederRepository; }
        }
    }
}
