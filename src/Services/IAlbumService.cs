using System.Collections.Generic;
using System.Threading.Tasks;
using Epsic.Gestion_artistes.Rpg.Models;

namespace Epsic.Gestion_artistes.Rpg.Services
{
    public interface IAlbumService
    {
        AlbumDetailViewModel GetSingle(int id);
        Album Update(int id, UpdateAlbumDto updateAlbumDto);
        Task<bool> AddMusicToAlbum(AddMusicToAlbumDto addMusicToAlbumDto);
        Album Create(CreateAlbumDto createAlbumDto);
        Task<bool> Delete(int id);
        void SetPicture(int id, byte[] image);
        byte[] GetPicture(int id);
    }
}