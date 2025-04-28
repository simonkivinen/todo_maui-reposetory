using System.Collections.ObjectModel;
using System.Text.Json.Serialization;

namespace todo_maui_reposetory.Models
{
    public class Category
    {
        public int ID { get; set; }
        public string Title { get; set; } = string.Empty;

        // Ändra namnet på egenskapen till HexColor
        [JsonPropertyName("color")]
        public string HexColor { get; set; } = "#FF0000";
        [JsonIgnore]
        public Brush ColorBrush => new SolidColorBrush(Color.FromArgb(HexColor));
        
    }
}


