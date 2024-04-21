using System.Collections.ObjectModel;
using System.Windows.Controls;
using ZadanieTodoList.Database.Entities;
using ZadanieTodoList.MVVM;
using Task = ZadanieTodoList.Model.Task;

namespace ZadanieTodoList.ViewModel
{
    internal class MainWindowViewModel : ViewModelBase
    {
        private DatePicker datePicker;
        private bool showAll;
        private List<Task> Tasks { get; set; }
        public ObservableCollection<Task> ListItems { get; set; }

        public RelayCommand AddCommand => new RelayCommand(execute => AddTask());
        public RelayCommand DeleteCommand => new RelayCommand(execute => DeleteTask(), canExecute => lastSelectedItem != null);
        public RelayCommand UpdateCommand => new RelayCommand(execute => UpdateTask(), canExecute => SelectedItem != null);
        public RelayCommand ShowAllCommand => new RelayCommand(execute => ShowAllTasks());
        public RelayCommand PriorityCommand => new RelayCommand(execute => TogglePriority(), canExecute => SelectedItem != null);
        public RelayCommand CompletedCommand => new RelayCommand(execute => ToggleCompleted(), canExecute => SelectedItem != null);

        public MainWindowViewModel(DatePicker datePicker)
        {
            this.datePicker = datePicker;
            showAll = false;
            Tasks = new List<Task>();
            ListItems = new ObservableCollection<Task>();

            foreach (var task in DatabaseLocator.Database.WorkTasks.ToList())
            {
                Tasks.Add(MapToViewModel(task));
            }

            displayDay = DateTime.Today;
            RefreshItems();
        }

        private Task lastSelectedItem;
        private Task selectedItem;
        public Task SelectedItem
        {
            get { return selectedItem; }
            set 
            { 
                selectedItem = value;
                if (selectedItem != null)
                {
                    lastSelectedItem = selectedItem;
                }
                OnPropertyChanged();
            }
        }

        private DateTime? displayDay;
        public DateTime? DisplayDay
        {
            get { return displayDay; }
            set
            {
                displayDay = value;
                if (displayDay != null)
                {
                    showAll = false;
                    RefreshItems();
                }
                else
                {
                    showAll = true;
                    RefreshItems();
                }

            }
        }

        private void AddTask()
        {
            TaskDb taskDb = new TaskDb
            {
                Description = "New Task",
                Date = displayDay ?? DateTime.Today,
                Priority = false,
                Completed = false
            };

            DatabaseLocator.Database.WorkTasks.Add(taskDb);
            SaveDatabase();

            Tasks.Add(MapToViewModel(taskDb));
            RefreshItems();
        }

        private void DeleteTask()
        {
            var foundEntity = DatabaseLocator.Database.WorkTasks.FirstOrDefault(x => x.Id == lastSelectedItem.Id);
            if (foundEntity != null)
            {
                DatabaseLocator.Database.WorkTasks.Remove(foundEntity);
                SaveDatabase();
            }

            Tasks.Remove(lastSelectedItem);
            RefreshItems();
            lastSelectedItem = selectedItem;
        }

        private void UpdateTask()
        {
            if (selectedItem is null)
            {
                return;
            }

            var foundTask = Tasks.FirstOrDefault(x => x.Id == selectedItem.Id);
            if (foundTask != null)
            {
                foundTask = selectedItem;

                var foundEntity = DatabaseLocator.Database.WorkTasks.FirstOrDefault(x => x.Id == foundTask.Id);
                if (foundEntity != null)
                {
                    foundEntity.Description = foundTask.Description;
                    foundEntity.Date = foundTask.Date;
                    foundEntity.Priority = foundTask.Priority;
                    foundEntity.Completed = foundTask.Completed;
                    SaveDatabase();
                }
            }

            RefreshItems();
        }

        private void ShowAllTasks()
        {
            datePicker.SelectedDate = null;
        }

        private void TogglePriority()
        {
            selectedItem.Priority = !selectedItem.Priority;
            UpdateTask();
        }

        private void ToggleCompleted()
        {
            selectedItem.Completed = !selectedItem.Completed;
            UpdateTask();
        }

        private void RefreshItems()
        {
            ListItems.Clear();
            LoadItems();
        }

        private void LoadItems()
        {
            if (showAll)
            {
                foreach (Task task in Tasks)
                {
                    ListItems.Add(task);
                }
            }
            else
            {
                foreach (Task task in Tasks)
                {
                    if (task.Date == displayDay)
                    {
                        ListItems.Add(task);
                    }
                }
            }
        }

        private Task MapToViewModel(TaskDb taskDb)
        {
            return new Task
            {
                Id = taskDb.Id,
                Description = taskDb.Description,
                Date = taskDb.Date,
                Priority = taskDb.Priority,
                Completed = taskDb.Completed,
            };
        }
        
        private void SaveDatabase()
        {
            DatabaseLocator.Database.SaveChanges();
        }
    }
}
