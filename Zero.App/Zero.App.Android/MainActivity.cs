using Android.App;
using Android.Content.PM;
using Avalonia;
using Avalonia.Android;
using Java.Lang;

namespace Zero.App.Android;

[Activity(
    Label = "Zero.App.Android",
    Theme = "@style/MyTheme.NoActionBar",
    Icon = "@drawable/icon",
    MainLauncher = true,
    ConfigurationChanges = ConfigChanges.Orientation | ConfigChanges.ScreenSize | ConfigChanges.UiMode)]
public class MainActivity : AvaloniaMainActivity
{

}
