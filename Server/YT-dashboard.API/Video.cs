using System;
using System.Collections.Generic;
using NpgsqlTypes;
using Microsoft.AspNetCore.Http.HttpResults;

namespace YT_dashboard.API;

public partial class Video
{
    public string Id { get; set; } = null!;

    public string? WebpageUrl { get; set; }

    public bool? IsLive { get; set; }

    public short? AgeLimit { get; set; }

    public string? UploaderId { get; set; }

    public string? Channel { get; set; }

    public string ChannelUrl { get; set; } = null!;

    public long? CommentCount { get; set; }

    public long LikeCount { get; set; }

    public long? ChannelFollowerCount { get; set; }

    public string? PlaylistId { get; set; }

    public string? PlaylistTitle { get; set; }

    public int? PlaylistIndex { get; set; }

    public string? DisplayId { get; set; }

    public long? ViewCount { get; set; }

    public string? Acodec { get; set; }

    public string? Fulltitle { get; set; }

    public string? Title { get; set; }

    public string? Description { get; set; }

    public string? Format { get; set; }

    public double? Fps { get; set; }

    public string? Tags { get; set; }

    public string? Thumbnail { get; set; }

    public DateOnly? UploadDate { get; set; }

    public string? Ext { get; set; }

    public int? Duration { get; set; }

    public string? DurationString { get; set; }

    public long? FilesizeApprox { get; set; }

    public long? Epoch { get; set; }

    public NpgsqlTsVector? Document { get; set; }
}