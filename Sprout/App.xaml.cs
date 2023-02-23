using Sprout.Sevices;
using Sprout.Views;
using System;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace Sprout
{
    public partial class App : Application
    {

        public App()
        {
            InitializeComponent();
            TheTheme.SetTheme();
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
