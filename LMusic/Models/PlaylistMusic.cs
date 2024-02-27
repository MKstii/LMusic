namespace LMusic.Models
{
    public class PlaylistMusic : IEntity
    {
        public int Id { get; set; }
        public int PlaylistId { get; set; }
        public Playlist Playlist { get; set; }
        public int MusicId { get; set; }
        public Music Music { get; set; }

        public int GetId()
        {
            return Id;
        }
    }
}
