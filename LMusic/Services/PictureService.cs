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
    }
}
