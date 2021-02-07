using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Epsic.Gestion_artistes.Rpg.Data;
using Epsic.Gestion_artistes.Rpg.Exceptions;
using Epsic.Gestion_artistes.Rpg.Models;
using Epsic.Gestion_artistes.Rpg.Repositories;

namespace Epsic.Gestion_artistes.Rpg.Services
{
    public class ArtisteService : IArtisteService
    {
        private readonly IArtisteRepository _artisteRepository;

        public ArtisteService(IArtisteRepository artisteRepository)
        {
            _artisteRepository = artisteRepository;
        }
        
        public ArtisteDetailViewModel GetSingle(int id)
        {
            if (id < 1)
                throw new ArgumentOutOfRangeException(nameof(id), id, "Id cannot be lower than 1.");

            var artisteDb = _artisteRepository.GetSingle(id);

            return artisteDb;
        }

        public  List<ArtisteDetailViewModel> GetAllArtiste()
        {
            var allArtiste = _artisteRepository.GetAllArtiste();
            return allArtiste;
        }

        public Artiste Update(int id, UpdateArtisteDto artisteToUpdate)
        {
            if (id < 1)
                throw new ArgumentOutOfRangeException(nameof(id), id, "Id cannot be lower than 1.");

            if (artisteToUpdate == null)
                throw new ArgumentNullException(nameof(artisteToUpdate));

            if (artisteToUpdate.Speudo.Length > 32)
                throw new ArgumentOutOfRangeException(nameof(artisteToUpdate.Speudo), artisteToUpdate.Speudo, "Artiste name length cannot be greater than 32.");

            if (!_artisteRepository.ExistsById(id))
                throw new DataNotFoundException($"Artiste Id:{id} doesn't exists.");
            return _artisteRepository.Update(id, artisteToUpdate);
        }

        public Artiste Create(CreateArtisteDto artisteToCreate)
        {
            if (artisteToCreate == null)
                throw new ArgumentNullException(nameof(artisteToCreate));

            if (artisteToCreate.Speudo.Length > 32)
                throw new ArgumentOutOfRangeException(nameof(artisteToCreate.Speudo), artisteToCreate.Speudo, "Artiste name length cannot be greater than 32.");

            if (_artisteRepository.ExistsBySpeudo(artisteToCreate.Speudo))
                throw new ArgumentException(nameof(artisteToCreate.Speudo), $"Artiste {artisteToCreate.Speudo} already exists.");

            var modelDb = _artisteRepository.Create(artisteToCreate);

            return modelDb;
        }

        public async Task<bool> Delete(int id)
        {
            if (id < 1)
                throw new ArgumentOutOfRangeException(nameof(id), id, "Id cannot be lower than 1.");

            var result = await _artisteRepository.Delete(id);

            if (result == 1)
                return true;
            else
                return false;
        }
        
        public async Task<int> AddAlbumToArtiste(AddAlbumToArtisteDto addAlbumToArtiste)
        {
            if (addAlbumToArtiste == null)
                throw new ArgumentNullException(nameof(addAlbumToArtiste));

            if (addAlbumToArtiste.ArtisteId < 1)
                throw new ArgumentOutOfRangeException(nameof(addAlbumToArtiste.ArtisteId), addAlbumToArtiste.ArtisteId, "Artiste Id cannot be lower than 1.");

            if (addAlbumToArtiste.AlbumId < 1)
                throw new ArgumentOutOfRangeException(nameof(addAlbumToArtiste.AlbumId), addAlbumToArtiste.AlbumId, "Album Id cannot be lower than 1.");

            if (!_artisteRepository.ExistsById(addAlbumToArtiste.ArtisteId))
                throw new DataNotFoundException($"Artiste Id:{addAlbumToArtiste.ArtisteId} doesn't exists.");

            var result = await _artisteRepository.AddAlbumToArtiste(addAlbumToArtiste.ArtisteId, addAlbumToArtiste.AlbumId);

            return result;
        }

        public byte[] GetPicture(int id)
        {
            if (id < 1)
                throw new ArgumentOutOfRangeException(nameof(id), id, "Id cannot be lower than 1.");

            var picture = _artisteRepository.GetPicture(id);

            return picture;
        }

        public void SetPicture(int id, byte[] image)
        {
            if (id < 1)
                throw new ArgumentOutOfRangeException(nameof(id), id, "Id cannot be lower than 1.");

            if (image == null)
                throw new ArgumentNullException(nameof(image));
            
            _artisteRepository.SetPicture(id, image);
        }
    }
}