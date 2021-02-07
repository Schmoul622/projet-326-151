using System.Collections.Generic;
using System.Threading.Tasks;
using Epsic.Gestion_artistes.Rpg.Models;

namespace Epsic.Gestion_artistes.Rpg.Repositories
{
    public interface IAlbumRepository
    {
        AlbumDetailViewModel GetSingle(int id);
        Album Update(int id, UpdateAlbumDto updateAlbumDto);
        Task<int> AddMusicToAlbum(int albumId, int musicId);
        Album Create(CreateAlbumDto createAlbumDto);
        Task<int> Delete(int id);
        bool ExistsById(int id);
        bool ExistsByName(string name);
        void SetPicture(int id, byte[] image);
        byte[] GetPicture(int id);
    }
}