using System.Text.Json.Serialization;
using Android.Nfc;
using static Android.Icu.Util.ULocale;

namespace todo_maui_reposetory.Models
{
    public class Project
    {
        public int ID { get; set; }
        public string Title { get; set; } = string.Empty;
        public string Description { get; set; } = string.Empty;
        public int? CategoryID { get; set; }
        public Category? Category { get; set; }
        public List<TodoTask> Tasks { get; set; } = new();
        public List<Tag> Tags { get; set; } = new();
        public string Name { get; internal set; }
        public string Icon { get; internal set; }
    }
}
    public class ProjectTask
    {
        public int ID { get; set; }
        public string Name { get; set; } = string.Empty;
        public string Description { get; set; } = string.Empty;
        public string Icon { get; set; } = string.Empty;

        [JsonIgnore]
        public int CategoryID { get; set; }

        public Category? Category { get; set; }

        public List<ProjectTask> Tasks { get; set; } = [];

        public List<Tag> Tags { get; set; } = [];
    public bool IsCompleted { get; internal set; }
    public int ProjectID { get; internal set; }
    public string Title { get; internal set; }

    public override string ToString() => $"{Name}";

    public static implicit operator ProjectTask?(todo_maui_reposetory.Models.TodoTask? v)
    {
        throw new NotImplementedException();
    }
}
