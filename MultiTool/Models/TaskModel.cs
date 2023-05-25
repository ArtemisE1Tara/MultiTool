namespace MultiTool.Models
{
    public class TaskModel
    {
        public string TaskName { get; set; }
        public bool IsCompleted { get; set; }

        public TaskModel(string taskName)
        {
            TaskName = taskName;
        }
    }
}
