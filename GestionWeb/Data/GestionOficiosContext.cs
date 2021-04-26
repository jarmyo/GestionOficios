using System;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.Extensions.Configuration;

#nullable disable

namespace GestionWeb.Data
{
    public partial class GestionOficiosContext : DbContext
    {
        public GestionOficiosContext()
        {
           
        }

        public GestionOficiosContext(DbContextOptions<GestionOficiosContext> options)
            : base(options)
        {
        }

        public virtual DbSet<Archiveros> Archiveros { get; set; }
        public virtual DbSet<Cajon> Cajon { get; set; }
        public virtual DbSet<Departamentos> Departamentos { get; set; }
        public virtual DbSet<Emisores> Emisores { get; set; }
        public virtual DbSet<Estados> Estados { get; set; }
        public virtual DbSet<FechasInhabiles> FechasInhabiles { get; set; }
        public virtual DbSet<Oficios> Oficios { get; set; }
        public virtual DbSet<OficiosArchivado> OficiosArchivado { get; set; }
        public virtual DbSet<OficiosEstados> OficiosEstados { get; set; }
        public virtual DbSet<OficiosEstadosNotas> OficiosEstadosNotas { get; set; }
        public virtual DbSet<OficiosUsuarios> OficiosUsuarios { get; set; }
        public virtual DbSet<Receptores> Receptores { get; set; }
        public virtual DbSet<TipoOficio> TipoOficio { get; set; }
        public virtual DbSet<TiposDeEmisor> TiposDeEmisor { get; set; }
        public virtual DbSet<TiposDeTerminos> TiposDeTerminos { get; set; }
        public virtual DbSet<Usuarios> Usuarios { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.HasAnnotation("Relational:Collation", "SQL_Latin1_General_CP1_CI_AS");

            modelBuilder.Entity<Archiveros>(entity =>
            {
                entity.Property(e => e.Nombre)
                    .IsRequired()
                    .HasMaxLength(50)
                    .IsUnicode(false);

                entity.HasOne(d => d.IdDepartamentoNavigation)
                    .WithMany(p => p.Archiveros)
                    .HasForeignKey(d => d.IdDepartamento)
                    .HasConstraintName("FK_Archiveros_Departamentos");
            });

            modelBuilder.Entity<Cajon>(entity =>
            {
                entity.Property(e => e.Nombre)
                    .IsRequired()
                    .HasMaxLength(30)
                    .IsUnicode(false);

                entity.HasOne(d => d.IdArchiveroNavigation)
                    .WithMany(p => p.Cajon)
                    .HasForeignKey(d => d.IdArchivero)
                    .HasConstraintName("FK_Cajon_Archiveros");
            });

            modelBuilder.Entity<Departamentos>(entity =>
            {
                entity.Property(e => e.Nombre)
                    .IsRequired()
                    .HasMaxLength(120)
                    .IsUnicode(false);
            });

            modelBuilder.Entity<Emisores>(entity =>
            {
                entity.Property(e => e.Nombre)
                    .IsRequired()
                    .HasMaxLength(40)
                    .IsUnicode(false);

                entity.HasOne(d => d.IdTipoEmisorNavigation)
                    .WithMany(p => p.Emisores)
                    .HasForeignKey(d => d.IdTipoEmisor)
                    .OnDelete(DeleteBehavior.Cascade)
                    .HasConstraintName("FK_Emisores_Emisores");
            });

            modelBuilder.Entity<Estados>(entity =>
            {
                entity.Property(e => e.Id).ValueGeneratedOnAdd();

                entity.Property(e => e.Nombre)
                    .IsRequired()
                    .HasMaxLength(30)
                    .IsUnicode(false);
            });

            modelBuilder.Entity<FechasInhabiles>(entity =>
            {
                entity.Property(e => e.Descripcion)
                    .HasMaxLength(20)
                    .IsUnicode(false);
            });

            modelBuilder.Entity<Oficios>(entity =>
            {
                entity.Property(e => e.Asunto)
                    .HasMaxLength(60)
                    .IsUnicode(false);

                entity.Property(e => e.FechaRecepcion).HasColumnType("smalldatetime");

                entity.Property(e => e.FechaTermino).HasColumnType("smalldatetime");

                entity.Property(e => e.Numero)
                    .HasMaxLength(6)
                    .IsUnicode(false);

                entity.Property(e => e.Oficio)
                    .IsRequired()
                    .HasMaxLength(40)
                    .IsUnicode(false);

                entity.HasOne(d => d.IdDepartamentoNavigation)
                    .WithMany(p => p.Oficios)
                    .HasForeignKey(d => d.IdDepartamento)
                    .HasConstraintName("FK_Oficios_Departamentos");

                entity.HasOne(d => d.IdEmisorNavigation)
                    .WithMany(p => p.Oficios)
                    .HasForeignKey(d => d.IdEmisor)
                    .HasConstraintName("FK_Oficios_Emisores");

                entity.HasOne(d => d.IdReceptorNavigation)
                    .WithMany(p => p.Oficios)
                    .HasForeignKey(d => d.IdReceptor)
                    .HasConstraintName("FK_Oficios_Receptores");

                entity.HasOne(d => d.IdTipoNavigation)
                    .WithMany(p => p.Oficios)
                    .HasForeignKey(d => d.IdTipo)
                    .OnDelete(DeleteBehavior.Cascade)
                    .HasConstraintName("FK_Oficios_TipoOficio");
            });

            modelBuilder.Entity<OficiosArchivado>(entity =>
            {
                entity.Property(e => e.Id).ValueGeneratedNever();

                entity.HasOne(d => d.IdNavigation)
                    .WithOne(p => p.OficiosArchivado)
                    .HasForeignKey<OficiosArchivado>(d => d.Id)
                    .HasConstraintName("FK_OficiosArchivado_Oficios");

                entity.HasOne(d => d.IdCajonNavigation)
                    .WithMany(p => p.OficiosArchivado)
                    .HasForeignKey(d => d.IdCajon)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_OficiosArchivado_Cajon");
            });

            modelBuilder.Entity<OficiosEstados>(entity =>
            {
                entity.Property(e => e.FechaHora).HasColumnType("smalldatetime");

                entity.HasOne(d => d.IdEstadoNavigation)
                    .WithMany(p => p.OficiosEstados)
                    .HasForeignKey(d => d.IdEstado)
                    .HasConstraintName("FK_OficiosEstados_Estados");

                entity.HasOne(d => d.IdOficioNavigation)
                    .WithMany(p => p.OficiosEstados)
                    .HasForeignKey(d => d.IdOficio)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_OficiosEstados_Oficios");

                entity.HasOne(d => d.IdUsuarioNavigation)
                    .WithMany(p => p.OficiosEstados)
                    .HasForeignKey(d => d.IdUsuario)
                    .HasConstraintName("FK_OficiosEstados_Usuarios");
            });

            modelBuilder.Entity<OficiosEstadosNotas>(entity =>
            {
                entity.Property(e => e.FechaHora).HasColumnType("smalldatetime");

                entity.Property(e => e.Nota)
                    .IsRequired()
                    .HasColumnType("text");

                entity.HasOne(d => d.IdEstadoOficioNavigation)
                    .WithMany(p => p.OficiosEstadosNotas)
                    .HasForeignKey(d => d.IdEstadoOficio)
                    .HasConstraintName("FK_OficiosEstadosNotas_OficiosEstados");
            });

            modelBuilder.Entity<OficiosUsuarios>(entity =>
            {
                entity.HasOne(d => d.IdOficioNavigation)
                    .WithMany(p => p.OficiosUsuarios)
                    .HasForeignKey(d => d.IdOficio)
                    .HasConstraintName("FK_OficiosUsuarios_Oficios");

                entity.HasOne(d => d.IdUsuarioNavigation)
                    .WithMany(p => p.OficiosUsuarios)
                    .HasForeignKey(d => d.IdUsuario)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_OficiosUsuarios_Usuarios");
            });

            modelBuilder.Entity<Receptores>(entity =>
            {
                entity.Property(e => e.Nombre)
                    .IsRequired()
                    .HasMaxLength(40)
                    .IsUnicode(false);
            });

            modelBuilder.Entity<TipoOficio>(entity =>
            {
                entity.Property(e => e.Id).ValueGeneratedOnAdd();

                entity.Property(e => e.Nombre)
                    .IsRequired()
                    .HasMaxLength(40)
                    .IsUnicode(false);
            });

            modelBuilder.Entity<TiposDeEmisor>(entity =>
            {
                entity.Property(e => e.Id).ValueGeneratedOnAdd();

                entity.Property(e => e.Nombre)
                    .IsRequired()
                    .HasMaxLength(30)
                    .IsUnicode(false);
            });

            modelBuilder.Entity<TiposDeTerminos>(entity =>
            {
                entity.Property(e => e.Nombre)
                    .IsRequired()
                    .HasMaxLength(30)
                    .IsUnicode(false);
            });

            modelBuilder.Entity<Usuarios>(entity =>
            {
                entity.Property(e => e.AspNetId)
                    .IsRequired()
                    .HasMaxLength(256);

                entity.Property(e => e.Email)
                    .HasMaxLength(256)
                    .IsUnicode(false);

                entity.Property(e => e.Nombre)
                    .IsRequired()
                    .HasMaxLength(60)
                    .IsUnicode(false);

                entity.HasOne(d => d.IdDepartamentoNavigation)
                    .WithMany(p => p.Usuarios)
                    .HasForeignKey(d => d.IdDepartamento)
                    .HasConstraintName("FK_Usuarios_Departamentos");
            });

            OnModelCreatingPartial(modelBuilder);
        }

        partial void OnModelCreatingPartial(ModelBuilder modelBuilder);
    }
}
