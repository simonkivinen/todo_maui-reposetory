using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Collections.ObjectModel;
using todoapp.Models;
using System.Windows.Input;

namespace todoapp.ViewModels
{
    public class TaskViewModel
    {
        
        public ObservableCollection<TaskItem> Tasks { get; set; }
        public TaskItem SelectedTask { get; set; }
        public ICommand DeleteTaskCommand { get; set; }
        public ICommand UpdateTaskCommand { get; set; }

        public TaskViewModel()
        {
            Tasks = new ObservableCollection<TaskItem>();
            DeleteTaskCommand = new Command(DeleteTask);
            UpdateTaskCommand = new Command(UpdateTask);
        }

        public void DeleteTask()
        {
            if (SelectedTask != null)
            {
                Tasks.Remove(SelectedTask);
            }
        }

        public void UpdateTask()
        {
            // Här kan du fylla i logik för att uppdatera uppgiften
            // Om det finns en UI-komponent att hantera uppdatering, koppla den här
        }
    }
}