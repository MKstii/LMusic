using LMusic.Models;
using Microsoft.EntityFrameworkCore;

namespace LMusic.Registries
{
    public class PlaylistUserRegistry : Registry<PlaylistUser>
    {
        public PlaylistUser GetByUserDefaultCreater(User user)
        {
            using (ContextDataBase db = new ContextDataBase())
            {
                DbSet<PlaylistUser> dbSet = db.Set<PlaylistUser>();
                var entity = dbSet.Where(i => i.UserId == user.Id && i.IsCreater == true && i.isDefault).FirstOrDefault();
                return entity;
            }
        }

        public List<PlaylistUser> GetByUserCreater(User user)
        {
            using (ContextDataBase db = new ContextDataBase())
            {
                DbSet<PlaylistUser> dbSet = db.Set<PlaylistUser>();
                var entity = dbSet.Where(i => i.UserId == user.Id && i.IsCreater && !i.isDefault).ToList();
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

        public User? GetUserOwnerByPlaylistId (int id)
        {
            using (ContextDataBase db = new ContextDataBase())
            {
                DbSet<PlaylistUser> dbSet = db.Set<PlaylistUser>();
                var entity = dbSet.Where(x => x.IsCreater == true)
                            .Where(x => x.PlaylistId == id)
                            .Include(x => x.User)
                            .Select(x => x.User)
                            .FirstOrDefault();
                return entity;
            }
        }

        public PlaylistUser GetByPlaylistAndUser(Playlist playlist, User user)
        {
            using (ContextDataBase db = new ContextDataBase())
            {
                DbSet<PlaylistUser> dbSet = db.Set<PlaylistUser>();
                var entity = dbSet.Where(i => i.UserId == user.Id && i.PlaylistId == playlist.Id).FirstOrDefault();
                return entity;
            }
        }
    }
}
