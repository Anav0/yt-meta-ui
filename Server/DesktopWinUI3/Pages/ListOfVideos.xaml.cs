// Copyright (c) Microsoft Corporation. All rights reserved.
// Licensed under the MIT License. See LICENSE in the project root for license information.

using CommunityToolkit.WinUI.UI.Controls;
using Microsoft.EntityFrameworkCore;
using Microsoft.UI.Xaml.Controls;
using Microsoft.UI.Xaml.Input;
using System.Collections.ObjectModel;
using System.Linq;
using Data;

// To learn more about WinUI, the WinUI project structure,
// and more about our project templates, see: http://aka.ms/winui-project-info.

namespace DesktopWinUI3;

/// <summary>
/// An empty page that can be used on its own or navigated to within a Frame.
/// </summary>
public sealed partial class ListOfVideos : Page
{
    private DataGridColumn? _sortColumn;

    public ListOfVideos()
    {
        InitializeComponent();
        VideosDataGrid.DataContext = new PostgresContext().Videos;

    }
    private void VideosDataGrid_Sorting(object sender, DataGridColumnEventArgs e)
    {
        _sortColumn = e.Column;
        var direction = _sortColumn.SortDirection;
        string column = (string)_sortColumn.Tag;
        using (var conn = new PostgresContext())
        {
            var query = conn.Videos.AsQueryable();
            FilterOnPhrase(SearchInput.Text, ref query);
            SortByColumn(column, direction, _sortColumn, ref query);

            VideosDataGrid.ItemsSource = new ObservableCollection<Video>(query);
        }
    }

    private void TextBox_KeyDown(object sender, KeyRoutedEventArgs e)
    {
        if (e.Key != Windows.System.VirtualKey.Enter) return;

        using (var conn = new PostgresContext())
        {
            string phrase = SearchInput.Text.Trim();
            var query = conn.Videos.AsQueryable();
            FilterOnPhrase(phrase, ref query);

            if (_sortColumn != null)
            {
                var direction = _sortColumn.SortDirection;
                string column = (string)_sortColumn.Tag;
                SortByColumn(column, direction, _sortColumn, ref query);
            }

            VideosDataGrid.ItemsSource = new ObservableCollection<Video>(query);
        }
    }

    private void SortByColumn(string column, DataGridSortDirection? direction, DataGridColumn columnObj, ref IQueryable<Video> query)
    {
        foreach (var c in VideosDataGrid.Columns)
        {
            if (c.Tag.ToString() != column)
                c.SortDirection = null;
        }

        if (direction == null || direction == DataGridSortDirection.Descending)
        {
            query = query.OrderByDescending(o => EF.Property<object>(o, column));
            columnObj.SortDirection = DataGridSortDirection.Ascending;
        }
        else
        {
            query = query.OrderBy(o => EF.Property<object>(o, column));
            columnObj.SortDirection = DataGridSortDirection.Descending;
        }
    }

    private void FilterOnPhrase(string phrase, ref IQueryable<Video> query)
    {
        phrase = phrase.Trim();

        if (phrase != null && phrase.Length > 0)
            query = query.Where(p => p.Document.Matches(phrase));

    }
}
