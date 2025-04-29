using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace todo_maui_reposetory.Models
{
    public class TodoList
    {
        public string Id { get; } = Guid.NewGuid().ToString();
        public string Name { get; set; }
        public string Description { get; set; }
        public ObservableCollection<TodoItem> Items { get; set; } = new();
        public DateTime CreatedAt { get; } = DateTime.Now;
    }
}

// Models/TodoItem.cs
namespace todo_maui_reposetory.Models
{
    public class TodoItem
    {
        public string Id { get; } = Guid.NewGuid().ToString();
        public string Title { get; set; }
        public string Description { get; set; }
        public bool IsCompleted { get; set; }
        public DateTime CreatedAt { get; } = DateTime.Now;

        public static explicit operator TodoItem(todo_maui_reposetory.TodoItem v)
        {
            throw new NotImplementedException();
        }
    }
}

