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
    public class AlbumService : IAlbumService
    {
        private readonly IAlbumRepository _albumRepository;
        
        public AlbumService(IAlbumRepository albumRepository)
        {
            _albumRepository = albumRepository;
        }

        public AlbumDetailViewModel GetSingle(int id)
        {
            if (id < 1)
                throw new ArgumentOutOfRangeException(nameof(id), id, "Id cannot be lower than 1.");

            var albumDb = _albumRepository.GetSingle(id);

            return albumDb;
        }

        public Album Update(int id, UpdateAlbumDto updateAlbumDto)
        {
            if (id < 1)
                throw new ArgumentOutOfRangeException(nameof(id), id, "Id cannot be lower than 1.");

            if (updateAlbumDto == null)
                throw new ArgumentNullException(nameof(updateAlbumDto));

            if (updateAlbumDto.Name.Length > 50)
                throw new ArgumentOutOfRangeException(nameof(updateAlbumDto.Name), updateAlbumDto.Name,
                    "Album name length cannot be greater than 50.");

            if (!_albumRepository.ExistsById(id))
                throw new DataNotFoundException($"Album Id:{id} doesn't exists");

            if (!_albumRepository.ExistsByName(updateAlbumDto.Name))
                throw new ArgumentException(nameof(updateAlbumDto.Name), $"Album {updateAlbumDto.Name} already exists");

            return _albumRepository.Update(id, updateAlbumDto);
        }

        public Album Create(CreateAlbumDto createAlbumDto)
        {
            if (createAlbumDto == null)
                throw new ArgumentNullException(nameof(createAlbumDto));

            if (createAlbumDto.Name.Length > 50)
                throw new ArgumentOutOfRangeException(nameof(createAlbumDto.Name), createAlbumDto.Name,
                    "Album name lenght cannot be greater than 50");

            if (_albumRepository.ExistsByName(createAlbumDto.Name))
                throw new ArgumentException(nameof(createAlbumDto.Name), $"Album {createAlbumDto.Name} already exists");

            var createAlbumDb = _albumRepository.Create(createAlbumDto);

            return createAlbumDb;
        }

        public async Task<bool> Delete(int id)
        {
            if (id < 1)
                throw new ArgumentOutOfRangeException(nameof(id), id, "Id cannot be lower than 1.");

            var result = await _albumRepository.Delete(id);

            if (result == 1)
                return true;
            else
            {
                return false;
            }
        }

        public async Task<bool> AddMusicToAlbum(AddMusicToAlbumDto addMusicToAlbumDto)
        {
            if (addMusicToAlbumDto == null)
                throw new ArgumentNullException(nameof(addMusicToAlbumDto));

            if (addMusicToAlbumDto.AlbumId < 1)
                throw new ArgumentOutOfRangeException(nameof(addMusicToAlbumDto.AlbumId), addMusicToAlbumDto.AlbumId, "Album Id cannot be lower than 1.");

            if (addMusicToAlbumDto.MusicId < 1)
                throw new ArgumentOutOfRangeException(nameof(addMusicToAlbumDto.MusicId), addMusicToAlbumDto.MusicId, "Music Id cannot be lower than 1.");

            if (!_albumRepository.ExistsById(addMusicToAlbumDto.AlbumId))
                throw new DataNotFoundException($"Album Id:{addMusicToAlbumDto.AlbumId} doesn't exists.");

            var result = await _albumRepository.AddMusicToAlbum(addMusicToAlbumDto.AlbumId, addMusicToAlbumDto.MusicId);

            if (result == 1)
                return true;
            else
                return false;
        }

        public byte[] GetPicture(int id)
        {
            if (id < 1)
                throw new ArgumentOutOfRangeException(nameof(id), id, "Id cannot be lower than 1.");

            var picture = _albumRepository.GetPicture(id);

            return picture;
        }

        public void SetPicture(int id, byte[] image)
        {
            if (id < 1)
                throw new ArgumentOutOfRangeException(nameof(id), id, "Id cannot be lower than 1.");

            if (image == null)
                throw new ArgumentNullException(nameof(image));

            _albumRepository.SetPicture(id, image);
        }
    }
}