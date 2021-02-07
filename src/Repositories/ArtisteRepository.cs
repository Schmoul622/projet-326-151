using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Epsic.Gestion_artistes.Rpg.Data;
using Epsic.Gestion_artistes.Rpg.Models;
using Microsoft.EntityFrameworkCore;

namespace Epsic.Gestion_artistes.Rpg.Repositories
{
    public class ArtisteRepository : IArtisteRepository
    {
        private readonly EpsicGestionArtisteRpgDataContext _context;
        public ArtisteRepository(EpsicGestionArtisteRpgDataContext context)
        {
            _context = context;
        }

        public ArtisteDetailViewModel GetSingle(int id)
        {
            return _context.Artistes.Include(t => t.Albums)
                                 .Select(t => new ArtisteDetailViewModel
                                 {
                                     Id = t.Id,
                                     Picture = t.Picture,
                                     Speudo = t.Speudo,
                                     FirstName = t.FirstName,
                                     Name = t.Name,
                                     Age = t.Age,
                                     CarrierStart = t.CarrierStart,
                                     Albums = t.Albums.Select(c => new AlbumSummaryViewModel
                                     {
                                         Id = c.Id,
                                         Picture = c.Picture,
                                         Name = c.Name
                                     })
                                 }).FirstOrDefault(c => c.Id == id);
        }

        public List<ArtisteDetailViewModel> GetAllArtiste()
        {
            return _context.Artistes.Include(t => t.Albums)
                                .Select(t => new ArtisteDetailViewModel
                                {
                                    Id = t.Id,
                                    Picture = t.Picture,
                                    Speudo = t.Speudo,
                                    FirstName = t.FirstName,
                                    Name = t.Name,
                                    Age = t.Age,
                                    CarrierStart = t.CarrierStart,
                                    Albums = t.Albums.Select(c => new AlbumSummaryViewModel
                                    {
                                        Id = c.Id,
                                        Picture = c.Picture,
                                        Name = c.Name
                                    })
                                }).ToList();
        }

        public Artiste Update(int id, UpdateArtisteDto artisteToUpdate)
        {
            var artiste = _context.Artistes.First(c => c.Id == id);

            artiste.Picture = artisteToUpdate.Picture;
            artiste.Speudo = artisteToUpdate.Speudo;
            artiste.FirstName = artisteToUpdate.FirstName;
            artiste.Name = artisteToUpdate.Name;
            artiste.Age = artisteToUpdate.Age;
            artiste.CarrierStart = artisteToUpdate.CarrierStart;

            _context.SaveChanges();
            return artiste;
        }

        public Artiste Create(CreateArtisteDto artisteToCreate)
        {
            var artisteDb = new Artiste();
            artisteDb.Picture = artisteToCreate.Picture;
            artisteDb.Speudo = artisteToCreate.Speudo;
            artisteDb.FirstName = artisteToCreate.FirstName;
            artisteDb.Name = artisteToCreate.Name;
            artisteDb.Age = artisteToCreate.Age;
            artisteDb.CarrierStart = artisteToCreate.CarrierStart;

            _context.Artistes.Add(artisteDb);
            _context.SaveChangesAsync();

            return artisteDb;
        }

        public async Task<int> Delete(int id)
        {
            _context.Artistes.Remove(await _context.Artistes.FirstOrDefaultAsync(c => c.Id == id));
            return await _context.SaveChangesAsync();
        }

        public async Task<int> AddAlbumToArtiste(int artisteId, int albumId)
        {
            var artisteDb = await _context.Artistes.Include(t => t.Albums).FirstOrDefaultAsync(c => c.Id == artisteId);

            artisteDb.Albums.Add(_context.Albums.Find(albumId));

            return await _context.SaveChangesAsync();
        }

        public bool ExistsById(int id)
        {
            return _context.Artistes.Any(c => c.Id == id);
        }

        public bool ExistsBySpeudo(string speudo)
        {
            return _context.Artistes.Any(c => c.Speudo == speudo);
        }

        public byte[] GetPicture(int id)
        {
            return _context.Artistes.Find(id).Picture;
        }

        public void SetPicture(int id, byte[] image)
        {
            var artiste = _context.Artistes.Find(id);
            artiste.Picture = image;
            _context.SaveChanges();
        }
    }
}