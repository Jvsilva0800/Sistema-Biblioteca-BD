using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Npgsql;

namespace SistemaBiblioteca.Infraestruture
{
    public class DbConnection : IDisposable
    {
        public DbConnection()
        {
            Connection = new NpgsqlConnection("Server=localhost;Port=5432;Database=Biblioteca;User Id=postgres;Password=180211123;Encoding=UTF8;");
            Connection.Open();
        }
        public NpgsqlConnection Connection { get; set; }

        public void Dispose()
        {
            Connection.Dispose();
        }
    }
}