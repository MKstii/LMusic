using LMusic.Services;
using System.Net;

namespace LMusic.Models
{
    public class Playlist : IEntity
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public List<PlaylistUser> PlaylistUser { get; set; }
        public int PictureId { get; set; }
        public Picture Picture { get; set; }
        public List<PlaylistMusic> Musics { get; set; }
        public Privacy Privacy { get; set; }
        public bool IsDefault { get; set; }

        public Playlist() { }

        public Playlist(int id, string name, int playlistUserId, int pictureId, Privacy privacy)
        {
            Id = id;
            Name = name;
            PictureId = pictureId;
            Privacy = privacy;
        }

        public static Playlist CreateDefault(Picture pict)
        {
            var result = new Playlist();
            result.Name = "Избранное";
            result.PictureId = pict.Id;
            result.Privacy = Privacy.ForAll;
            result.IsDefault = true;
            return result;
        }

        public int GetId()
        {
            return Id;
        }
    }
}
