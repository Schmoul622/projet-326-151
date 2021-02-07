using System.Collections.Generic;
using System.Threading.Tasks;
using Epsic.Gestion_artistes.Rpg.Models;

namespace Epsic.Gestion_artistes.Rpg.Repositories
{
    public interface IArtisteRepository
    {
        ArtisteDetailViewModel GetSingle(int id);
        List<ArtisteDetailViewModel> GetAllArtiste();
        Artiste Update(int id, UpdateArtisteDto artisteToUpdate);
        Task<int> AddAlbumToArtiste(int artisteId, int albumId);
        Artiste Create(CreateArtisteDto artisteToCreate);
        Task<int> Delete(int id);
        bool ExistsById(int id);
        bool ExistsBySpeudo(string speudo);
        void SetPicture(int id, byte[] image);
        byte[] GetPicture(int id);
    }
}