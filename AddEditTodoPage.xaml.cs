namespace todo_maui_reposetory.Pages;

public partial class AddEditTodoPage : ContentPage
{
    private TodoItem currentItem;
    private string a = "Test";

    public AddEditTodoPage(TodoItem item)
    {
        InitializeComponent();
        currentItem = item;
        BindingContext = currentItem;
    }

    private async void OnSaveClicked(object sender, EventArgs e)
    {
        if (Application.Current.MainPage is MainPage mainPage)
        {
            if (!mainPage.ViewModel.Items.Contains(currentItem))
            {
                mainPage.ViewModel.Items.Add(currentItem);
            }
            mainPage.ViewModel.SaveItems();
        }
        await Navigation.PopAsync();
    }
}