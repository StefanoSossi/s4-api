
namespace s4.Data.Repository.Interfaces
{
    public interface IDataSeederRepository
    {
        Task<string> CreateAsync();
    }
}
