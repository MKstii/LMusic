using LMusic.Models;
using Microsoft.EntityFrameworkCore;

namespace LMusic.Registries
{
    public abstract class Registry<Model> where Model : class, IEntity
    {
        //private ContextDataBase db = new ContextDataBase();
        virtual public void Add(Model entity)
        {
            using (ContextDataBase db = new ContextDataBase())
            {
                db.Add(entity);
                db.SaveChanges();
            }
        }
        virtual public void Add(IEnumerable<Model> entity)
        {
            using (ContextDataBase db = new ContextDataBase())
            {
                db.AddRange(entity);
                db.SaveChanges();
            }
        }

        virtual public void Update(Model entity)
        {
            using (ContextDataBase db = new ContextDataBase())
            {
                db.Update(entity);
                db.SaveChanges();
            }
        }
        virtual public void Delete(Model entity)
        {
            using (ContextDataBase db = new ContextDataBase())
            {
                db.Remove(entity);
                db.SaveChanges();
            }
        }
        virtual public Model Find(int id)
        {
            using (ContextDataBase db = new ContextDataBase())
            {
                DbSet<Model> dbSet = db.Set<Model>();
                var entity = dbSet.Find(id);
                return entity;
            }
        }
    }
}
