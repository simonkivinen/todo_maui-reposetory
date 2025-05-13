using System;
using System.Collections.ObjectModel;
using todo_maui_reposetory.Models;

namespace todo_maui_reposetory.Models
{
    public class Category
    {
        public int ID { get; set; }
        public string Title { get; set; } = string.Empty;
        public string Name { get; set; } = string.Empty;  // Lägg till denna property
        public string Description { get; set; } = string.Empty;
        public string CreatedDate { get; set; } = DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss");
        public ObservableCollection<TodoItem> Items { get; set; } = new();
    }
}


