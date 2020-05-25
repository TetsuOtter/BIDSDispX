using System;

using Android.App;
using Android.Content.PM;
using Android.Runtime;
using Android.Views;
using Android.Widget;
using Android.OS;
using TR.BIDSDispX.Core;
using TR.BIDSDIspX.Droid;

namespace TR.BIDSDispX.Droid
{

  [Activity(Label = "BIDSDispX", Icon = "@mipmap/ic_launcher", Theme = "@style/MainTheme", MainLauncher = true, ConfigurationChanges = ConfigChanges.ScreenSize | ConfigChanges.Orientation)]
  public class MainActivity : global::Xamarin.Forms.Platform.Android.FormsAppCompatActivity
  {
    SystemUiFlags systemUiFlags = SystemUiFlags.LayoutStable
            | SystemUiFlags.LayoutHideNavigation
            | SystemUiFlags.LayoutFullscreen
            | SystemUiFlags.HideNavigation
            | SystemUiFlags.Fullscreen
            | SystemUiFlags.ImmersiveSticky;

    protected override void OnCreate(Bundle savedInstanceState)
    {      
      Window.DecorView.SystemUiVisibility = (StatusBarVisibility)(int)systemUiFlags;

      TabLayoutResource = Resource.Layout.Tabbar;
      ToolbarResource = Resource.Layout.Toolbar;

      RequestWindowFeature(WindowFeatures.NoTitle);
      if (Build.VERSION.SdkInt >= BuildVersionCodes.Lollipop)
      {
        Window.AddFlags(WindowManagerFlags.Fullscreen);
        Window.AddFlags(WindowManagerFlags.KeepScreenOn);
      }

      base.OnCreate(savedInstanceState);

      Xamarin.Essentials.Platform.Init(this, savedInstanceState);
      global::Xamarin.Forms.Forms.Init(this, savedInstanceState);
      LoadApplication(new App());
    }

    protected override void OnResume()
    {
      base.OnResume();
      Window.DecorView.SystemUiVisibility = (StatusBarVisibility)(int)systemUiFlags;

    }

    public override void OnBackPressed()
    {
      if (DispCom.CurrentView is StartPage)
      {
        base.OnBackPressed();
      }
      else
      {
        DispCom.CurrentView = new StartPage();
      }
    }

    public override void OnRequestPermissionsResult(int requestCode, string[] permissions, [GeneratedEnum] Permission[] grantResults)
    {
      Xamarin.Essentials.Platform.OnRequestPermissionsResult(requestCode, permissions, grantResults);

      base.OnRequestPermissionsResult(requestCode, permissions, grantResults);
    }
  }
}