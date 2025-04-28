using System.Text.Json.Serialization;
using todo_maui_reposetory.Models;

[JsonSerializable(typeof(Project))]
[JsonSerializable(typeof(ProjectTask))]
[JsonSerializable(typeof(ProjectsJson))]

[JsonSerializable(typeof(Tag))]
public partial class JsonContext : JsonSerializerContext
{
}