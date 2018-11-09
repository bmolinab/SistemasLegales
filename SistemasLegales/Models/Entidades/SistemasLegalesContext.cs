using System;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata;

namespace SistemasLegales.Models.Entidades
{
    public partial class SistemasLegalesContext : IdentityDbContext<ApplicationUser>
    {
        public virtual DbSet<Accion> Accion { get; set; }
        public virtual DbSet<Actor> Actor { get; set; }
        public virtual DbSet<Requisito> Requisito { get; set; }
        public virtual DbSet<Ciudad> Ciudad { get; set; }
        public virtual DbSet<Documento> Documento { get; set; }
        public virtual DbSet<DocumentoRequisito> DocumentoRequisito { get; set; }
        public virtual DbSet<OrganismoControl> OrganismoControl { get; set; }
        public virtual DbSet<Proceso> Proceso { get; set; }
        public virtual DbSet<Proyecto> Proyecto { get; set; }
        public virtual DbSet<RequisitoLegal> RequisitoLegal { get; set; }
        public virtual DbSet<Status> Status { get; set; }

        public SistemasLegalesContext(DbContextOptions<SistemasLegalesContext> options)
            : base(options)
        { }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);
            modelBuilder.Entity<Accion>(entity =>
            {
                entity.HasKey(e => e.IdAccion)
                    .HasName("PK_Accion");

                entity.Property(e => e.IdAccion).ValueGeneratedNever();

                entity.Property(e => e.Detalle).HasColumnType("text");

                entity.HasOne(d => d.IdRequisitoNavigation)
                    .WithMany(p => p.Accion)
                    .HasForeignKey(d => d.IdRequisito)
                    .HasConstraintName("FK_Accion_Requisito");
            });

            modelBuilder.Entity<Actor>(entity =>
            {
                entity.HasKey(e => e.IdActor)
                    .HasName("PK_Actor");

                entity.Property(e => e.IdActor).ValueGeneratedNever();

                entity.Property(e => e.Departamento).HasColumnType("varchar(200)");

                entity.Property(e => e.Email).HasColumnType("varchar(200)");

                entity.Property(e => e.Nombres)
                    .IsRequired()
                    .HasColumnType("varchar(200)");
            });


            modelBuilder.Entity<Ciudad>(entity =>
            {
                entity.HasKey(e => e.IdCiudad)
                    .HasName("PK_Ciudad");

                entity.Property(e => e.IdCiudad).ValueGeneratedNever();

                entity.Property(e => e.Nombre)
                    .IsRequired()
                    .HasColumnType("varchar(200)");
            });

            modelBuilder.Entity<Documento>(entity =>
            {
                entity.HasKey(e => e.IdDocumento)
                    .HasName("PK_Documento");

                entity.Property(e => e.IdDocumento).ValueGeneratedNever();

                entity.Property(e => e.Cantidad).HasDefaultValueSql("0");

                entity.Property(e => e.Nombre)
                    .IsRequired()
                    .HasColumnType("varchar(1000)");

                entity.Property(e => e.Tipo).HasDefaultValueSql("1");

                entity.HasOne(d => d.RequisitoLegal)
                      .WithMany(p => p.Documento)
                      .HasForeignKey(d => d.IdRequisitoLegal)
                      .HasConstraintName("FK_Documento_RequisitoLegal");
            });

            modelBuilder.Entity<DocumentoRequisito>(entity =>
            {
                entity.HasKey(e => e.IdDocumentoRequisito)
                    .HasName("PK_DocumentoRequisito");

                entity.Property(e => e.Nombre)
                    .IsRequired()
                    .HasColumnType("varchar(200)");

                entity.Property(e => e.Url).HasColumnType("varchar(1024)");

                entity.HasOne(d => d.Requisito)
                     .WithMany(p => p.DocumentoRequisito)
                     .HasForeignKey(d => d.IdRequisito)
                     .OnDelete(DeleteBehavior.Cascade)
                     .HasConstraintName("FK_DocumentoRequisito_Requisito");
            });


            modelBuilder.Entity<OrganismoControl>(entity =>
            {
                entity.HasKey(e => e.IdOrganismoControl)
                    .HasName("PK_OrganismoControl");

                entity.Property(e => e.IdOrganismoControl).ValueGeneratedNever();

                entity.Property(e => e.Nombre)
                    .IsRequired()
                    .HasColumnType("varchar(1000)");
            });

            modelBuilder.Entity<Proceso>(entity =>
            {
                entity.HasKey(e => e.IdProceso)
                    .HasName("PK_Proceso");

                entity.Property(e => e.IdProceso).ValueGeneratedNever();

                entity.Property(e => e.Nombre)
                    .IsRequired()
                    .HasColumnType("varchar(200)");
            });

            modelBuilder.Entity<Proyecto>(entity =>
            {
                entity.HasKey(e => e.IdProyecto)
                    .HasName("PK_Proyecto");

                entity.Property(e => e.IdProyecto).ValueGeneratedNever();

                entity.Property(e => e.Nombre).HasColumnType("varchar(250)");
            });

            modelBuilder.Entity<Requisito>(entity =>
            {
                entity.HasKey(e => e.IdRequisito)
                    .HasName("PK_AdminRequisitoLegal");

                entity.Property(e => e.IdRequisito).ValueGeneratedNever();

                entity.Property(e => e.Criticidad).HasDefaultValueSql("0");

                entity.Property(e => e.EmailNotificacion1).HasColumnType("varchar(100)");

                entity.Property(e => e.EmailNotificacion2).HasColumnType("varchar(100)");

                entity.Property(e => e.Finalizado).HasDefaultValueSql("0");

                entity.Property(e => e.Habilitado).HasDefaultValueSql("1");

                entity.Property(e => e.NotificacionEnviadaUltima).HasDefaultValueSql("0");

                entity.Property(e => e.Observaciones).HasColumnType("text");

                entity.HasOne(d => d.ActorCustodioDocumento)
                    .WithMany(p => p.AdminRequisitoLegalIdActorCustodioDocumento)
                    .HasForeignKey(d => d.IdActorCustodioDocumento)
                    .OnDelete(DeleteBehavior.Restrict)
                    .HasConstraintName("FK_AdminRequisitoLegal_Actor2");

                entity.HasOne(d => d.ActorDuennoProceso)
                    .WithMany(p => p.AdminRequisitoLegalIdActorDuennoProceso)
                    .HasForeignKey(d => d.IdActorDuennoProceso)
                    .OnDelete(DeleteBehavior.Restrict)
                    .HasConstraintName("FK_AdminRequisitoLegal_Actor");

                entity.HasOne(d => d.ActorResponsableGestSeg)
                    .WithMany(p => p.AdminRequisitoLegalIdActorResponsableGestSeg)
                    .HasForeignKey(d => d.IdActorResponsableGestSeg)
                    .OnDelete(DeleteBehavior.Restrict)
                    .HasConstraintName("FK_AdminRequisitoLegal_Actor1");

                entity.HasOne(d => d.Ciudad)
                    .WithMany(p => p.AdminRequisitoLegal)
                    .HasForeignKey(d => d.IdCiudad)
                    .HasConstraintName("FK_AdminRequisitoLegal_Ciudad");

                entity.HasOne(d => d.Documento)
                    .WithMany(p => p.Requisito)
                    .HasForeignKey(d => d.IdDocumento)
                    .HasConstraintName("FK_Requisito_Documento");

                entity.HasOne(d => d.Proceso)
                    .WithMany(p => p.AdminRequisitoLegal)
                    .HasForeignKey(d => d.IdProceso)
                    .HasConstraintName("FK_AdminRequisitoLegal_Proceso");

                entity.HasOne(d => d.Proyecto)
                  .WithMany(p => p.Requisito)
                  .HasForeignKey(d => d.IdProyecto)
                    .HasConstraintName("FK_Requisito_Proyecto");

                entity.HasOne(d => d.Status)
                    .WithMany(p => p.AdminRequisitoLegal)
                    .HasForeignKey(d => d.IdStatus)
                    .HasConstraintName("FK_AdminRequisitoLegal_Status");
            });

            modelBuilder.Entity<RequisitoLegal>(entity =>
            {
                entity.HasKey(e => e.IdRequisitoLegal)
                    .HasName("PK_RequisitoLegal");

                entity.Property(e => e.IdRequisitoLegal).ValueGeneratedNever();

                entity.Property(e => e.Nombre)
                    .IsRequired()
                    .HasColumnType("varchar(1000)");

                entity.HasOne(d => d.OrganismoControl)
                   .WithMany(p => p.RequisitoLegal)
                   .HasForeignKey(d => d.IdOrganismoControl)
                   .HasConstraintName("FK_RequisitoLegal_OrganismoControl");
            });






            modelBuilder.Entity<Status>(entity =>
            {
                entity.HasKey(e => e.IdStatus)
                    .HasName("PK_Status");

                entity.Property(e => e.IdStatus).ValueGeneratedNever();

                entity.Property(e => e.Nombre)
                    .IsRequired()
                    .HasColumnType("varchar(100)");
            });
        }
    }
}