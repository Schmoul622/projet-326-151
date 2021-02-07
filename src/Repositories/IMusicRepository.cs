using System.Collections.Generic;
using System.Threading.Tasks;
using Epsic.Gestion_artistes.Rpg.Models;

namespace Epsic.Gestion_artistes.Rpg.Repositories
{
    public interface IMusicRepository
    {
        MusicDetailViewModel GetSingle(int id);
        Music Update(int id, UpdateMusicDto updateMusicDto);
        Music Create(CreateMusicDto createMusicDto);
        Task<int> Delete(int id);
        bool ExistsById(int id);
    }
}