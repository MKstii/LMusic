using LMusic.Models;
using LMusic.Registries;
using Microsoft.EntityFrameworkCore;

namespace LMusic.Services
{
    public abstract class DbServiceAbstract<Model> where Model : class, IEntity
    {
        protected Registry<Model> _registry;

        public DbServiceAbstract(Registry<Model> registry) 
        {
            _registry = registry;
        }
        virtual public void Add(Model entity)
        {
            _registry.Add(entity);
        }
        virtual public void Add(IEnumerable<Model> entity)
        {
            _registry.Add(entity);
        }

        virtual public void Update(Model entity)
        {
            _registry.Update(entity);
        }
        virtual public void Delete(Model entity)
        {
            _registry.Delete(entity);
        }
        virtual public Model Find(int id)
        {
            return _registry.Find(id);
        }
    }
}
