using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
namespace todo_maui_reposetory
{   
    public class TodoItem
    {
        public string Id { get; set; } = Guid.NewGuid().ToString();
        public string Title { get; set; }
        public string Description { get; set; }
        public bool IsCompleted { get; set; }
        public DateTime CreatedAt { get; set; } = DateTime.Now;
        public string Category { get; set; }
        public string Name { get; set; }
        public ObservableCollection<TodoItem> Items { get; set; } = new();

    }
}
