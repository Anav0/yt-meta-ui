using Microsoft.EntityFrameworkCore;
using Data;
namespace YT.HTTP;

public static class VideoEndpoints
{
    public static void MapVideoEndpoints(this IEndpointRouteBuilder routes)
    {
        var group = routes.MapGroup("/api/Video");

        group.MapGet("/{page}/{howMany}/{order}/{column}/{phrase?}", async Task<IList<Video>> (int page, int howMany, string order, string column, string? phrase, PostgresContext db) =>
        {
            var query = db.Videos.AsQueryable();

            if (phrase != null && phrase.Trim().Length > 0)
            {
                query = db.Videos.Where(p => p.Document.Matches(phrase));
            }

            if (order == "desc")
                query = query.OrderByDescending(o => EF.Property<object>(o, column));
            else
                query = query.OrderBy(o => EF.Property<object>(o, column));


            return await query.Skip(page * howMany).Take(howMany).ToListAsync();
        })
        .WithName("GetOrderedVideoForPage");

        group.MapGet("/{page}/{howMany}/{phrase?}", async Task<IList<Video>> (int page, int howMany, string? order, string? column, string? phrase, PostgresContext db) =>
        {
            var query = db.Videos.AsQueryable();

            if (phrase != null && phrase.Trim().Length > 0)
            {
                query = db.Videos.Where(p => p.Document.Matches(phrase));
            }

            return await query.Skip(page * howMany).Take(howMany).ToListAsync();
        })
       .WithName("GetVideoForPage");

        group.MapGet("/total", async Task<int> (PostgresContext db) =>
        {
            return await db.Videos.CountAsync();
        })
      .WithName("GetTotal");

    }
}
