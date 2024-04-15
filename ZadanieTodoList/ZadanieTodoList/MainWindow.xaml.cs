using System.Windows;
using System.Windows.Input;
using ZadanieTodoList.ViewModel;

namespace ZadanieTodoList
{
    public partial class MainWindow : Window
    {
        MainWindowViewModel vm;
        public MainWindow()
        {
            InitializeComponent();
            vm = new MainWindowViewModel(datePicker, editionPanel);
            DataContext = vm;
        }

        private void txtBox_LostFocus(object sender, RoutedEventArgs e)
        {
            vm.UpdateCommand.Execute(null);
        }

        private void txtBox_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.Key == Key.Return)
            {
                vm.UpdateCommand.Execute(null);
                Keyboard.ClearFocus();
            }
        }

        private void editionDatePicker_CalendarClosed(object sender, RoutedEventArgs e)
        {
            vm.UpdateCommand.Execute(null);
        }

        private void editionDatePicker_LostFocus(object sender, RoutedEventArgs e)
        {
            vm.UpdateCommand.Execute(null);
        }
    }
}
