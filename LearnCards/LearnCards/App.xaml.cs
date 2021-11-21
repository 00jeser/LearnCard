using LearnCards.Services;
using LearnCards.Views;
using System;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace LearnCards
{
    public partial class App : Application
    {

        public App()
        {
            Singleton.Init();
            InitializeComponent();
            MainPage = new AppShell();
        }

        protected override void OnStart()
        {
        }

        protected override void OnSleep()
        {
        }

        protected override void OnResume()
        {
        }
    }
}
