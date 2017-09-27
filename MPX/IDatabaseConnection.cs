using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using SQLite;

namespace MPX
{
        public interface IDatabaseConnection
        {
            SQLiteConnection GetConnection();
            SQLiteAsyncConnection GetConnectionAsync();
        }
}
