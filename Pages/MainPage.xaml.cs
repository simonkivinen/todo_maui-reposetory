using todo_maui_reposetory.Models;
using todo_maui_reposetory.PageModels;

namespace todo_maui_reposetory.Pages
{
    public partial class MainPage : ContentPage
    {
        private TodoViewModel viewModel;

        public MainPage()
        {
            InitializeComponent();
            viewModel = new TodoViewModel();
            BindingContext = viewModel;
        }

        protected override void OnDisappearing()
        {
            viewModel.SaveItems();
            base.OnDisappearing();
        }

        private async void OnAddClicked(object sender, EventArgs e)
        {
            await Navigation.PushAsync(new AddEditTodoPage(new TodoItem()));
        }
    }
}