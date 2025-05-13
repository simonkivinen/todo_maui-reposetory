using System.Text.Json.Serialization;
using CommunityToolkit.Maui.Core.Extensions;

namespace todo_maui_reposetory.Models
{
    public class Tag
    {
        public int ID { get; set; }
        public string Name { get; set; } = string.Empty;
        public string Color { get; set; } = string.Empty;
        public bool IsSelected { get; internal set; }
    }
}