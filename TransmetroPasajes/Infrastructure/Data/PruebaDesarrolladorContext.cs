using System;
using System.Collections.Generic;
using Core.Entities;
using Core.Entities.SQLContext;
using Microsoft.EntityFrameworkCore;

namespace Infrastructure.Data;

public partial class PruebaDesarrolladorContext : DbContext
{
    public PruebaDesarrolladorContext()
    {
    }

    public PruebaDesarrolladorContext(DbContextOptions<PruebaDesarrolladorContext> options)
        : base(options)
    {
    }

    public virtual DbSet<Pago> Pagos { get; set; }

    public virtual DbSet<Pasaje> Pasajes { get; set; }

    public virtual DbSet<TarjetaVirtual> TarjetaVirtuals { get; set; }

    public virtual DbSet<Usuario> Usuarios { get; set; }

    public virtual DbSet<UsuarioGet> UsuariosGet { get; set; }

	public virtual DbSet<Respuesta> Respuesta { get; set; }

	protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder) { }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.Entity<Pago>(entity =>
        {
            entity.ToTable("Pago");

            entity.Property(e => e.Fecha).HasDefaultValueSql("(sysutcdatetime())");
            entity.Property(e => e.Medio).HasMaxLength(50);
            entity.Property(e => e.Resultado).HasMaxLength(150).HasConversion<string>().IsRequired(); ;

            //entity.HasOne(d => d.Pasaje).WithMany(p => p.Pagos).HasForeignKey(d => d.PasajeId);
        });

        modelBuilder.Entity<Pasaje>(entity =>
        {
            entity.HasKey(e => e.PasajeId).HasName("PK_Pasaje");

            entity.Property(e => e.Codigo).HasMaxLength(250);
            entity.Property(e => e.Estado).HasMaxLength(50).HasConversion<string>().IsRequired(); ;
            entity.Property(e => e.FechaCompra).HasDefaultValueSql("(sysutcdatetime())");
            entity.Property(e => e.MedioPago).HasMaxLength(50).HasConversion<string>().IsRequired(); ;
            entity.Property(e => e.Precio).HasColumnType("numeric(10, 3)");
            entity.Property(e => e.TipoPasaje).HasMaxLength(50).HasConversion<string>().IsRequired(); ;

            /*entity.HasOne(d => d.Usuario).WithMany(p => p.Pasajes)
                .HasForeignKey(d => d.UsuarioId)
                .OnDelete(DeleteBehavior.Cascade);*/
        });

        modelBuilder.Entity<TarjetaVirtual>(entity =>
        {
            entity.ToTable("TarjetaVirtual");

            entity.Property(e => e.FechaEmision).HasDefaultValueSql("(sysutcdatetime())");
            entity.Property(e => e.Numero).HasMaxLength(150);
            entity.Property(e => e.Saldo).HasColumnType("numeric(10, 3)");
            entity.Property(e => e.EstadoTarjeta).HasMaxLength(80);

            //entity.HasOne(d => d.Usuario).WithMany(p => p.TarjetaVirtuals).HasForeignKey(d => d.UsuarioId);
        });

        modelBuilder.Entity<Usuario>(entity =>
        {
            entity.Property(e => e.Clave).HasMaxLength(150);
            entity.Property(e => e.Documento).HasMaxLength(150);
            entity.Property(e => e.Email).HasMaxLength(150);
            entity.Property(e => e.Nombre).HasMaxLength(150);
        });

        modelBuilder.Entity<UsuarioGet>(entity =>
        {
            entity.HasKey(e => e.UsuarioId);
            entity.Property(e => e.Documento).HasMaxLength(150);
            entity.Property(e => e.Email).HasMaxLength(150);
            entity.Property(e => e.Nombre).HasMaxLength(150);
        });

        OnModelCreatingPartial(modelBuilder);
    }

    partial void OnModelCreatingPartial(ModelBuilder modelBuilder);
}
