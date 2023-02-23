using Sprout.Views;
using System;
using System.Collections.Generic;
using Xamarin.Forms;

namespace Sprout
{
    public partial class AppShell : Xamarin.Forms.Shell
    {
        public AppShell()
        {
            InitializeComponent();

            Routing.RegisterRoute(nameof(AddRoomPage), typeof(AddRoomPage));
            Routing.RegisterRoute(nameof(AddDevicePage), typeof(AddDevicePage));
            Routing.RegisterRoute(nameof(ManageDevicePage), typeof(ManageDevicePage));
            Routing.RegisterRoute(nameof(RegistrationPage), typeof(RegistrationPage));
        }
    }
}
