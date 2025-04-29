using System.Text.Json.Serialization;

namespace todo_maui_reposetory.Models
{
    public class ProjectTask
    {
        public int ID { get; set; }
        public string Title { get; set; } = string.Empty;
        public bool IsCompleted { get; set; }

        [JsonIgnore]
        public int ProjectID { get; set; }

        internal object WhenAll(Task task1, Task task2)
        {
            throw new NotImplementedException();
        }
    }
}