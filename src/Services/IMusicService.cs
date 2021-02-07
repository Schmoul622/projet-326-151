using System.Collections.Generic;
using System.Threading.Tasks;
using Epsic.Gestion_artistes.Rpg.Models;

namespace Epsic.Gestion_artistes.Rpg.Services
{
    public interface IMusicService
    {
        MusicDetailViewModel GetSingle(int id);
        Music Update(int id, UpdateMusicDto updateMusicDto);
        Music Create(CreateMusicDto createMusicDto);
        Task<bool> Delete(int id);
    }
}