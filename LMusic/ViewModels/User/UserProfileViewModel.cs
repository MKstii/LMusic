using LMusic.Models;

namespace LMusic.ViewModels.User
{
    public class UserProfileViewModel
    {
        public string UserName { get; set; }
        public double? FreeSpace { get; set; }
        public string PhotoPath { get; set; }
        public string TgId { get; set; }
        public List<PlaylistViewmodel> Playlists { get; set; }
        public List<MusicViewmodel> FavoriteMusic { get; set; }
        public Privacy Privacy { get; set; }
        public UserAccess UserProfileAccess { get; set; }
    }


}
