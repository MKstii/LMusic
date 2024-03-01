namespace LMusic.Models
{
    public class FriendRequest : IEntity
    {
        public int Id { get; set; }
        public int RequesterId { get; set; }
        public User Requester { get; set; }
        public int AddresseeId { get; set; }
        public User Addressee { get; set; }
        public FriendRequest() { }

        public FriendRequest(int id, int requesterId, int addresseeId)
        {
            Id = id;
            RequesterId = requesterId;
            AddresseeId = addresseeId;
        }

        public int GetId()
        {
            return Id;
        }
    }
}
