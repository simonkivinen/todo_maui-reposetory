namespace todo_maui_reposetory;

public partial class AddEditTodoPage : ContentPage
{
    private TodoItem currentItem;

    public AddEditTodoPage(TodoItem item)
    {
        InitializeComponent();
        currentItem = item;
        BindingContext = currentItem;
    }

    private async void OnSaveClicked(object sender, EventArgs e)
    {
        var vm = (MainPage)Application.Current.MainPage.Navigation.NavigationStack.First();
        if (!vm.viewModel.Items.Contains(currentItem))
        {
            vm.viewModel.Items.Add(currentItem);
        }
        vm.viewModel.SaveItems();
        await Navigation.PopAsync();
    }
}