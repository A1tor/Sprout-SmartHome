using System;
using Android.App;
using Android.Content.PM;
using Android.Runtime;
using Android.OS;
using Android.Widget;
using AndroidX.Core.View;
using MyCoffeeApp.Helpers;
using System.Threading.Tasks;
using Xamarin.Essentials;
using Xamarin.Forms;
using Sprout.Sevices;
using Sprout.Droid;

[assembly: Dependency(typeof(Toaster))]
[assembly: Dependency(typeof(Sprout.Droid.Environment))]

namespace Sprout.Droid
{
    [Activity(Label = "Sprout", Icon = "@mipmap/icon", Theme = "@style/MainTheme", MainLauncher = true, ConfigurationChanges = ConfigChanges.ScreenSize | ConfigChanges.Orientation | ConfigChanges.UiMode | ConfigChanges.ScreenLayout | ConfigChanges.SmallestScreenSize )]
    public class MainActivity : global::Xamarin.Forms.Platform.Android.FormsAppCompatActivity
    {
        protected override void OnCreate(Bundle savedInstanceState)
        {
            base.OnCreate(savedInstanceState);

            Xamarin.Essentials.Platform.Init(this, savedInstanceState);
            global::Xamarin.Forms.Forms.Init(this, savedInstanceState);
            LoadApplication(new App());
        }
        public override void OnRequestPermissionsResult(int requestCode, string[] permissions, [GeneratedEnum] Android.Content.PM.Permission[] grantResults)
        {
            Xamarin.Essentials.Platform.OnRequestPermissionsResult(requestCode, permissions, grantResults);

            base.OnRequestPermissionsResult(requestCode, permissions, grantResults);
        }
    }
    public class Toaster : IToast
    {
        public void MakeToast(string message)
        {
            Toast.MakeText(Platform.AppContext, message, ToastLength.Long).Show();
        }
    }
    public class Environment : IEnvironment
    {
        public async void SetStatusBarColor(System.Drawing.Color color, bool darkStatusBarTint)
        {
            if (Build.VERSION.SdkInt < Android.OS.BuildVersionCodes.Lollipop)
                return;

            var activity = Platform.CurrentActivity;
            var window = activity.Window;

            window.AddFlags(Android.Views.WindowManagerFlags.DrawsSystemBarBackgrounds);
            window.ClearFlags(Android.Views.WindowManagerFlags.TranslucentStatus);

            if (Build.VERSION.SdkInt >= Android.OS.BuildVersionCodes.M)
            {
                await Task.Delay(50);
                WindowCompat.GetInsetsController(window, window.DecorView).AppearanceLightStatusBars = darkStatusBarTint;
            }

            window.SetStatusBarColor(color.ToPlatformColor());
        }
    }
}