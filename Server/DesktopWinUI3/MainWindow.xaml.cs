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

namespace DesktopWinUI3
{
    public class GroupInfoCollection<T> : ObservableCollection<T>
    {
        public string Key { get; set; }
    }

    public sealed partial class MainWindow : Window
    {
        public MainWindow()
        {
            this.InitializeComponent();

            VideosDataGrid.DataContext = new PostgresContext().Videos;
        }

        private void VideosDataGrid_Sorting(object sender, DataGridColumnEventArgs e)
        {
            var direction = e.Column.SortDirection;
            var column = (string)e.Column.Tag;

            // Remove sorting indicators from other columns
            foreach (var c in VideosDataGrid.Columns)
            {
                if (c.Tag.ToString() != column)
                    c.SortDirection = null;
            }

            using (var conn = new PostgresContext())
            {
                var query = conn.Videos.AsQueryable();

                if (direction == null || direction == DataGridSortDirection.Descending)
                {
                    query = query.OrderByDescending(o => EF.Property<object>(o, column));
                    e.Column.SortDirection = DataGridSortDirection.Ascending;
                }
                else
                {
                    query = query.OrderBy(o => EF.Property<object>(o, column));
                    e.Column.SortDirection = DataGridSortDirection.Descending;
                }

                VideosDataGrid.ItemsSource = new ObservableCollection<Video>(query);
            }

        }

        //private void OnGroupBtnClick(object sender, RoutedEventArgs e)
        //{
        //    ObservableCollection<GroupInfoCollection<Video>> groups = new();

        //    Dictionary<string, List<Video>> dict = new();
        //    VideosDataGrid.DataContext = null;

        //    foreach (var video in postgresContext.Videos)
        //    {
        //        if (dict.TryGetValue(video.Channel, out var group))
        //            group.Add(video);
        //        else
        //            dict.Add(video.Channel, new List<Video>() { video });

        //    }

        //    foreach (var g in dict)
        //    {
        //        GroupInfoCollection<Video> info = new();
        //        info.Key = g.Key;
        //        foreach (var item in g.Value)
        //            info.Add(item);

        //        groups.Add(info);
        //    }

        //    grouped = new CollectionViewSource();
        //    grouped.IsSourceGrouped = true;
        //    grouped.Source = groups;

        //    VideosDataGrid.DataContext = grouped.View;
        //}

        //private void VideosDataGrid_LoadingRowGroup(object sender, DataGridRowGroupHeaderEventArgs e)
        //{

        //}
    }
}
