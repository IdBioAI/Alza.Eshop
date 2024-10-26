using System;
using System.Collections.Generic;
using Alza.DbContexts.Models;
using Microsoft.EntityFrameworkCore;

namespace Alza.DbContexts;

public partial class ApplicationDbContext : DbContext
{
    public ApplicationDbContext()
    {
    }

    public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options)
        : base(options)
    {
    }

    public virtual DbSet<Product> Products { get; set; }

    protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        => optionsBuilder.UseSqlServer("Name=AlzaShopDbContext");

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.UseCollation("Czech_CI_AS");

        modelBuilder.Entity<Product>(entity =>
        {
            entity.HasNoKey();

            entity.HasIndex(e => e.Id, "PK_id").IsUnique();

            entity.Property(e => e.Description).UseCollation("SQL_Latin1_General_CP1_CI_AS");
            entity.Property(e => e.ImgUri)
                .HasMaxLength(2048)
                .UseCollation("SQL_Latin1_General_CP1_CI_AS");
            entity.Property(e => e.Name)
                .HasMaxLength(255)
                .UseCollation("SQL_Latin1_General_CP1_CI_AS");
            entity.Property(e => e.Price).HasColumnType("decimal(18, 2)");
        });

        OnModelCreatingPartial(modelBuilder);
    }

    partial void OnModelCreatingPartial(ModelBuilder modelBuilder);
}
