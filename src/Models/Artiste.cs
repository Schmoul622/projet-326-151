using System;
using System.Collections.Generic;

namespace Epsic.Gestion_artistes.Rpg.Models
{
    public class Artiste
    {
        public Artiste()
        {
            Albums = new List<Album>();
        }
        
        public int Id { get; set; }
        public byte[] Picture { get; set; }
        public string Speudo { get; set; }
        public string FirstName { get; set; }
        public string Name { get; set; }
        public int Age { get; set; }
        public int CarrierStart { get; set; }
        public List<Album> Albums { get; set; }
    }

    public class ArtisteSummaryViewModel
    {
        public int Id { get; set; }
        public byte[] Picture { get; set; }
        public string Speudo { get; set; }
    }

    public class ArtisteDetailViewModel
    {
        public int Id { get; set; }
        public byte[] Picture { get; set; }
        public string Speudo { get; set; }
        public string FirstName { get; set; }
        public string Name { get; set; }
        public int Age { get; set; }
        public int CarrierStart { get; set; }
        public IEnumerable<AlbumSummaryViewModel> Albums { get; set; } 
    }

    public class UpdateArtisteDto
    {
        public byte[] Picture { get; set; }
        public string Speudo { get; set; }
        public string FirstName { get; set; }
        public string Name { get; set; }
        public int Age { get; set; }
        public int CarrierStart { get; set; }
    }

    public class CreateArtisteDto
    {
        public byte[] Picture { get; set; }
        public string Speudo { get; set; }
        public string FirstName { get; set; }
        public string Name { get; set; }
        public int Age { get; set; }
        public int CarrierStart { get; set; }
    }

    public class AddAlbumToArtisteDto
    {
        public int ArtisteId { get; set; }
        public int AlbumId { get; set; }
    }
}