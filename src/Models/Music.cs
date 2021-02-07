namespace Epsic.Gestion_artistes.Rpg.Models
{
    public class Music
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string Duration { get; set; }
        
        public int? AlbumId { get; set; }
        public Album Album { get; set; }
    }

    public class MusicSummaryViewModel
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string Duration { get; set; }
    }

    public class MusicDetailViewModel
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string Duration { get; set; }
    }

    public class UpdateMusicDto
    {
        public string Name { get; set; }
        public string Duration { get; set; }
    }
    
    public class CreateMusicDto
    {
        public string Name { get; set; }
        public string Duration { get; set; }
        public int? AlbumId { get; set; }
    }
}