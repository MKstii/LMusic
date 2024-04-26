using LMusic.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.Win32;

namespace LMusic.Registries
{
    public class UserRegistry : Registry<User>
    {
        public User GetUserByTgId(int id)
        {
            using (ContextDataBase db = new ContextDataBase())
            {
                DbSet<User> dbSet = db.Set<User>();
                var entity = dbSet.Where(x => x.TelegramId == id).FirstOrDefault();
                return entity;
            }
        }
    }
}
