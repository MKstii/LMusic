using LMusic.Models;
using LMusic.Registries;

namespace LMusic.Services
{
    public class FriendService
    {
        private FriendRequestRegistry _friendRequestRegistry;
        private FriendsListRegistry _friendsListRegistry;

        public FriendService()
        {
            _friendRequestRegistry = new FriendRequestRegistry();
            _friendsListRegistry = new FriendsListRegistry();
        }

        public void AddRequest(User requester, User addressee)
        {
            var request = new FriendRequest();
            request.RequesterId = requester.Id;
            request.AddresseeId = addressee.Id;

            _friendRequestRegistry.Add(request);
        }

        //public bool HasRequest(User requester, User addressee)
        //{

        //}
    }
}
