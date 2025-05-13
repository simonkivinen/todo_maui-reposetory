using System;

namespace todo_maui_reposetory.Models
{
    public class TodoItem
    {
        public string Id { get; } = Guid.NewGuid().ToString();
        public required string Title { get; set; }
        public required string Description { get; set; }
        public bool IsCompleted { get; set; }
        public DateTime CreatedAt { get; } = DateTime.Now;
        public required string Category { get; set; }
    }
}