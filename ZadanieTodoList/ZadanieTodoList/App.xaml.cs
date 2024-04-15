using System.Windows;
using ZadanieTodoList.Database;
using ZadanieTodoList.MVVM;

namespace ZadanieTodoList
{
    /// <summary>
    /// Interaction logic for App.xaml
    /// </summary>
    public partial class App : Application
    {
        protected override void OnStartup(StartupEventArgs e)
        {
            base.OnStartup(e);

            var database = new TodoListDbContext();

            database.Database.EnsureCreated();

            DatabaseLocator.Database = database;

            var culture = "en-EN";
            Thread.CurrentThread.CurrentCulture = new System.Globalization.CultureInfo(culture);
            Thread.CurrentThread.CurrentUICulture = new System.Globalization.CultureInfo(culture);
        }
    }

}
