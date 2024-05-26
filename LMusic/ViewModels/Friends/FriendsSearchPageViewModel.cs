namespace LMusic.ViewModels.Friends
{
    public class FriendsSearchPageViewModel
    {
        public List<UserViewmodel_FriendsPage> Friends { get; set; }
        public List<UserViewmodel_FriendsPage> Users { get; set; }
        public int Page { get; set; }
        public int Limit { get; set; }

    }
}
