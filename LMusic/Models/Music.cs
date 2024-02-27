namespace LMusic.Models
{
    public class Music : IEntity
    {
        public int Id { get; set; }
        public int UserId { get; set; }
        public User User { get; set; }
        public int PictureId {  get; set; }
        public Picture Picture { get; set; }
        public string Title { get; set; }
        public string Musician { get; set; }
        public string Path { get; set; }
        public int Duration { get; set; }
        public double Weight { get; set; }
        public string AlbumName { get; set; }
        public bool IsDelited { get; set; }
        public List<PlaylistMusic> Playlists { get; set; }

        public int GetId()
        {
            return Id;
        }
    }
}
