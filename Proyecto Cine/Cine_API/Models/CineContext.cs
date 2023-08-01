using System;
using System.Collections.Generic;
using Microsoft.EntityFrameworkCore;

namespace Cine_API.Models;

public partial class CineContext : DbContext
{
    public CineContext()
    {
    }

    public CineContext(DbContextOptions<CineContext> options)
        : base(options)
    {
    }

    public virtual DbSet<Cliente> Clientes { get; set; }

    public virtual DbSet<Comidum> Comida { get; set; }

    public virtual DbSet<Entrada> Entradas { get; set; }

    public virtual DbSet<Historial> Historials { get; set; }

    public virtual DbSet<Horario> Horarios { get; set; }

    public virtual DbSet<Pedido> Pedidos { get; set; }

    public virtual DbSet<Pelicula> Peliculas { get; set; }

    public virtual DbSet<SalaCine> SalaCines { get; set; }

//    protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
//#warning To protect potentially sensitive information in your connection string, you should move it out of source code. You can avoid scaffolding the connection string by using the Name= syntax to read it from configuration - see https://go.microsoft.com/fwlink/?linkid=2131148. For more guidance on storing connection strings, see http://go.microsoft.com/fwlink/?LinkId=723263.
//        => optionsBuilder.UseSqlServer("Server=.\\SQLEXPRESS; Database=Cine;Trusted_Connection=true;TrustServerCertificate=True;");

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.Entity<Cliente>(entity =>
        {
            entity.HasKey(e => e.IdCliente);

            entity.Property(e => e.IdCliente)
                .ValueGeneratedNever()
                .HasColumnName("ID_Cliente");
            entity.Property(e => e.Apellido)
                .HasMaxLength(50)
                .IsUnicode(false);
            entity.Property(e => e.CorreoElectronico)
                .HasMaxLength(50)
                .IsUnicode(false)
                .HasColumnName("Correo_Electronico");
            entity.Property(e => e.Nombre)
                .HasMaxLength(50)
                .IsUnicode(false);
            entity.Property(e => e.Password)
                .HasMaxLength(200)
                .IsUnicode(false);
        });

        modelBuilder.Entity<Comidum>(entity =>
        {
            entity.HasKey(e => e.IdComida);

            entity.Property(e => e.IdComida)
                .ValueGeneratedNever()
                .HasColumnName("ID_comida");
            entity.Property(e => e.Descripcion)
                .HasMaxLength(200)
                .IsUnicode(false)
                .HasColumnName("descripcion");
            entity.Property(e => e.Fecha).HasColumnType("date");
            entity.Property(e => e.Precio)
                .HasColumnType("money")
                .HasColumnName("precio");
        });

        modelBuilder.Entity<Entrada>(entity =>
        {
            entity.HasKey(e => e.IdEntradas);

            entity.Property(e => e.IdEntradas)
                .ValueGeneratedNever()
                .HasColumnName("ID_Entradas");
            entity.Property(e => e.Asiento)
                .HasMaxLength(10)
                .IsFixedLength();
            entity.Property(e => e.Estado)
                .HasMaxLength(20)
                .IsFixedLength();
            entity.Property(e => e.IdCliente).HasColumnName("ID_Cliente");
            entity.Property(e => e.IdHorario).HasColumnName("ID_Horario");
            entity.Property(e => e.Precio).HasColumnType("money");

            entity.HasOne(d => d.IdClienteNavigation).WithMany(p => p.Entrada)
                .HasForeignKey(d => d.IdCliente)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK_Entradas_Clientes");

            entity.HasOne(d => d.IdHorarioNavigation).WithMany(p => p.Entrada)
                .HasForeignKey(d => d.IdHorario)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK_Entradas_Horario");
        });

        modelBuilder.Entity<Historial>(entity =>
        {
            entity
                .HasNoKey()
                .ToTable("Historial");

            entity.Property(e => e.Fecha).HasColumnType("date");
            entity.Property(e => e.HoraInicio).HasColumnName("Hora_inicio");
            entity.Property(e => e.IdHorario).HasColumnName("ID_Horario");
            entity.Property(e => e.IdPelicula).HasColumnName("ID_Pelicula");
            entity.Property(e => e.IdRegistro).HasColumnName("ID_registro");
            entity.Property(e => e.IdSala).HasColumnName("ID_Sala");

            entity.HasOne(d => d.IdHorarioNavigation).WithMany()
                .HasForeignKey(d => d.IdHorario)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK_Historial_Horario");

            entity.HasOne(d => d.IdPeliculaNavigation).WithMany()
                .HasForeignKey(d => d.IdPelicula)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK_Historial_Peliculas");

            entity.HasOne(d => d.IdSalaNavigation).WithMany()
                .HasForeignKey(d => d.IdSala)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK_Historial_Sala_Cine");
        });

        modelBuilder.Entity<Horario>(entity =>
        {
            entity.HasKey(e => e.IdHorario);

            entity.ToTable("Horario");

            entity.Property(e => e.IdHorario)
                .ValueGeneratedNever()
                .HasColumnName("ID_Horario");
            entity.Property(e => e.HoraInicio).HasColumnName("Hora_Inicio");
            entity.Property(e => e.IdPelicula).HasColumnName("ID_Pelicula");
            entity.Property(e => e.IdSala).HasColumnName("ID_Sala");

            entity.HasOne(d => d.IdPeliculaNavigation).WithMany(p => p.Horarios)
                .HasForeignKey(d => d.IdPelicula)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK_Horario_Peliculas");

            entity.HasOne(d => d.IdSalaNavigation).WithMany(p => p.Horarios)
                .HasForeignKey(d => d.IdSala)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK_Horario_Sala_Cine");
        });

        modelBuilder.Entity<Pedido>(entity =>
        {
            entity
                .HasNoKey()
                .ToTable("Pedido");

            entity.Property(e => e.IdCliente).HasColumnName("ID_Cliente");
            entity.Property(e => e.IdComida).HasColumnName("ID_comida");
            entity.Property(e => e.IdEntradas).HasColumnName("ID_Entradas");
            entity.Property(e => e.Nombre)
                .HasMaxLength(50)
                .IsUnicode(false);

            entity.HasOne(d => d.IdClienteNavigation).WithMany()
                .HasForeignKey(d => d.IdCliente)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK_Pedido_Clientes");

            entity.HasOne(d => d.IdComidaNavigation).WithMany()
                .HasForeignKey(d => d.IdComida)
                .HasConstraintName("FK_Pedido_Comida");

            entity.HasOne(d => d.IdEntradasNavigation).WithMany()
                .HasForeignKey(d => d.IdEntradas)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK_Pedido_Entradas");
        });

        modelBuilder.Entity<Pelicula>(entity =>
        {
            entity.HasKey(e => e.IdPelicula);

            entity.Property(e => e.IdPelicula)
                .ValueGeneratedNever()
                .HasColumnName("ID_Pelicula");
            entity.Property(e => e.Genero)
                .HasMaxLength(20)
                .IsFixedLength();
            entity.Property(e => e.Poster).IsUnicode(false);
            entity.Property(e => e.Sinopsis)
                .HasMaxLength(1000)
                .IsUnicode(false);
            entity.Property(e => e.Titulo)
                .HasMaxLength(20)
                .IsFixedLength();
        });

        modelBuilder.Entity<SalaCine>(entity =>
        {
            entity.HasKey(e => e.IdSala);

            entity.ToTable("Sala_Cine");

            entity.Property(e => e.IdSala)
                .ValueGeneratedNever()
                .HasColumnName("ID_Sala");
            entity.Property(e => e.CapacidadSala).HasColumnName("Capacidad_Sala");
        });

        OnModelCreatingPartial(modelBuilder);
    }

    partial void OnModelCreatingPartial(ModelBuilder modelBuilder);
}
