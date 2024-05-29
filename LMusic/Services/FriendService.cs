using LMusic.Models;
using LMusic.Registries;
using LMusic.ViewModels.Friends;
using System.Runtime.CompilerServices;

namespace LMusic.Services
{
    public class FriendService
    {
        private FriendRequestRegistry _friendRequestRegistry;
        private FriendsListRegistry _friendsListRegistry;
        private PictureService _pictureService = new PictureService();

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

        public UserViewmodel_FriendsPage GetUserViewmode_FriendsPage(User user)
        {
            var userViewmodel = new UserViewmodel_FriendsPage();
            userViewmodel.Id = user.TelegramId;
            userViewmodel.Name = user.UserName;
            userViewmodel.PicPath = _pictureService.GetPicture(user.PictureId).GetFullPath();
            return userViewmodel;
        }

        public bool HasRequest(User requester, User addressee)
        {
            return GetRequest(requester, addressee) != null;
        }

        public FriendRequest? GetRequest(User requester, User addressee)
        {
            return _friendRequestRegistry.GetRequest(requester, addressee);
        }

        public void DeleteRequest(FriendRequest request)
        {
            _friendRequestRegistry.Delete(request);
        }

        public void DenyRequest(FriendRequest request)
        {
            DeleteRequest(request);
        }
        public void AcceptRequest(FriendRequest request)
        {
            var requester = CreateFriendList(request.RequesterId, request.AddresseeId);
            var addresser = CreateFriendList(request.AddresseeId, request.RequesterId);

            _friendsListRegistry.Add([requester, addresser]);
        }

        private FriendsList CreateFriendList(int userId, int friendId)
        {
            var friendList = new FriendsList();
            friendList.UserId = userId;
            friendList.FriendId = friendId;
            return friendList;
        }

        public List<FriendRequest> GetRequestByAddresser(User user, string filter, int page, int limit)
        {
            return _friendRequestRegistry.GetRequestByAddresse(user, filter, page, limit);
        }

        public FriendsList? GetFriendList(User user, User friend)
        {
            return _friendsListRegistry.GetFriendsList(user, friend);
        }

        public void DeleteFriendList(FriendsList friendList)
        {
            var mirrorFriendList = _friendsListRegistry.GetFriendsListMirror(friendList);
            _friendsListRegistry.Delete(mirrorFriendList);
            _friendsListRegistry.Delete(friendList);
        }

        public bool IsFriends(User user, User friend)
        {
            var friendList = _friendsListRegistry.GetFriendsList(user, friend);
            return friendList != null;
        }
    }
}
