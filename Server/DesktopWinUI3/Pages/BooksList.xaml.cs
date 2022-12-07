using CommunityToolkit.WinUI.UI.Controls;
using Microsoft.EntityFrameworkCore;
using Microsoft.UI.Xaml.Controls;
using Microsoft.UI.Xaml.Input;
using System.Collections.ObjectModel;
using System.Linq;
using System.Threading;
using WinRT;
using YT.Data;

namespace DesktopWinUI3;

public sealed partial class BooksList : Page
{
    private DataGridColumn? _sortColumn;

    public BooksList()
    {
        InitializeComponent();

        using (var conn = new PostgresContext())
        {
            var query = conn.Books.AsQueryable();
            UpdateView(query);
        }
    }

    private void BooksListGrid_Sorting(object sender, DataGridColumnEventArgs e)
    {
        _sortColumn = e.Column;
        var direction = _sortColumn.SortDirection;
        string column = (string)_sortColumn.Tag;
        using (var conn = new PostgresContext())
        {
            var query = conn.Books.AsQueryable();
            FilterOnPhrase(SearchInput.Text, ref query);
            SortByColumn(column, direction, _sortColumn, ref query);

            BooksListGrid.ItemsSource = new ObservableCollection<Book>(query);
        }
    }

    private void TextBox_KeyDown(object sender, KeyRoutedEventArgs e)
    {
        if (e.Key != Windows.System.VirtualKey.Enter) return;

        using (var conn = new PostgresContext())
        {
            string phrase = SearchInput.Text.Trim();
            var query = conn.Books.AsQueryable();
            FilterOnPhrase(phrase, ref query);

            if (_sortColumn != null)
            {
                var direction = _sortColumn.SortDirection;
                string column = (string)_sortColumn.Tag;
                SortByColumn(column, direction, _sortColumn, ref query);
            }

            UpdateView(query);
        }
    }

    private void UpdateView(IQueryable<Book> query)
    {
        BooksListGrid.ItemsSource = new ObservableCollection<Book>(query);
        var books = BooksListGrid.ItemsSource.OfType<Book>();
        TotalTextBlock.Text = $"Książek: {books.Count()}";
        TotalPrice.Text = $"Suma: {books.Sum(x => x.Price)}zł";
        Read.Text = $"Przeczytano: {books.Sum(x => x.Bought == null ? 0 : 1)}";
    }

    private void SortByColumn(string column, DataGridSortDirection? direction, DataGridColumn columnObj, ref IQueryable<Book> query)
    {
        foreach (var c in BooksListGrid.Columns)
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

    private void FilterOnPhrase(string phrase, ref IQueryable<Book> query)
    {
        phrase = phrase.Trim();

        if (phrase != null && phrase.Length > 0)
            query = query.Where(p => p.Text.Matches(phrase));

    }


}
