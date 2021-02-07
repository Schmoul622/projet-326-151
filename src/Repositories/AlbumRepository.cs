using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Epsic.Gestion_artistes.Rpg.Data;
using Epsic.Gestion_artistes.Rpg.Models;
using Microsoft.EntityFrameworkCore;

namespace Epsic.Gestion_artistes.Rpg.Repositories
{
    public class AlbumRepository : IAlbumRepository
    {
        private readonly EpsicGestionArtisteRpgDataContext _context;
        public AlbumRepository(EpsicGestionArtisteRpgDataContext context)
        {
            _context = context;
        }

        public AlbumDetailViewModel GetSingle(int id)
        {
            // Retourne un album avec toutes ces musiques liées
            return _context.Albums.Include(t => t.Musics)
                                .Select(t => new AlbumDetailViewModel
                                {
                                    Id = t.Id,
                                    Picture = t.Picture,
                                    Name = t.Name,
                                    NumberTitles = t.NumberTitles,
                                    ReleaseDate = t.ReleaseDate,
                                    Duration = t.Duration,
                                    Musics = t.Musics.Select(c => new MusicSummaryViewModel
                                    {
                                        Id = c.Id,
                                        Name = c.Name,
                                        Duration = c.Duration
                                    })
                                }).FirstOrDefault(c => c.Id == id);
        }

        public Album Update(int id, UpdateAlbumDto updateAlbumDto)
        {
            var album = _context.Albums.First(c => c.Id == id);

            album.Picture = updateAlbumDto.Picture;
            album.Name = updateAlbumDto.Name;
            album.NumberTitles = updateAlbumDto.NumberTitles;
            album.ReleaseDate = updateAlbumDto.ReleaseDate;
            album.Duration = updateAlbumDto.Duration;
            
            _context.SaveChanges();
            return album;
        }

        public Album Create(CreateAlbumDto createAlbumDto)
        {
            var albumDb = new Album();
            albumDb.Picture = createAlbumDto.Picture;
            albumDb.Name = createAlbumDto.Name;
            albumDb.NumberTitles = createAlbumDto.NumberTitles;
            albumDb.ReleaseDate = createAlbumDto.ReleaseDate;
            albumDb.Duration = createAlbumDto.Duration;
            albumDb.ArtisteId = createAlbumDto.ArtisteId;

            _context.Albums.Add(albumDb);
            _context.SaveChanges();

            return albumDb;
        }

        public async Task<int> Delete(int id)
        {
            _context.Albums.Remove(await _context.Albums.FirstOrDefaultAsync(c => c.Id == id));
            return await _context.SaveChangesAsync();
        }

        public async Task<int> AddMusicToAlbum(int albumId, int musicId)
        {
            var albumDb = await _context.Albums.Include(t => t.Musics).FirstOrDefaultAsync(c => c.Id == albumId);
            
            albumDb.Musics.Add(_context.Musics.Find(musicId));
            
            return await _context.SaveChangesAsync();
        }

        public bool ExistsById(int id)
        {
            return _context.Albums.Any(c => c.Id == id);
        }

        public bool ExistsByName(string name)
        {
            return _context.Albums.Any(c => c.Name == name);
        }

        public byte[] GetPicture(int id)
        {
            return _context.Albums.Find(id).Picture;
        }

        public void SetPicture(int id, byte[] image)
        {
            var album = _context.Albums.Find(id);
            album.Picture = image;
            _context.SaveChanges();
        }
    }
}