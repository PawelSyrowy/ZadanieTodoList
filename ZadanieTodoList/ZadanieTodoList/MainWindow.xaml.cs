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
            vm = new MainWindowViewModel(datePicker);
            DataContext = vm;
        }

        private void TextBox_LostFocus(object sender, RoutedEventArgs e)
        {
            vm.UpdateCommand.Execute(null);
        }

        private void DatePicker_SelectedDateChanged(object sender, System.Windows.Controls.SelectionChangedEventArgs e)
        {
            vm.UpdateCommand.Execute(null);
        }

        private void CheckBoxPriority_Click(object sender, RoutedEventArgs e)
        {
            vm.UpdateCommand.Execute(null);
        }

        private void CheckBoxCompleted_Click(object sender, RoutedEventArgs e)
        {
            vm.UpdateCommand.Execute(null);
        }
    }
}
