using LMusic.Models;

namespace LMusic.Registries
{
    public class PictureRegistry : Registry<Picture>
    {
        public Picture? GetDefaultPicture(PictureType type)
        {
            using (var db = new ContextDataBase())
            {
                var pic = db.Pictures.Where(x => x.IsDefault == true).Where(x => x.Type == type).FirstOrDefault();
                return pic;
            }
        }

        public Picture GetUserAvatar(User user)
        {
            using (var db = new ContextDataBase())
            {
                var pic = db.Pictures.Where(x => x.Id == user.PictureId).FirstOrDefault();
                return pic;
            }
        }

        public Picture GetPlaylistAvatar(Playlist playlist)
        {
            using (var db = new ContextDataBase())
            {
                var pic = db.Pictures.Where(x => x.Id == playlist.PictureId).FirstOrDefault();
                return pic;
            }
        }

        public Picture GetMusicAvatar(Music music)
        {
            using (var db = new ContextDataBase())
            {
                var pic = db.Pictures.Where(x => x.Id == music.PictureId).FirstOrDefault();
                return pic;
            }
        }
    }
}
