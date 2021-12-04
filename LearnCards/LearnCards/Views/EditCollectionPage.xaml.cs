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
    public partial class EditCollectionPage : ContentPage
    {
        public string Id
        {
            set
            {
                BindingContext = new CollectionEditViewModel(Singleton.Storage.GetCollectionById(int.Parse(value)));
            }
        }
        public EditCollectionPage()
        {
            InitializeComponent();
        }
        protected override bool OnBackButtonPressed()
        {
            if (((CollectionEditViewModel)BindingContext).AddShow)
                ((CollectionEditViewModel)BindingContext).AddShow = false;
            else
                Shell.Current.GoToAsync("//CollectionSelect");
            return true;
        }
    }
}