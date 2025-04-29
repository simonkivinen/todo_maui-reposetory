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
        var category = new Category { Name = "Ny kategori", Description = "Beskrivning" };
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
            // TODO: Add using directive for CategoryDetailPage or create the CategoryDetailPage class
            // For now, display category details in a simple alert
            await DisplayAlert("Category Details", 
                $"Name: {category.Name}\nDescription: {category.Description}", 
                "OK");
        }
    }
}