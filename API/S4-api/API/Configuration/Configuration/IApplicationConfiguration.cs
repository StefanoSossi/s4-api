using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace s4.Configuration
{
    public class DatabaseConnectionStrings
    {
        public string? DATABASE { get; set; }
    }
    public interface IApplicationConfiguration
    {
        DatabaseConnectionStrings GetDatabaseConnectionString();
    }
}
