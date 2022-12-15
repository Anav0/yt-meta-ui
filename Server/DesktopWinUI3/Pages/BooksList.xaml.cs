using CommunityToolkit.WinUI.UI.Controls;
using Microsoft.EntityFrameworkCore;
using Microsoft.UI.Xaml.Controls;
using Microsoft.UI.Xaml.Data;
using Microsoft.UI.Xaml.Input;
using System.Collections.ObjectModel;
using System.Linq;
using System.Threading;
using WinRT;
using YT.Data;
using static Microsoft.EntityFrameworkCore.DbLoggerCategory;

namespace DesktopWinUI3;

public sealed partial class BooksList : Page
{
    private DataGridColumn? _sortColumn;

    ObservableCollection<GroupInfoCollection<Book>> GroupedByAuthor;

    public BooksList()
    {
        InitializeComponent();

        DisplayBooks();
    }

    private void DisplayBooks()
    {
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

            BooksListGrid.ItemsSource = new ObservableCollection<Book>(query);

            UpdateInfoBar();
        }
    }

    private void GroupByAuthor()
    {
        using (var conn = new PostgresContext())
        {
            GroupedByAuthor = new ObservableCollection<GroupInfoCollection<Book>>();

            var groups = conn.Books.GroupBy(o => o.Author, (k, os) => os.ToList());

            foreach (var group in groups)
            {
                GroupInfoCollection<Book> info = new GroupInfoCollection<Book>();
                info.Key = group[0].Author;
                foreach (var book in group)
                {
                    info.Add(book);
                }
                GroupedByAuthor.Add(info);
            }

            CollectionViewSource groupedItems = new CollectionViewSource();
            groupedItems.IsSourceGrouped = true;
            groupedItems.Source = GroupedByAuthor;
            BooksListGrid.ItemsSource = groupedItems.View;
            UpdateInfoBar();
        }
    }

    private void BooksListGrid_Sorting(object sender, DataGridColumnEventArgs e)
    {
        _sortColumn = e.Column;
        DisplayBooks();
    }

    private void TextBox_KeyDown(object sender, KeyRoutedEventArgs e)
    {
        if (e.Key != Windows.System.VirtualKey.Enter) return;

        DisplayBooks();
    }

    private void UpdateInfoBar()
    {
        var books = BooksListGrid.ItemsSource.OfType<Book>();
        TotalTextBlock.Text = $"Książek: {books.Count()}";
        TotalPrice.Text = $"Suma: {books.Sum(x => x.Price)}zł";
        Read.Text = $"Przeczytano: {books.Sum(x => x.Finished == null ? 0 : 1)}";
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

    private void BooksListGrid_LoadingRowGroup(object sender, DataGridRowGroupHeaderEventArgs e)
    {
        ICollectionViewGroup group = e.RowGroupHeader.CollectionViewGroup;
        var book = group.GroupItems[0] as Book;
        e.RowGroupHeader.PropertyValue = book.Author;
    }

    private void AddNewBook(object sender, Microsoft.UI.Xaml.RoutedEventArgs e)
    {

    }

    private void Group(object sender, Microsoft.UI.Xaml.RoutedEventArgs e)
    {
        if (GroupButton.IsChecked.Value)
            GroupByAuthor();
        else
            DisplayBooks();
    }
}
