using System.Text.Json.Serialization;

namespace todo_maui_reposetory.Models
{
    public class ProjectsJson
    {
        [JsonPropertyName("projects")]
        public List<Project> Projects { get; set; } = new();
    }
}