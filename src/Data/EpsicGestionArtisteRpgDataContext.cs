using Epsic.Gestion_artistes.Rpg.Models;
using Microsoft.EntityFrameworkCore;

namespace Epsic.Gestion_artistes.Rpg.Data
{
    public class EpsicGestionArtisteRpgDataContext : DbContext
    {
        public DbSet<Artiste> Artistes { get; set; }
        public DbSet<Album> Albums { get; set; }
        public DbSet<Music> Musics { get; set; }

        public EpsicGestionArtisteRpgDataContext(DbContextOptions<EpsicGestionArtisteRpgDataContext> options)
            : base(options)
        {
            
        }
    }
}