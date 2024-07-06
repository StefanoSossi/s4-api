
using AutoMapper;
using s4.Data;
using s4.Logic.Managers.Interfaces;

namespace s4.Logic.Managers
{
    public class DataSeederManager(IUnitOfWork uow) : IDataSeederManager
    {
        private readonly IUnitOfWork _uow = uow;

        public async Task<string> SeedData()
        {
            return await _uow.DataSeederRepository.CreateAsync();
        }
    }
}
