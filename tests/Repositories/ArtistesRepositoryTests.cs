using System.Data.Common;
using System.Threading.Tasks;
using Epsic.Gestion_artistes.Rpg.Data;
using Epsic.Gestion_artistes.Rpg.Repositories;
using Microsoft.Data.Sqlite;
using Microsoft.EntityFrameworkCore;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace tests.Repositories
{
    [TestClass]
    public class ArtistesRepositoryTests
    {
        private readonly DbContextOptions<EpsicGestionArtisteRpgDataContext> _options;

        public ArtistesRepositoryTests()
        {
            _options = new DbContextOptionsBuilder<EpsicGestionArtisteRpgDataContext>()
                .UseSqlite(CreateInMemoryDatabase()).Options;
        }
        
        private static DbConnection CreateInMemoryDatabase()
        {
            var connection = new SqliteConnection($"Filename=:memory:");

            connection.Open();
            
            return connection;
        }
    }
    
}