using Microsoft.EntityFrameworkCore;

namespace LMusic
{
    public class ContextDataBase : DbContext
    {
        public ContextDataBase() { }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            optionsBuilder.UseInMemoryDatabase("LMusic");
            // Additional.DatabaseData будут объекты, которые надо будет засунуть в базу
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            // Настройка моделей. Тут надо будет настроить 2 юзера
        }
    }
}
