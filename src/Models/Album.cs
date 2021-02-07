using System.Collections.Generic;

namespace Epsic.Gestion_artistes.Rpg.Models
{
    public class Album
    {
        public Album()
        {
            Musics = new List<Music>();
        }
        public int Id { get; set; }
        public byte[] Picture { get; set; }
        public string Name { get; set; }
        public int NumberTitles { set; get; }
        public int ReleaseDate { get; set; }
        public string Duration { get; set; }
        public List<Music> Musics { get; set; }

        public int? ArtisteId { get; set; }
        public Artiste Artiste { get; set; }
    }

    public class AlbumSummaryViewModel
    {
        public int Id { get; set; }
        public byte[] Picture { get; set; }
        public string Name { get; set; }
    }

    public class AlbumPatchViewModel
    {
        public byte[] Picture { get; set; }
        public string Name { get; set; } 
        public int NumberTitles { set; get; }
        public int ReleaseDate { get; set; }
        public string Duration { get; set; }
    }

    public class AlbumDetailViewModel
    {
        public int Id { get; set; }
        public byte[] Picture { get; set; }
        public string Name { get; set; }
        public int NumberTitles { set; get; }
        public int ReleaseDate { get; set; }
        public string Duration { get; set; }
        public IEnumerable<MusicSummaryViewModel> Musics { get; set; }
    }

    public class UpdateAlbumDto
    {
        public byte[] Picture { get; set; }
        public string Name { get; set; }
        public int NumberTitles { get; set; }
        public int ReleaseDate { get; set; }
        public string Duration { get; set; }
    }

    public class CreateAlbumDto
    {
        public byte[] Picture { get; set; }
        public string Name { get; set; }
        public int NumberTitles { get; set; }
        public int ReleaseDate { get; set; }
        public string Duration { get; set; }
        public int? ArtisteId { get; set; }
    }

    public class AddMusicToAlbumDto
    {
        public int AlbumId { get; set; }
        public int MusicId { get; set; }
    }
}