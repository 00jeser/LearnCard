using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Xamarin.Essentials;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace LearnCards.Controls
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class SwypedCard : ContentView
    {
        public SwypedCard()
        {
            InitializeComponent();
            PanGuesture.PanUpdated += PanGuesture_PanUpdated;
            TapGuesture.Tapped += TapGuesture_Tapped;
            lab.Text = Shell.Current.CurrentPage?.Width.ToString();
        }

        private string _field1;
        public string Field1
        {
            get => (string)GetValue(Field1Property);
            set
            {
                SetValue(Field1Property, value);
                lab.Text = value;
                _field1 = value;
            }
        }
        public static readonly BindableProperty Field1Property = BindableProperty.Create(
            nameof(Field1),
            typeof(string),
            typeof(SwypedCard),
            "def",
            propertyChanging: (BindableObject bindable, object oldValue, object newValue) =>
            {
                (bindable as SwypedCard).Field1 = (string)newValue;
            }
         );

        private string _field2;
        public string Field2
        {
            get => (string)GetValue(Field2Property);
            set
            {
                SetValue(Field2Property, value);
                lab.Text = value;
                _field2 = value;
            }
        }
        public static readonly BindableProperty Field2Property = BindableProperty.Create(
            nameof(Field2),
            typeof(string),
            typeof(SwypedCard),
            "def",
            propertyChanging: (BindableObject bindable, object oldValue, object newValue) =>
            {
                (bindable as SwypedCard).Field2 = (string)newValue;
            }
         );

        private Command _commandLeft;
        public Command CommandLeft
        {
            get => (Command)GetValue(CommandLeftProperty);
            set
            {
                SetValue(CommandLeftProperty, value);
                _commandLeft = value;
            }
        }
        public static readonly BindableProperty CommandLeftProperty = BindableProperty.Create(
            nameof(CommandLeft),
            typeof(Command),
            typeof(SwypedCard),
            null,
            propertyChanging: (BindableObject bindable, object oldValue, object newValue) =>
            {
                (bindable as SwypedCard).CommandLeft = (Command)newValue;
            }
         );


        private Command _commandRight;
        public Command CommandRight
        {
            get => (Command)GetValue(CommandRightProperty);
            set
            {
                SetValue(CommandRightProperty, value);
                _commandRight = value;
            }
        }
        public static readonly BindableProperty CommandRightProperty = BindableProperty.Create(
            nameof(CommandRight),
            typeof(Command),
            typeof(SwypedCard),
            null,
            propertyChanging: (BindableObject bindable, object oldValue, object newValue) =>
            {
                (bindable as SwypedCard).CommandRight = (Command)newValue;
            }
         );

        private object _commandParameter;
        public object CommandParameter
        {
            get => GetValue(CommandParameterProperty);
            set
            {
                SetValue(CommandParameterProperty, value);
                _commandParameter = value;
            }
        }
        public static readonly BindableProperty CommandParameterProperty = BindableProperty.Create(
            nameof(Field2),
            typeof(object),
            typeof(SwypedCard),
            null,
            propertyChanging: (BindableObject bindable, object oldValue, object newValue) =>
            {
                (bindable as SwypedCard).CommandParameter = newValue;
            }
         );

        private async void TapGuesture_Tapped(object sender, EventArgs e)
        {
            await frame.RotateYTo(90);
            lab.Text = lab.Text == _field1 ? _field2 : _field1;
            await frame.RotateYTo(180);
            frame.RotationY = 0;
        }

        double LastX = 0;
        private async void PanGuesture_PanUpdated(object sender, PanUpdatedEventArgs e)
        {
            frame.TranslationX = e.TotalX * 1.3;
            frame.TranslationY = Math.Abs(e.TotalX / 5);
            frame.Rotation = e.TotalX / 20;
            if (e.StatusType == GestureStatus.Completed)
                if (LastX > 100)
                {
                    frame.TranslationX = LastX * 1.3;
                    frame.TranslationY = Math.Abs(LastX / 5);
                    frame.Rotation = LastX / 20;

                    frame.TranslateTo(500, frame.TranslationY + 400);
                    await frame.RotateTo(90);
                    CommandRight?.Execute(CommandParameter);
                    frame.TranslationY = 0;
                    frame.TranslationX = 0;
                    frame.Rotation = 0;
                }
                else if (LastX < -100)
                {
                    frame.TranslationX = LastX * 1.3;
                    frame.TranslationY = Math.Abs(LastX / 5);
                    frame.Rotation = LastX / 20;

                    frame.TranslateTo(-500, frame.TranslationY + 400);
                    await frame.RotateTo(-90);
                    CommandLeft?.Execute(CommandParameter);
                    frame.TranslationY = 0;
                    frame.TranslationX = 0;
                    frame.Rotation = 0;
                }
            LastX = e.TotalX;
        }
    }
}