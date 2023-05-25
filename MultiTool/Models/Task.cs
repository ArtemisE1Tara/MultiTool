using System;

namespace DailyTasksApp.Models
{
    public class Task
    {
        public string TaskName { get; set; }
        public bool IsCompleted { get; set; }
        public DateTime DateCreated { get; set; }
    }
}
