using Max8.NET.ViewModels;
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
            mainVm = new MainVm();
            mainView = new MainWindow { DataContext = mainVm };
            mainView.Show();
        }
    }
}