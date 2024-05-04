using LMusic.Models;
using LMusic.Registries;

namespace LMusic.Services
{
    public class MusicService : DbServiceAbstract<Music>
    {
        private MusicRegistry _musicRegistry;
        public MusicService() : base(new MusicRegistry())
        {
            _musicRegistry = (MusicRegistry)_registry;
        }

        public string CreatePath(User user)
        {
            return $"/audio/{user.TelegramId}";
        }
    }
}
