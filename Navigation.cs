using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using todoapp.ViewModels;
using Microsoft.Maui.Controls;

namespace navigation
{
    public partial class MainPage : ContentPage
    {
        public CategoryViewModel ViewModel { get; set; }

        public MainPage()
        {
            InitializeComponent();
            ViewModel = new CategoryViewModel();
            BindingContext = ViewModel;
        }

        private void InitializeComponent()
        {
            throw new NotImplementedException();
        }
    }
}
