using LearnCards.Services;
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
    public partial class ColectionSelectPage : ContentPage
    {
        public ColectionSelectPage()
        {
            InitializeComponent();
            BindingContext = Resources["vm"];
        }

        private void TapGestureRecognizer_Tapped(object sender, EventArgs e)
        {
            //((ViewModels.CollectionSelectViewModel)BindingContext).OpenCollection.Execute((sender as Frame).);
        }
    }
}