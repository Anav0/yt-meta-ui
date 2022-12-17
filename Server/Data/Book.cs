using System;
using System.Collections.Generic;
using NpgsqlTypes;

namespace Data;

public partial class Book
{
    public string? Author { get; set; }

    public string Title { get; set; } = null!;

    public decimal? Price { get; set; }

    public DateTime? Bought { get; set; }

    public DateTime? Finished { get; set; }

    public DateTime? Added { get; set; }

    public NpgsqlTsVector? Text { get; set; }

    public int Id { get; set; }
}
