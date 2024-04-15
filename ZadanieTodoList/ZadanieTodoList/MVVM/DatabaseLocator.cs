using ZadanieTodoList.Database;

namespace ZadanieTodoList.MVVM
{
    public class DatabaseLocator
    {
        public static TodoListDbContext Database { get; set; }
    }
}
