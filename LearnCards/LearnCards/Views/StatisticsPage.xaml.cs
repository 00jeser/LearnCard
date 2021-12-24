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
    [QueryProperty(nameof(id), "id")]
    [QueryProperty(nameof(Max), "max")]
    [QueryProperty(nameof(Done), "done")]
    public partial class StatisticsPage : ContentPage
    {
        private string _id;
        public string id
        {
            set
            {
                _id = value;
                nameLabel.Text = Singleton.Storage.GetCollectionById(int.Parse(value)).Name;
            }
            get => _id;
        }

        private string max;
        public string Max
        {
            set
            {
                max = value;
                if (!string.IsNullOrWhiteSpace(done))
                {
                    ring.Progress = double.Parse(done) / double.Parse(max);
                    lbl.Text = $"{done}/{max}";
                }
            }
        }

        private string done;
        public string Done
        {
            set
            {
                done = value;
                if (!string.IsNullOrWhiteSpace(max))
                {
                    ring.Progress = double.Parse(done) / double.Parse(max);
                    lbl.Text = $"{done}/{max}";
                }
            }
        }
        public StatisticsPage()
        {
            InitializeComponent();
        }

        private void Button_Clicked(object sender, EventArgs e)
        {
            Shell.Current.GoToAsync($"//Learn?id={id}");
        }

        private void GoToMainPage(object sender, EventArgs e)
        {
            Shell.Current.GoToAsync("//Main");
        }
    }
}