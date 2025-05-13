using todo_maui_reposetory.Models;

public partial class MainPage : ContentPage
{
    public System.Collections.ObjectModel.ObservableCollection<Category> Categories { get; } = new();
    
    public MainPage()
    {
        InitializeComponent();
        BindingContext = this;
    }

    private void InitializeComponent()
    {
        throw new NotImplementedException();
    }

    private async void OnAddCategoryClicked(object sender, EventArgs e)
    {
        var category = new Category();
        Categories.Add(category);
    }

    private void OnDeleteCategoryClicked(object sender, EventArgs e)
    {
        if (Categories.Count > 0)
            Categories.RemoveAt(Categories.Count - 1);
    }

    private async void OnCategorySelected(object sender, SelectionChangedEventArgs e)
    {
        if (e.CurrentSelection.FirstOrDefault() is Category category)
        {
            await DisplayAlert("Category Details", 
                $"Category Details",
                "OK");
        }
    }
}