using Microsoft.Maui.Controls;
using Microsoft.Maui.Media;
using Plugin.Maui.Audio; 
using System.Collections.Generic;
using System.Formats.Tar;
using todoapp.Models;
using Microsoft.Maui.Controls;
using todoapp.ViewModels;
using System;

namespace todoapp
{
    public partial class MainPage : ContentPage
    {
        public CategoryViewModel ViewModel { get; set; }

        public MainPage()
        {
            InitializeComponent();
            ViewModel = new CategoryViewModel();
            BindingContext = ViewModel;
        }

        private async void OnAddListClicked(object sender, EventArgs e)
        {
            // Lägg till ny kategori
            ViewModel.AddCategoryCommand.Execute(null);
        }

        private async void OnListSelected(object sender, SelectedItemChangedEventArgs e)
        {
            if (e.SelectedItem != null)
            {
                var selectedCategory = e.SelectedItem as CategoryItem;
                var taskViewModel = new TaskViewModel
                {
                    SelectedTask = selectedCategory.Tasks[0] // Här väljer vi den första uppgiften, justera vid behov.
                };
                var taskPage = new TaskPage(taskViewModel);
                await Navigation.PushAsync(taskPage);
            }
        }
    }
}