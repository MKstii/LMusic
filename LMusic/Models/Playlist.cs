namespace LMusic.Models
{
    public class Playlist
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string Description { get; set; }
        public int UserId { get; set; }
        public User User { get; set; }
        public int PictureId { get; set; }
        public Picture Picture { get; set; }
        public List<PlaylistMusic> Musics { get; set; }
    }
}
