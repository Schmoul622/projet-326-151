using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Epsic.Gestion_artistes.Rpg.Data;
using Epsic.Gestion_artistes.Rpg.Models;
using Microsoft.EntityFrameworkCore;

namespace Epsic.Gestion_artistes.Rpg.Repositories
{
    public class MusicRepository : IMusicRepository
    {
        private readonly EpsicGestionArtisteRpgDataContext _context;

        public MusicRepository(EpsicGestionArtisteRpgDataContext context)
        {
            _context = context;
        }

        public MusicDetailViewModel GetSingle(int id)
        {
            return _context.Musics.Select(t => new MusicDetailViewModel
            {
                Id = t.Id,
                Name = t.Name,
                Duration = t.Duration,
            }).FirstOrDefault(c => c.Id == id);
        }

        public Music Update(int id, UpdateMusicDto updateMusicDto)
        {
            var music = _context.Musics.First(c => c.Id == id);

            music.Name = updateMusicDto.Name;
            music.Duration = updateMusicDto.Duration;
            
            _context.SaveChanges();
            return music;
        }
        
        public Music Create(CreateMusicDto createMusicDto)
        {
            var musicDb = new Music();
            musicDb.Name = createMusicDto.Name;
            musicDb.Duration = createMusicDto.Duration;
            musicDb.AlbumId = createMusicDto.AlbumId;

            _context.Musics.Add(musicDb);
            _context.SaveChanges();

            return musicDb;
        }
        
        public async Task<int> Delete(int id)
        {
            _context.Musics.Remove(await _context.Musics.FirstOrDefaultAsync(c => c.Id == id));
            return await _context.SaveChangesAsync();
        }
        
        public bool ExistsById(int id)
        {
            return _context.Musics.Any(c => c.Id == id);
        }
    }
}