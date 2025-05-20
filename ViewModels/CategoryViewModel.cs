using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using todoapp.Models;

namespace todoapp.ViewModels
{
    public class CategoryViewModel
    {
        public ObservableCollection<CategoryItem> Categories { get; set; }
        public Command AddCategoryCommand { get; set; }
        public object SelectedCategory { get; internal set; }

        public CategoryViewModel()
        {
            Categories = new ObservableCollection<CategoryItem>();
            AddCategoryCommand = new Command(AddCategory);
        }

        private void AddCategory()
        {
            var newCategory = new CategoryItem { Name = "New Category" };
            Categories.Add(newCategory);
        }
    }
}