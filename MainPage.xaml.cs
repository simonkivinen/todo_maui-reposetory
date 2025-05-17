using Microsoft.Maui.Controls;
using System.Collections.Generic;
using System.Formats.Tar;
using Microsoft.Maui.Media;
// Ensure you have installed the Plugin.Maui.Audio NuGet package in your project.
// You can do this by running the following command in the NuGet Package Manager Console:
// Install-Package Plugin.Maui.Audio

using Plugin.Maui.Audio; // This namespace is part of the Plugin.Maui.Audio NuGet package.


namespace todoapp
{
    public partial class MainPage : ContentPage
    {
        private List<TaskItem> tasks;

        public MainPage()
        {
            InitializeComponent();
            tasks = new List<TaskItem>();
        }

        private async void OnAddListClicked(object sender, EventArgs e)
        {
            var newTaskTitle = TaskEntry.Text;
            var taskDescription = DescriptionEntry.Text;

            if (!string.IsNullOrEmpty(newTaskTitle) && !string.IsNullOrEmpty(taskDescription))
            {
                var newTask = new TaskItem
                {
                    Title = newTaskTitle,
                    Description = taskDescription,
                    IsCompleted = false
                };

                // Check for the special Easter egg
                if (newTaskTitle.Equals("order66", StringComparison.OrdinalIgnoreCase))
                {
                    // Visa Empire-loggan
                    var empireImage = new Image
                    {
                        Source = "empire_logo.png",  // Byt ut med filnamnet för din logga
                        WidthRequest = 100,          // Ställ in bredden
                        HeightRequest = 100,         // Ställ in höjden
                        HorizontalOptions = LayoutOptions.Center,
                        VerticalOptions = LayoutOptions.Center
                    };

                    TaskList.Children.Add(empireImage);
                    await DisplayAlert("Clone trupper", "That will be done my lorde", "OK");

                    // Eller spela ljudklippet
                    await PlaySound("order66.mp3");
                }
                else
                {
                    tasks.Add(newTask);
                    UpdateTaskList();
                }

                TaskEntry.Text = "";
                DescriptionEntry.Text = "";
            }
            else
            {
                await DisplayAlert("Error", "Please enter both a title and a description.", "OK");
            }
        }

        // Metod för att spela ljud
        private async Task PlaySound(string fileName)
        {
            try
            {
                var audioPlayer = AudioManager.Current.CreatePlayer(await FileSystem.OpenAppPackageFileAsync(fileName));
                audioPlayer.Play();
            }
            catch (Exception ex)
            {
                await DisplayAlert("Error", $"Could not play sound: {ex.Message}", "OK");
            }
        }

        private void UpdateTaskList()
        {
            TaskList.Children.Clear();
            foreach (var task in tasks)
            {
                var taskLayout = new StackLayout { Orientation = StackOrientation.Horizontal, Margin = new Thickness(0, 5) };

                var checkBox = new CheckBox { IsChecked = task.IsCompleted };
                checkBox.CheckedChanged += (s, e) =>
                {
                    task.IsCompleted = e.Value;
                    UpdateTaskList();
                };

                var taskLabel = new Label
                {
                    Text = task.Title,
                    FontSize = 16,
                    VerticalOptions = LayoutOptions.Center
                };

                var descriptionEntry = new Entry
                {
                    Text = task.Description,
                    IsVisible = false
                };

                var deleteButton = new Button
                {
                    Text = "Delete",
                    IsVisible = false  // Döljer knappen tills uppgiften är markerad
                };

                var updateButton = new Button
                {
                    Text = "Update",
                    IsVisible = false // Döljer knappen till en början
                };

                // Hantera klick på uppgiftens titel
                taskLabel.GestureRecognizers.Add(new TapGestureRecognizer
                {
                    NumberOfTapsRequired = 1, // Markera uppgiften
                    Command = new Command(() =>
                    {
                        task.IsCompleted = true; // Markerar som slutförd
                        deleteButton.IsVisible = true; // Visar delete-knappen
                        descriptionEntry.IsVisible = true; // Visar beskrivningsfältet
                        updateButton.IsVisible = true; // Visar uppdateringsknappen
                    })
                });

                // Lägg till logik för delete-knapp
                deleteButton.Clicked += (s, e) =>
                {
                    tasks.Remove(task);
                    UpdateTaskList(); // Uppdatera listan
                };

                // Dubbelklick för att ta bort uppgift
                taskLabel.GestureRecognizers.Add(new TapGestureRecognizer
                {
                    NumberOfTapsRequired = 2,
                    Command = new Command(() =>
                    {
                        tasks.Remove(task);
                        UpdateTaskList();
                    })
                });

                // Klicka på uppdateringsknappen
                updateButton.Clicked += (s, e) =>
                {
                    task.Description = descriptionEntry.Text; // Uppdatera beskrivningen
                    descriptionEntry.IsVisible = false; // Dölja fältet
                    deleteButton.IsVisible = false; // Dölja knappen
                    updateButton.IsVisible = false; // Dölja uppdateringsknappen
                    UpdateTaskList(); // Uppdatera listan
                };

                // Lägg till element i layout
                taskLayout.Children.Add(checkBox);
                taskLayout.Children.Add(taskLabel);
                taskLayout.Children.Add(descriptionEntry);
                taskLayout.Children.Add(deleteButton);
                taskLayout.Children.Add(updateButton); // Lägg till knappen i layouten

                TaskList.Children.Add(taskLayout);
            }
        }


        public class TaskItem
        {
            public string Title { get; set; }
            public string Description { get; set; }
            public bool IsCompleted { get; set; }
        }
    }
}