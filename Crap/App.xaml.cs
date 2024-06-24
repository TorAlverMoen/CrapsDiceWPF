using System.Windows;

namespace Crap
{
    /// <summary>
    /// Interaction logic for App.xaml
    /// </summary>
    public partial class App : Application
    {
        protected override void OnStartup(StartupEventArgs e)
        {
            base.OnStartup(e);

            SplashScreen splash = new SplashScreen("img/CrapSplash.png");
            splash.Show(autoClose: false);
            Thread.Sleep(4000);
            splash.Close(TimeSpan.FromMilliseconds(500));
        }
    }

}
