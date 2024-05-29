using LMusic.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure.Internal;

namespace LMusic.Registries
{
    public class MusicRegistry : Registry<Music>
    {
        public Music? FindWithIncludeUser(int id)
        {
            using (ContextDataBase db = new ContextDataBase())
            {
                DbSet<Music> dbSet = db.Set<Music>();
                var entity = dbSet.Where(x => x.Id == id).Include(x => x.User).FirstOrDefault();
                return entity;
            }
        }

        public void DeleteCascade(Music music)
        {
            using (ContextDataBase db = new ContextDataBase())
            {
                DbSet<Music> dbSet = db.Set<Music>();
                db. (music);
                db.SaveChanges();
                return entity;
            }
        }
    }
}
