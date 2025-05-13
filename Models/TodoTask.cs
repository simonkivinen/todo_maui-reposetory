namespace todo_maui_reposetory.Models
{
    public class TodoTask
    {
        public int ID { get; set; }
        public string Title { get; set; } = string.Empty;
        public string Description { get; set; } = string.Empty;
        public bool IsCompleted { get; set; }
        public int CategoryID { get; set; }
        public string? DueDate { get; set; }
        public string CreatedDate { get; set; } = DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss");
        public int ProjectID { get; internal set; }
    }
}