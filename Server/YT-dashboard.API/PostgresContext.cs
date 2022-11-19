using System;
using System.Collections.Generic;
using Microsoft.EntityFrameworkCore;

namespace YT_dashboard.API;

public partial class PostgresContext : DbContext
{
    public PostgresContext()
    {
    }

    public PostgresContext(DbContextOptions<PostgresContext> options)
        : base(options)
    {
    }

    public virtual DbSet<Video> Videos { get; set; }

    protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
#warning To protect potentially sensitive information in your connection string, you should move it out of source code. You can avoid scaffolding the connection string by using the Name= syntax to read it from configuration - see https://go.microsoft.com/fwlink/?linkid=2131148. For more guidance on storing connection strings, see http://go.microsoft.com/fwlink/?LinkId=723263.
        => optionsBuilder.UseNpgsql("Server=127.0.0.1;Port=5433;Database=postgres;User Id=postgres;Password=.;");

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder
            .HasPostgresExtension("pg_catalog", "adminpack")
            .HasPostgresExtension("pgagent", "pgagent");

        modelBuilder.Entity<Video>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("videos_pkey");

            entity.ToTable("videos");

            entity.HasIndex(e => e.Channel, "channel_index");

            entity.Property(e => e.Id).HasColumnName("id");
            entity.Property(e => e.Acodec).HasColumnName("acodec");
            entity.Property(e => e.AgeLimit).HasColumnName("age_limit");
            entity.Property(e => e.Channel).HasColumnName("channel");
            entity.Property(e => e.ChannelFollowerCount).HasColumnName("channel_follower_count");
            entity.Property(e => e.ChannelUrl).HasColumnName("channel_url");
            entity.Property(e => e.CommentCount).HasColumnName("comment_count");
            entity.Property(e => e.Description).HasColumnName("description");
            entity.Property(e => e.DisplayId).HasColumnName("display_id");
            entity.Property(e => e.Document)
            .HasColumnName("document");
            entity.Property(e => e.Duration).HasColumnName("duration");
            entity.Property(e => e.DurationString).HasColumnName("duration_string");
            entity.Property(e => e.Epoch).HasColumnName("epoch");
            entity.Property(e => e.Ext).HasColumnName("ext");
            entity.Property(e => e.FilesizeApprox).HasColumnName("filesize_approx");
            entity.Property(e => e.Format).HasColumnName("format");
            entity.Property(e => e.Fps).HasColumnName("fps");
            entity.Property(e => e.Fulltitle).HasColumnName("fulltitle");
            entity.Property(e => e.IsLive).HasColumnName("is_live");
            entity.Property(e => e.LikeCount).HasColumnName("like_count");
            entity.Property(e => e.PlaylistId).HasColumnName("playlist_id");
            entity.Property(e => e.PlaylistIndex).HasColumnName("playlist_index");
            entity.Property(e => e.PlaylistTitle).HasColumnName("playlist_title");
            entity.Property(e => e.Tags).HasColumnName("tags");
            entity.Property(e => e.Thumbnail).HasColumnName("thumbnail");
            entity.Property(e => e.Title).HasColumnName("title");
            entity.Property(e => e.UploadDate).HasColumnName("upload_date");
            entity.Property(e => e.UploaderId).HasColumnName("uploader_id");
            entity.Property(e => e.ViewCount).HasColumnName("view_count");
            entity.Property(e => e.WebpageUrl).HasColumnName("webpage_url");
        });

        OnModelCreatingPartial(modelBuilder);
    }

    partial void OnModelCreatingPartial(ModelBuilder modelBuilder);
}
