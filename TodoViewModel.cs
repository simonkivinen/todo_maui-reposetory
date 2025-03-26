using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Text.Json;
using System.Threading.Tasks;

namespace todo_maui_reposetory
{
    public class TodoViewModel : INotifyPropertyChanged
    {
        private ObservableCollection<TodoItem> _items;
        private string _dataFile;

        public ObservableCollection<TodoItem> Items
        {
            get => _items;
            set
            {
                _items = value;
                OnPropertyChanged();
            }
        }

        public TodoViewModel()
        {
            _dataFile = Path.Combine(FileSystem.AppDataDirectory, "todos.json");
            LoadItems();
        }

        public void SaveItems()
        {
            var json = JsonSerializer.Serialize(Items);
            File.WriteAllText(_dataFile, json);
        }

        private void LoadItems()
        {
            if (File.Exists(_dataFile))
            {
                var json = File.ReadAllText(_dataFile);
                Items = JsonSerializer.Deserialize<ObservableCollection<TodoItem>>(json);
            }
            else
            {
                Items = new ObservableCollection<TodoItem>();
            }
        }

        public event PropertyChangedEventHandler PropertyChanged;

        protected virtual void OnPropertyChanged([CallerMemberName] string propertyName = null)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }
    }
}
