using LMusic.Models;
using Microsoft.EntityFrameworkCore;

namespace LMusic.Registries
{
    public class PlaylistUserRegistry : Registry<PlaylistUser>
    {
        public PlaylistUser GetByUserCreater(User user)
        {
            using (ContextDataBase db = new ContextDataBase())
            {
                DbSet<PlaylistUser> dbSet = db.Set<PlaylistUser>();
                var entity = dbSet.Where(i => i.UserId == user.Id && i.IsCreater == true && i.isDefault).FirstOrDefault();
                return entity;
            }
        }

        public List<PlaylistUser> GetByUser(User user)
        {
            using (ContextDataBase db = new ContextDataBase())
            {
                DbSet<PlaylistUser> dbSet = db.Set<PlaylistUser>();
                var entity = dbSet.Where(i => i.UserId == user.Id).ToList();
                return entity;
            }
        }
    }
}
