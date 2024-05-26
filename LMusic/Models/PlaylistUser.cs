namespace LMusic.Models
{
    public class PlaylistUser : IEntity
    {
        public int Id { get; set; }
        public bool isDefault { get; set; }
        public int UserId { get; set; }
        public User User { get; set; }
        public int  PlaylistId { get; set; }
        public Playlist Playlist { get; set; }
        public bool IsCreater { get; set; }

        public int GetId()
        {
            return Id;
        }
    }
}
