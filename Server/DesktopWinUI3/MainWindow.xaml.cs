using Microsoft.UI.Xaml;
using Microsoft.UI.Xaml.Controls;
using System;

namespace DesktopWinUI3
{
    public sealed partial class MainWindow : Window
    {

        public string AppTitleText
        {
            get
            {
#if DEBUG
                return "YT dashboard debug";
#else
                return "YT dashboard";
#endif
            }
        }

        public MainWindow()
        {
            this.InitializeComponent();
            Type pageType = Type.GetType("DesktopWinUI3.ListOfVideos");
            contentFrame.Navigate(pageType);


            MainWindowXaml.ExtendsContentIntoTitleBar = true;  // enable custom titlebar
            MainWindowXaml.SetTitleBar(AppTitleBar);      // set user ui element as titlebar
        }

        private void NavigationBar_SelectionChanged(NavigationView sender, NavigationViewSelectionChangedEventArgs args)
        {
            var selectedItem = (NavigationViewItem)args.SelectedItem;
            string selectedItemTag = ((string)selectedItem.Tag);
            string pageName = "DesktopWinUI3." + selectedItemTag;

            Type pageType = Type.GetType(pageName);
            contentFrame.Navigate(pageType, null, args.RecommendedNavigationTransitionInfo);
        }
    }
}
