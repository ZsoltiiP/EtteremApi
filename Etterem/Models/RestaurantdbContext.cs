using System;
using System.Collections.Generic;
using Microsoft.EntityFrameworkCore;

namespace Etterem.Models;

public partial class RestaurantdbContext : DbContext
{
    public RestaurantdbContext()
    {
    }

    public RestaurantdbContext(DbContextOptions<RestaurantdbContext> options)
        : base(options)
    {
    }

    public virtual DbSet<Kapcsolo> Kapcsolos { get; set; }

    public virtual DbSet<Rendele> Rendeles { get; set; }

    public virtual DbSet<Termekek> Termekeks { get; set; }

    protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
#warning To protect potentially sensitive information in your connection string, you should move it out of source code. You can avoid scaffolding the connection string by using the Name= syntax to read it from configuration - see https://go.microsoft.com/fwlink/?linkid=2131148. For more guidance on storing connection strings, see https://go.microsoft.com/fwlink/?LinkId=723263.
        => optionsBuilder.UseMySQL("server=localhost;database=restaurantdb;user=root;password=;");

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.Entity<Kapcsolo>(entity =>
        {
            entity.HasKey(e => e.Kapcsoloid).HasName("PRIMARY");

            entity.ToTable("kapcsolo");

            entity.Property(e => e.Kapcsoloid)
                .HasColumnType("int(11)")
                .HasColumnName("kapcsoloid");
            entity.Property(e => e.Rendelesid)
                .HasDefaultValueSql("'NULL'")
                .HasColumnType("int(11)")
                .HasColumnName("rendelesid");
            entity.Property(e => e.Termekekid)
                .HasDefaultValueSql("'NULL'")
                .HasColumnType("int(11)")
                .HasColumnName("termekekid");
        });

        modelBuilder.Entity<Rendele>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("PRIMARY");

            entity.ToTable("rendeles");

            entity.Property(e => e.Id)
                .HasColumnType("int(11)")
                .HasColumnName("id");
            entity.Property(e => e.Asztalszam)
                .HasDefaultValueSql("'NULL'")
                .HasColumnType("int(11)")
                .HasColumnName("asztalszam");
            entity.Property(e => e.Fizetesimod)
                .HasMaxLength(50)
                .HasDefaultValueSql("'NULL'")
                .HasColumnName("fizetesimod");
        });

        modelBuilder.Entity<Termekek>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("PRIMARY");

            entity.ToTable("termekek");

            entity.Property(e => e.Id)
                .HasColumnType("int(11)")
                .HasColumnName("id");
            entity.Property(e => e.Ar)
                .HasDefaultValueSql("'NULL'")
                .HasColumnType("int(11)")
                .HasColumnName("ar");
            entity.Property(e => e.Etel)
                .HasMaxLength(100)
                .HasDefaultValueSql("'NULL'")
                .HasColumnName("etel");
        });

        OnModelCreatingPartial(modelBuilder);
    }

    partial void OnModelCreatingPartial(ModelBuilder modelBuilder);
}
