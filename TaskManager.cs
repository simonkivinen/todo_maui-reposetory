using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Collections.Generic;
using todoapp.Models;

public class TaskManager
{
    public List<TaskItem> Tasks { get; set; }

    public TaskManager()
    {
        Tasks = new List<TaskItem>();
    }

    public void AddTask(TaskItem task)
    {
        Tasks.Add(task);
    }

    public void RemoveTask(TaskItem task)
    {
        Tasks.Remove(task);
    }
}
