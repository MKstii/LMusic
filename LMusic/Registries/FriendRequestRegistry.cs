using LMusic.Models;
using System.Runtime.Intrinsics.X86;

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

        public FriendRequest? GetRequest(User requester, User addressee)
        {
            using (var db = new ContextDataBase())
            {
                var request = db.FriendRequests
                    .Where(x => x.RequesterId == requester.Id && x.AddresseeId == addressee.Id).FirstOrDefault();
                return request;
            }
        }
    }
}
