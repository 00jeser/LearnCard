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
    public partial class MainPage : ContentPage
    {
        public MainPage()
        {
            InitializeComponent();
            BindingContext = new MainPageViewModel();
        }

        private void CardsCollection_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            CardsCollection.SelectedItem = null;
        }

        protected override bool OnBackButtonPressed()
        {
            (BindingContext as MainPageViewModel).AddShow = false;
            return true;
        }
    }
}