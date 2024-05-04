using LMusic.Services;
using System.Net;

namespace LMusic.Models
{
    public class Playlist : IEntity
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public int UserId { get; set; }
        public User User { get; set; }
        public int PictureId { get; set; }
        public Picture Picture { get; set; }
        public List<PlaylistMusic> Musics { get; set; }
        public Privacy Privacy { get; set; }
        public bool IsDefault { get; set; }

        public Playlist() { }

        public Playlist(int id, string name, int userId, int pictureId, Privacy privacy)
        {
            Id = id;
            Name = name;
            UserId = userId;
            PictureId = pictureId;
            Privacy = privacy;
        }

        public Playlist(User user)
        {
            Name = "Любимое";
            UserId = user.Id;
            PictureId = new PictureService().GetDefaulPlaylistPicture().Id;
            Privacy = Privacy.ForAll;
            IsDefault = true;
        }

        public int GetId()
        {
            return Id;
        }
    }
}
