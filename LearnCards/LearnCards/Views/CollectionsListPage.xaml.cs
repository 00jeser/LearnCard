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
    public partial class CollectionsListPage : ContentPage
    {
        public CollectionsListPage()
        {
            InitializeComponent();
            BindingContext = Resources["vm"];
        }
        protected override bool OnBackButtonPressed()
        {
            (BindingContext as CollectionsListViewModel).Back();
            return true;
        }
    }
}