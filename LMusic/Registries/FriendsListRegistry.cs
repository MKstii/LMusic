using LMusic.Models;

namespace LMusic.Registries
{
    public class FriendsListRegistry : Registry<FriendsList>
    {
        public FriendsList? GetFriendsList(User user, User friend)
        {
            using (var db = new ContextDataBase())
            {
                var request = db.FriendsLists.Where(x => x.UserId == user.Id && x.FriendId == friend.Id).FirstOrDefault();
                return request;
            }
        }
        public FriendsList? GetFriendsListMirror(FriendsList friendsList)
        {
            using (var db = new ContextDataBase())
            {
                var request = db.FriendsLists.Where(x => x.UserId == friendsList.FriendId && x.FriendId == friendsList.UserId).FirstOrDefault();
                return request;
            }
        }
    }
}
