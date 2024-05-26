using LMusic.Models;
using LMusic.Registries;

namespace LMusic.Services
{
    public class PictureService : DbServiceAbstract<Picture>
    {
        private PictureRegistry _pictureRegistry;
        public PictureService() : base(new PictureRegistry())
        {
            _pictureRegistry = (PictureRegistry)_registry;
        }
        public string CreatePath(Picture pic, User user)
        {
            return $"/pictures/{pic.Type}/{user.TelegramId}";
        }
        public Picture? GetDefaulAvatarPicture()
        {
            return _pictureRegistry.GetDefaultPicture(PictureType.Avatar);
        }
        public Picture? GetDefaulPlaylistPicture()
        {
            return _pictureRegistry.GetDefaultPicture(PictureType.Playlist);
        }
        public Picture? GetDefaulMusicPicture()
        {
            return _pictureRegistry.GetDefaultPicture(PictureType.Music);
        }

        public Picture CreatePicture(User user, IFormFile pictureFile, PictureType type, string webRootPath)
        {
            var pic = new Picture();
            pic.FileName = pictureFile.FileName;
            pic.Type = type;
            pic.IsDefault = false;
            pic.IsDeleted = false;
            pic.Path = CreatePath(pic, user);
            Directory.CreateDirectory(webRootPath + pic.Path);
            using (var fileStream = new FileStream(webRootPath + pic.GetFullPath(), FileMode.Create))
            {
                pictureFile.CopyTo(fileStream);
            }
            Add(pic);
            return pic;
        }

        public Picture GetPicture(int id) 
        {
            return _pictureRegistry.Find(id);
        }

        public Picture GetUserAvatar(User user)
        {
            return _pictureRegistry.GetUserAvatar(user);
        }

        public Picture GetPlaylistAvatar(Playlist playlist)
        {
            return _pictureRegistry.GetPlaylistAvatar(playlist);
        }

        public Picture GetMusicAvatar(Music music)
        {
            return _pictureRegistry.GetMusicAvatar(music);
        }
    }
}
