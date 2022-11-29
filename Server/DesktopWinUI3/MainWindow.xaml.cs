// Copyright (c) Microsoft Corporation. All rights reserved.
// Licensed under the MIT License. See LICENSE in the project root for license information.

using Microsoft.EntityFrameworkCore;
using Microsoft.UI.Xaml;
using Microsoft.UI.Xaml.Controls;
using Microsoft.UI.Xaml.Controls.Primitives;
using Microsoft.UI.Xaml.Data;
using Microsoft.UI.Xaml.Input;
using Microsoft.UI.Xaml.Media;
using Microsoft.UI.Xaml.Navigation;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.IO;
using System.Linq;
using System.Runtime.InteropServices.WindowsRuntime;
using Windows.Foundation;
using Windows.Foundation.Collections;
using CommunityToolkit.WinUI.UI.Controls;

using YT.Data;
using System.Text.RegularExpressions;
using Microsoft.EntityFrameworkCore.Metadata.Internal;
using static Npgsql.Replication.PgOutput.Messages.RelationMessage;
using static Microsoft.EntityFrameworkCore.DbLoggerCategory;
using Microsoft.UI.Xaml.Media.Animation;

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
