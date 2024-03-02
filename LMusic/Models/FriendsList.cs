namespace LMusic.Models
{
    public class FriendsList : IEntity
    {
        public int Id { get; set; }
        public int UserId { get; set; }
        public User User { get; set; }
        public int FriendId {  get; set; }
        public User Friend { get; set; }

        public FriendsList() { }

        public FriendsList(int id, int userId, int friendId)
        {
            Id = id;
            UserId = userId;
            FriendId = friendId;
        }

        public int GetId()
        {
            return Id;
        }
    }

}
