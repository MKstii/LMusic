using LMusic.Models;

namespace LMusic.Registries
{
    public class FriendRequestRegistry : Registry<FriendRequest>
    {
        public FriendRequest? GetRequestByMembers(User user1, User user2)
        {
            using (var db = new ContextDataBase())
            {
                return db.FriendRequests
                    .Where(x => (x.AddresseeId == user1.Id && x.RequesterId == user2.Id)
                            || (x.AddresseeId == user2.Id && x.RequesterId == user1.Id)).FirstOrDefault();
            }
        }
    }
}
