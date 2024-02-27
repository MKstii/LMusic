namespace LMusic.Models
{
    public class FriendsList : IEntity
    {
        public int Id { get; set; }
        public int UserId { get; set; }
        public User User { get; set; }
        public int FriendId {  get; set; }
        public User Friend { get; set; }
    
        public int GetId()
        {
            return Id;
        }
    }

}
