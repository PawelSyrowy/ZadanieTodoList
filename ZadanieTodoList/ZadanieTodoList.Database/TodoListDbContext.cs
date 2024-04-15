using Microsoft.EntityFrameworkCore;
using ZadanieTodoList.Database.Entities;

namespace ZadanieTodoList.Database
{
    public class TodoListDbContext : DbContext
    {
        public DbSet<TaskDb> WorkTasks { get; set; }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            base.OnConfiguring(optionsBuilder);

            optionsBuilder.UseSqlite($"Filename={Path.Combine(Environment.GetFolderPath(Environment.SpecialFolder.MyDocuments), "toDoListApp.sqlite")}");
        }
    }
}
