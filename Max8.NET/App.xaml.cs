using Max8.NET.Views;
using System.Windows;

namespace Max8.NET
{
    public partial class App : Application
    {
        Window mainView;
        object mainVm;

        protected override void OnStartup(StartupEventArgs e)
        {
            base.OnStartup(e);
            mainView = new MainWindow();
            mainView.Show();
        }
    }
}