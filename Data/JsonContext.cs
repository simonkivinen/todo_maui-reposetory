using System.Text.Json.Serialization;
using todo_maui_reposetory.Models;

namespace todo_maui_reposetory.Data
{
    [JsonSourceGenerationOptions(WriteIndented = true)]
    [JsonSerializable(typeof(ProjectsJson))]
    [JsonSerializable(typeof(Tag))]
    public partial class JsonContext : JsonSerializerContext
    {
    }
}