using todo_maui_reposetory.Models;
using todo_maui_reposetory.Data;

namespace todo_maui_reposetory.Views
{
    public partial class MainPage : ContentPage
    {
        private readonly CategoryRepository _categoryRepository;
        private readonly TaskRepository _taskRepository;

        public object CategoryList { get; private set; }

        public MainPage(CategoryRepository categoryRepository, TaskRepository taskRepository)
        {
            InitializeComponent();
            _categoryRepository = categoryRepository;
            _taskRepository = taskRepository;
            LoadCategories();
        }

        private async 
        Task
LoadCategories()
        {
            try
            {
                // Här skulle vi hämta kategorier från databasen
                // För tillfället simulerar vi några testkategorier
                CategoryList.ItemsSource = new List<Category>
                {
                    new Category { ID = 1, Title = "Att göra", Description = "Dagliga uppgifter" },
                    new Category { ID = 2, Title = "Handla", Description = "Inköpslista" },
                    new Category { ID = 3, Title = "Projekt", Description = "Projektuppgifter" }
                };
            }
            catch (Exception ex)
            {
                await DisplayAlert("Fel", "Kunde inte ladda kategorier: " + ex.Message, "OK");
            }
        }

        private async void OnCategoryOptionsClicked(object sender, EventArgs e)
        {
            var button = sender as Button;
            var category = button?.CommandParameter as Category;
            if (category == null) return;

            var action = await DisplayActionSheet("Kategorialternativ", "Avbryt", null, 
                "Redigera lista", "Ta bort lista");

            switch (action)
            {
                case "Redigera lista":
                    // Implementera redigering
                    break;
                case "Ta bort lista":
                    bool answer = await DisplayAlert("Bekräfta", 
                        "Är du säker på att du vill ta bort denna lista?", "Ja", "Nej");
                    if (answer)
                    {
                        await _categoryRepository.DeleteItemAsync(category);
                        await LoadCategories();
                    }
                    break;
            }
        }

        private async void OnAddListClicked(object sender, EventArgs e)
        {
            string result = await DisplayPromptAsync("Ny lista", 
                "Ange namn på den nya listan:", "OK", "Avbryt");

            if (!string.IsNullOrEmpty(result))
            {
                var newCategory = new Category { Title = result };
                await _categoryRepository.SaveItemAsync(newCategory);
                await LoadCategories();
            }
        }

        private async void OnDeleteClicked(object sender, EventArgs e)
        {
            // Implementera borttagning av vald uppgift
        }
    }
}