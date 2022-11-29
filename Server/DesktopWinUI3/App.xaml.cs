using Microsoft.UI.Xaml;
using System.Collections.Generic;
using YT.Data;

namespace DesktopWinUI3
{
    public partial class App : Application
    {
        Window m_window;

        public App()
        {
            this.InitializeComponent();

          
        }

        protected override void OnLaunched(Microsoft.UI.Xaml.LaunchActivatedEventArgs args)
        {
            m_window = new MainWindow();
            m_window.Activate();
        }

    }
}
