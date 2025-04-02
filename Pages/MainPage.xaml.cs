using todo_maui_reposetory.Models;
using todo_maui_reposetory.PageModels;

namespace todo_maui_reposetory.Pages
{
    public partial class MainPage : ContentPage
    {

        public TodoViewModel ViewModel { get; private set; }

        public MainPage()
        {
            InitializeComponent();
            ViewModel = new TodoViewModel();
            BindingContext = ViewModel;

        }

        protected override void OnDisappearing()
        {
            ViewModel.SaveItems();
            base.OnDisappearing();
        }

        private async void OnAddClicked(object sender, EventArgs e)
        {
            await Navigation.PushAsync(new AddEditTodoPage(new TodoItem()));
        }
    }
}