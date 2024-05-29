using LMusic.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.Win32;

namespace LMusic.Registries
{
    public class UserRegistry : Registry<User>
    {
        public User? GetUserByTgId(string id)
        {
            using (ContextDataBase db = new ContextDataBase())
            {
                DbSet<User> dbSet = db.Set<User>();
                var entity = dbSet.Where(x => x.TelegramId == id).FirstOrDefault();
                return entity;
            }
        }

        public List<User> GetUsersByIds(int[] ids)
        {
            using (ContextDataBase db = new ContextDataBase())
            {
                DbSet<User> dbSet = db.Set<User>();
                var entity = dbSet.Where(x => ids.Contains(x.Id)).ToList();
                return entity;
            }
        }

        public (List<User>?, int) GetUsers(string filter, int page, int limit)
        {
            using (ContextDataBase db = new ContextDataBase())
            {
                
                DbSet<User> dbSet = db.Set<User>();
                var users = dbSet.Where(x => x.UserName.ToLower().Contains(filter));
                var total = users.Count();
                var resUsers = users.Skip((page-1)*limit)
                            .Take(limit)
                            .ToList();
                return (resUsers, total);
            }
        }

        public List<User>? GetFriends(User user, string filter, int page, int limit)
        {
            using (ContextDataBase db = new ContextDataBase())
            {
                var friendsIds = db.FriendsLists.Where(x => x.UserId == user.Id).Select(x => x.FriendId);
                var users = db.Users.Where(x => friendsIds.Contains(x.Id))
                    .Skip((page-1)*limit)
                    .Take(limit)
                    .ToList();
                return users;
            }
        }
    }
}
