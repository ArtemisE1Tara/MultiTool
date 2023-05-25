using System;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.IO;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Threading.Tasks;
using Newtonsoft.Json;
using Windows.Storage;
using MultiTool.Models;

namespace MultiTool.ViewModels
{
    public class TaskViewModel : INotifyPropertyChanged
    {
        private ObservableCollection<TaskModel> tasks;
        public ObservableCollection<TaskModel> Tasks
        {
            get { return tasks; }
            set
            {
                tasks = value;
                OnPropertyChanged(nameof(Tasks));
            }
        }

        public TaskViewModel()
        {
            LoadTasks();
            SortTasksByDate();
        }

        public async void SaveTasks()
        {
            var localFolder = ApplicationData.Current.LocalFolder;
            var tasksFile = await localFolder.CreateFileAsync("tasks.json", CreationCollisionOption.ReplaceExisting);

            string tasksJson = JsonConvert.SerializeObject(Tasks);
            await FileIO.WriteTextAsync(tasksFile, tasksJson);
        }

        public async void LoadTasks()
        {
            var localFolder = ApplicationData.Current.LocalFolder;
            try
            {
                var tasksFile = await localFolder.GetFileAsync("tasks.json");
                string tasksJson = await FileIO.ReadTextAsync(tasksFile);
                Tasks = JsonConvert.DeserializeObject<ObservableCollection<TaskModel>>(tasksJson);
            }
            catch (FileNotFoundException)
            {
                Tasks = new ObservableCollection<TaskModel>();
            }
        }

        public void SortTasksByDate()
        {
            Tasks = new ObservableCollection<TaskModel>(Tasks.OrderByDescending(t => t.DateCreated));
        }

        public void DeleteTask(TaskModel task)
        {
            Tasks.Remove(task);
        }

        public event PropertyChangedEventHandler PropertyChanged;

        protected virtual void OnPropertyChanged([CallerMemberName] string propertyName = null)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }
    }
}
