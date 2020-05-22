using System;

using Android.App;
using Android.Content.PM;
using Android.Runtime;
using Android.Views;
using Android.Widget;
using Android.OS;
using Xamarin.Forms;
using Android.Content;

namespace ForeGroundService.Droid
{
    [Activity(Label = "ForeGroundService", Icon = "@mipmap/icon", Theme = "@style/MainTheme", MainLauncher = true, ConfigurationChanges = ConfigChanges.ScreenSize | ConfigChanges.Orientation)]
    public class MainActivity : global::Xamarin.Forms.Platform.Android.FormsAppCompatActivity
    {
        protected override void OnCreate(Bundle savedInstanceState)
        {
            TabLayoutResource = Resource.Layout.Tabbar;
            ToolbarResource = Resource.Layout.Toolbar;

            base.OnCreate(savedInstanceState);

            Xamarin.Essentials.Platform.Init(this, savedInstanceState);
            global::Xamarin.Forms.Forms.Init(this, savedInstanceState);
            LoadApplication(new App());
            //StartBackgroundService();
            StartForegroundService();
        }
        public override void OnRequestPermissionsResult(int requestCode, string[] permissions, [GeneratedEnum] Android.Content.PM.Permission[] grantResults)
        {
            Xamarin.Essentials.Platform.OnRequestPermissionsResult(requestCode, permissions, grantResults);

            base.OnRequestPermissionsResult(requestCode, permissions, grantResults);
        }

        void StartForegroundService()
        {
            MessagingCenter.Subscribe<string>(this, "ForegroundService", message =>
            {

                if (message.Equals("Start"))
                {
                    var intent = new Intent(this, typeof(ForegroundService));
                    if (Android.OS.Build.VERSION.SdkInt >= Android.OS.BuildVersionCodes.O)
                    {
                        StartForegroundService(intent);
                    }
                    else
                    {
                        StartService(intent);
                    }
                }
                else
                {
                    var intent = new Intent(this, typeof(ForegroundService));
                    if (Android.OS.Build.VERSION.SdkInt >= Android.OS.BuildVersionCodes.O)
                    {
                        StopService(intent);
                    }
                    else
                    {
                        StopService(intent);
                    }
                }
            });
        }

        
    }
}