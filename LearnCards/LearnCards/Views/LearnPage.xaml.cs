using LearnCards.Services;
using LearnCards.ViewModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace LearnCards.Views
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    [QueryProperty(nameof(Id), "id")]
    public partial class LearnPage : ContentPage
    {
        LearnViewModel viewModel;
        public string Id
        {
            set
            {
                viewModel = new LearnViewModel(int.Parse(value));
                BindingContext = viewModel;
            }
        }
        public LearnPage()
        {
            InitializeComponent();
        }

        protected override bool OnBackButtonPressed()
        {
            Shell.Current.GoToAsync("//Main");
            return true;
        }
    }
}