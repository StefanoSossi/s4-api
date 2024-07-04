using Microsoft.EntityFrameworkCore;
using s4.Data.Repository.Generic;
using Serilog;

namespace s4.Data
{
    public class UnitOfWork
    {
        private readonly S4DBContext _S4DBContext;

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
    }
}
