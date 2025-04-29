using System.Collections.ObjectModel;
using System.Text.Json.Serialization;

namespace todo_maui_reposetory.Models
{
    public class Category
    {
        internal int Id;
        internal int ID;
        public string Name { get; set; }
        public string Description { get; set; }
        public ObservableCollection<TodoTask> Tasks { get; set; } = new();
        public string Title { get; internal set; }
        public string HexColor { get; internal set; }
    }
}


