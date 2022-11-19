using Microsoft.EntityFrameworkCore;
using Microsoft.AspNetCore.Http.HttpResults;
using YT_dashboard.API;
namespace YT_dashboard.API.Controllers;

public static class VideoEndpoints
{
    public static void MapVideoEndpoints(this IEndpointRouteBuilder routes)
    {
        var group = routes.MapGroup("/api/Video");

        group.MapGet("/{page}/{howMany}/{phrase?}", async Task<IList<Video>> (int page, int howMany, string? phrase, PostgresContext db) =>
        {
            var query = db.Videos.AsQueryable();

            if (phrase != null && phrase.Trim().Length > 0)
                query = db.Videos.Where(p => p.Document.Matches(phrase));

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
