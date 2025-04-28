using todo_maui_reposetory.Models;
using todo_maui_reposetory.PageModels;
using System.Collections.ObjectModel;
using System.Windows.Input;
using System.ComponentModel;
using System.Runtime.CompilerServices;
using todo_maui_reposetory.PageModels;

namespace todo_maui_reposetory.Pages
{
    public partial class MainPage : ContentPage
    {
        public MainPage(MainPageModel viewModel)
        {
            InitializeComponent();
            BindingContext = viewModel;
        }

        protected override void OnDisappearing()
        {
            (BindingContext as MainPageModel)?.SaveLists();
            base.OnDisappearing();
        }
    }
}