using Sprout.Models;
using Sprout.ViewModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace Sprout.Views
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class MainPage : ContentPage
    {
        public MainPage()
        {
            InitializeComponent();
        }

        protected override async void OnAppearing()
        {
            base.OnAppearing();
            var vm = (MainPageViewModel)BindingContext;
            if (vm.SmartDevices.Count == 0)
                await vm.RefreshCommand.ExecuteAsync();
        }
    }
}