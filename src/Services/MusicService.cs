using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Epsic.Gestion_artistes.Rpg.Exceptions;
using Epsic.Gestion_artistes.Rpg.Models;
using Epsic.Gestion_artistes.Rpg.Repositories;

namespace Epsic.Gestion_artistes.Rpg.Services
{
    public class MusicService : IMusicService
    {
        private readonly IMusicRepository _musicRepository;

        public MusicService(IMusicRepository musicRepository)
        {
            _musicRepository = musicRepository;
        }

        public MusicDetailViewModel GetSingle(int id)
        {
            if (id < 1)
                throw new ArgumentOutOfRangeException(nameof(id), id, "Id cannot be lower than 1.");
            
            var musicDb = _musicRepository.GetSingle(id);

            return musicDb;
        }

        public Music Update(int id, UpdateMusicDto updateMusicDto)
        {
            if (id < 1)
                throw new ArgumentOutOfRangeException(nameof(id), id, "Id cannot be lower than 1.");

            if (updateMusicDto == null)
                throw new ArgumentNullException(nameof(updateMusicDto));

            if (updateMusicDto.Name.Length > 50)
                throw new ArgumentOutOfRangeException(nameof(updateMusicDto.Name), updateMusicDto.Name,
                    "Music name length cannot be greater than 50.");

            if (!_musicRepository.ExistsById(id))
                throw new DataNotFoundException($"Music Id:{id} doesn't exists");

            return _musicRepository.Update(id, updateMusicDto);
        }
        
        public Music Create(CreateMusicDto createMusicDto)
        {
            if (createMusicDto == null)
                throw new ArgumentNullException(nameof(createMusicDto));

            if (createMusicDto.Name.Length > 50)
                throw new ArgumentOutOfRangeException(nameof(createMusicDto.Name), createMusicDto.Name,
                    "Album name lenght cannot be greater than 50");

            var createMusicDb = _musicRepository.Create(createMusicDto);

            return createMusicDb;
        }
        
        public async Task<bool> Delete(int id)
        {
            if (id < 1)
                throw new ArgumentOutOfRangeException(nameof(id), id, "Id cannot be lower than 1.");

            var result = await _musicRepository.Delete(id);

            if (result == 1)
                return true;
            else
            {
                return false;
            }
        }
    }
}