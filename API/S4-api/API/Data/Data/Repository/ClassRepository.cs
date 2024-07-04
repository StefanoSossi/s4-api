using s4.Data.Models;
using s4.Data.Repository.Generic;

namespace s4.Data.Repository
{
    public class ClassRepository : GenericRepository<Class>, IGenericRepository<Class>
    {
        public ClassRepository(S4DBContext s4DbContext) : base(s4DbContext) { }
    }
}
