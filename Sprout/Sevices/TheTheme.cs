using MyCoffeeApp.Helpers;
using System;
using System.Collections.Generic;
using System.Text;
using Xamarin.Forms;

namespace Sprout.Sevices
{
    public static class TheTheme
    {
        public static void SetTheme()
        {
            switch (Settings.Theme)
            {
                case 0:
                    App.Current.UserAppTheme = OSAppTheme.Unspecified;
                    break;
                case 1:
                    App.Current.UserAppTheme = OSAppTheme.Light;
                    break;
                case 2:
                    App.Current.UserAppTheme = OSAppTheme.Dark;
                    break;
            }

            var nav = App.Current.MainPage as Xamarin.Forms.NavigationPage;

            var e = DependencyService.Get<IEnvironment>();

            if (App.Current.RequestedTheme == OSAppTheme.Dark)
            {
                e?.SetStatusBarColor(Color.FromHex("#132a13"), false);
                if (nav != null)
                {
                    nav.BarBackgroundColor = Color.FromHex("#132a13");
                    nav.BarTextColor = Color.FromHex("#ecf39e");
                }
            }
            else
            {
                e?.SetStatusBarColor(Color.FromHex("#ecf39e"), true);
                if (nav != null)
                {
                    nav.BarBackgroundColor = Color.FromHex("#ecf39e");
                    nav.BarTextColor = Color.FromHex("#132a13");
                }
            }
        }
    }
}
