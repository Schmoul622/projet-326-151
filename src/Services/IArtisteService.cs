using System.Collections.Generic;
using System.Threading.Tasks;
using Epsic.Gestion_artistes.Rpg.Models;

namespace Epsic.Gestion_artistes.Rpg.Services
{
    public interface IArtisteService
    {
        ArtisteDetailViewModel GetSingle(int id);
        List<ArtisteDetailViewModel> GetAllArtiste();
        Artiste Update(int id, UpdateArtisteDto artisteToUpdate);
        Task<int> AddAlbumToArtiste(AddAlbumToArtisteDto addAlbumToArtisteDto);
        Artiste Create(CreateArtisteDto artisteToCreate);
        Task<bool> Delete(int id);
        void SetPicture(int id, byte[] image);
        byte[] GetPicture(int id);
    }
}