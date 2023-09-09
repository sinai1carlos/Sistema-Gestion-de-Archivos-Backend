using System;
using System.Collections.Generic;
using Microsoft.EntityFrameworkCore;
using Sistema_GestionArchivos_Backend.Models;

namespace Sistema_GestionArchivos_Backend.Repositories;

public partial class ArchivosdbContext : DbContext
{
    public ArchivosdbContext()
    {
    }

    public ArchivosdbContext(DbContextOptions<ArchivosdbContext> options)
        : base(options)
    {
    }

    public virtual DbSet<Archivo> Archivos { get; set; }

    protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder) { }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.Entity<Archivo>(entity =>
        {
            entity.HasKey(e => e.IdArchivo).HasName("PK__ARCHIVOS__26B9211126662F45");

            entity.ToTable("ARCHIVOS");

            entity.Property(e => e.Nombre)
                .HasMaxLength(50)
                .IsUnicode(false);
            entity.Property(e => e.Ruta)
                .HasMaxLength(100)
                .IsUnicode(false);
        });

        OnModelCreatingPartial(modelBuilder);
    }

    partial void OnModelCreatingPartial(ModelBuilder modelBuilder);
}
