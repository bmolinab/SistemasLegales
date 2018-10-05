using System;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Migrations;
using SistemasLegales.Models.Entidades;

namespace SistemasLegales.Migrations
{
    [DbContext(typeof(SistemasLegalesContext))]
    partial class SistemasLegalesContextModelSnapshot : ModelSnapshot
    {
        protected override void BuildModel(ModelBuilder modelBuilder)
        {
            modelBuilder
                .HasAnnotation("ProductVersion", "1.1.5")
                .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

            modelBuilder.Entity("Microsoft.AspNetCore.Identity.EntityFrameworkCore.IdentityRole", b =>
                {
                    b.Property<string>("Id")
                        .ValueGeneratedOnAdd();

                    b.Property<string>("ConcurrencyStamp")
                        .IsConcurrencyToken();

                    b.Property<string>("Name")
                        .HasMaxLength(256);

                    b.Property<string>("NormalizedName")
                        .HasMaxLength(256);

                    b.HasKey("Id");

                    b.HasIndex("NormalizedName")
                        .IsUnique()
                        .HasName("RoleNameIndex");

                    b.ToTable("AspNetRoles");
                });

            modelBuilder.Entity("Microsoft.AspNetCore.Identity.EntityFrameworkCore.IdentityRoleClaim<string>", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd();

                    b.Property<string>("ClaimType");

                    b.Property<string>("ClaimValue");

                    b.Property<string>("RoleId")
                        .IsRequired();

                    b.HasKey("Id");

                    b.HasIndex("RoleId");

                    b.ToTable("AspNetRoleClaims");
                });

            modelBuilder.Entity("Microsoft.AspNetCore.Identity.EntityFrameworkCore.IdentityUserClaim<string>", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd();

                    b.Property<string>("ClaimType");

                    b.Property<string>("ClaimValue");

                    b.Property<string>("UserId")
                        .IsRequired();

                    b.HasKey("Id");

                    b.HasIndex("UserId");

                    b.ToTable("AspNetUserClaims");
                });

            modelBuilder.Entity("Microsoft.AspNetCore.Identity.EntityFrameworkCore.IdentityUserLogin<string>", b =>
                {
                    b.Property<string>("LoginProvider");

                    b.Property<string>("ProviderKey");

                    b.Property<string>("ProviderDisplayName");

                    b.Property<string>("UserId")
                        .IsRequired();

                    b.HasKey("LoginProvider", "ProviderKey");

                    b.HasIndex("UserId");

                    b.ToTable("AspNetUserLogins");
                });

            modelBuilder.Entity("Microsoft.AspNetCore.Identity.EntityFrameworkCore.IdentityUserRole<string>", b =>
                {
                    b.Property<string>("UserId");

                    b.Property<string>("RoleId");

                    b.HasKey("UserId", "RoleId");

                    b.HasIndex("RoleId");

                    b.ToTable("AspNetUserRoles");
                });

            modelBuilder.Entity("Microsoft.AspNetCore.Identity.EntityFrameworkCore.IdentityUserToken<string>", b =>
                {
                    b.Property<string>("UserId");

                    b.Property<string>("LoginProvider");

                    b.Property<string>("Name");

                    b.Property<string>("Value");

                    b.HasKey("UserId", "LoginProvider", "Name");

                    b.ToTable("AspNetUserTokens");
                });

            modelBuilder.Entity("SistemasLegales.Models.Entidades.Actor", b =>
                {
                    b.Property<int>("IdActor")
                        .ValueGeneratedOnAdd();

                    b.Property<string>("Departamento")
                        .IsRequired()
                        .HasColumnType("varchar(200)")
                        .HasMaxLength(200);

                    b.Property<string>("Email")
                        .IsRequired()
                        .HasColumnType("varchar(200)");

                    b.Property<string>("Nombres")
                        .IsRequired()
                        .HasColumnType("varchar(200)")
                        .HasMaxLength(200);

                    b.HasKey("IdActor")
                        .HasName("PK_Actor");

                    b.ToTable("Actor");
                });

            modelBuilder.Entity("SistemasLegales.Models.Entidades.ApplicationUser", b =>
                {
                    b.Property<string>("Id")
                        .ValueGeneratedOnAdd();

                    b.Property<int>("AccessFailedCount");

                    b.Property<string>("ConcurrencyStamp")
                        .IsConcurrencyToken();

                    b.Property<string>("Email")
                        .HasMaxLength(256);

                    b.Property<bool>("EmailConfirmed");

                    b.Property<bool>("LockoutEnabled");

                    b.Property<DateTimeOffset?>("LockoutEnd");

                    b.Property<string>("NormalizedEmail")
                        .HasMaxLength(256);

                    b.Property<string>("NormalizedUserName")
                        .HasMaxLength(256);

                    b.Property<string>("PasswordHash");

                    b.Property<string>("PhoneNumber");

                    b.Property<bool>("PhoneNumberConfirmed");

                    b.Property<string>("SecurityStamp");

                    b.Property<bool>("TwoFactorEnabled");

                    b.Property<string>("UserName")
                        .HasMaxLength(256);

                    b.HasKey("Id");

                    b.HasIndex("NormalizedEmail")
                        .HasName("EmailIndex");

                    b.HasIndex("NormalizedUserName")
                        .IsUnique()
                        .HasName("UserNameIndex");

                    b.ToTable("AspNetUsers");
                });

            modelBuilder.Entity("SistemasLegales.Models.Entidades.Ciudad", b =>
                {
                    b.Property<int>("IdCiudad")
                        .ValueGeneratedOnAdd();

                    b.Property<string>("Nombre")
                        .IsRequired()
                        .HasColumnType("varchar(200)")
                        .HasMaxLength(200);

                    b.HasKey("IdCiudad")
                        .HasName("PK_Ciudad");

                    b.ToTable("Ciudad");
                });

            modelBuilder.Entity("SistemasLegales.Models.Entidades.Documento", b =>
                {
                    b.Property<int>("IdDocumento")
                        .ValueGeneratedOnAdd();

                    b.Property<int>("IdRequisitoLegal");

                    b.Property<string>("Nombre")
                        .IsRequired()
                        .HasColumnType("varchar(200)")
                        .HasMaxLength(200);

                    b.HasKey("IdDocumento")
                        .HasName("PK_Documento");

                    b.HasIndex("IdRequisitoLegal");

                    b.ToTable("Documento");
                });

            modelBuilder.Entity("SistemasLegales.Models.Entidades.DocumentoRequisito", b =>
                {
                    b.Property<int>("IdDocumentoRequisito")
                        .ValueGeneratedOnAdd();

                    b.Property<DateTime>("Fecha");

                    b.Property<int?>("IdRequisito");

                    b.Property<string>("Nombre")
                        .IsRequired()
                        .HasColumnType("varchar(200)")
                        .HasMaxLength(200);

                    b.Property<string>("Url")
                        .IsRequired()
                        .HasColumnType("varchar(1024)")
                        .HasMaxLength(1024);

                    b.HasKey("IdDocumentoRequisito")
                        .HasName("PK_DocumentoRequisito");

                    b.HasIndex("IdRequisito");

                    b.ToTable("DocumentoRequisito");
                });

            modelBuilder.Entity("SistemasLegales.Models.Entidades.OrganismoControl", b =>
                {
                    b.Property<int>("IdOrganismoControl")
                        .ValueGeneratedOnAdd();

                    b.Property<string>("Nombre")
                        .IsRequired()
                        .HasColumnType("varchar(200)")
                        .HasMaxLength(200);

                    b.HasKey("IdOrganismoControl")
                        .HasName("PK_OrganismoControl");

                    b.ToTable("OrganismoControl");
                });

            modelBuilder.Entity("SistemasLegales.Models.Entidades.Proceso", b =>
                {
                    b.Property<int>("IdProceso")
                        .ValueGeneratedOnAdd();

                    b.Property<string>("Nombre")
                        .IsRequired()
                        .HasColumnType("varchar(200)")
                        .HasMaxLength(200);

                    b.HasKey("IdProceso")
                        .HasName("PK_Proceso");

                    b.ToTable("Proceso");
                });

            modelBuilder.Entity("SistemasLegales.Models.Entidades.Requisito", b =>
                {
                    b.Property<int>("IdRequisito")
                        .ValueGeneratedOnAdd();

                    b.Property<int>("DiasNotificacion");

                    b.Property<int>("DuracionTramite");

                    b.Property<string>("EmailNotificacion1")
                        .IsRequired()
                        .HasColumnType("varchar(100)")
                        .HasMaxLength(100);

                    b.Property<string>("EmailNotificacion2")
                        .IsRequired()
                        .HasColumnType("varchar(100)")
                        .HasMaxLength(100);

                    b.Property<DateTime?>("FechaCaducidad");

                    b.Property<DateTime>("FechaCumplimiento");

                    b.Property<int>("IdActorCustodioDocumento");

                    b.Property<int>("IdActorDuennoProceso");

                    b.Property<int>("IdActorResponsableGestSeg");

                    b.Property<int>("IdCiudad");

                    b.Property<int>("IdDocumento");

                    b.Property<int>("IdProceso");

                    b.Property<int>("IdStatus");

                    b.Property<bool>("NotificacionEnviada");

                    b.Property<string>("Observaciones")
                        .HasColumnType("varchar(1000)")
                        .HasMaxLength(1000);

                    b.HasKey("IdRequisito")
                        .HasName("PK_AdminRequisitoLegal");

                    b.HasIndex("IdActorCustodioDocumento");

                    b.HasIndex("IdActorDuennoProceso");

                    b.HasIndex("IdActorResponsableGestSeg");

                    b.HasIndex("IdCiudad");

                    b.HasIndex("IdDocumento");

                    b.HasIndex("IdProceso");

                    b.HasIndex("IdStatus");

                    b.ToTable("Requisito");
                });

            modelBuilder.Entity("SistemasLegales.Models.Entidades.RequisitoLegal", b =>
                {
                    b.Property<int>("IdRequisitoLegal")
                        .ValueGeneratedOnAdd();

                    b.Property<int>("IdOrganismoControl");

                    b.Property<string>("Nombre")
                        .IsRequired()
                        .HasColumnType("varchar(200)")
                        .HasMaxLength(200);

                    b.HasKey("IdRequisitoLegal")
                        .HasName("PK_RequisitoLegal");

                    b.HasIndex("IdOrganismoControl");

                    b.ToTable("RequisitoLegal");
                });

            modelBuilder.Entity("SistemasLegales.Models.Entidades.Status", b =>
                {
                    b.Property<int>("IdStatus")
                        .ValueGeneratedOnAdd();

                    b.Property<string>("Nombre")
                        .IsRequired()
                        .HasColumnType("varchar(100)")
                        .HasMaxLength(100);

                    b.HasKey("IdStatus")
                        .HasName("PK_Status");

                    b.ToTable("Status");
                });

            modelBuilder.Entity("Microsoft.AspNetCore.Identity.EntityFrameworkCore.IdentityRoleClaim<string>", b =>
                {
                    b.HasOne("Microsoft.AspNetCore.Identity.EntityFrameworkCore.IdentityRole")
                        .WithMany("Claims")
                        .HasForeignKey("RoleId")
                        .OnDelete(DeleteBehavior.Cascade);
                });

            modelBuilder.Entity("Microsoft.AspNetCore.Identity.EntityFrameworkCore.IdentityUserClaim<string>", b =>
                {
                    b.HasOne("SistemasLegales.Models.Entidades.ApplicationUser")
                        .WithMany("Claims")
                        .HasForeignKey("UserId")
                        .OnDelete(DeleteBehavior.Cascade);
                });

            modelBuilder.Entity("Microsoft.AspNetCore.Identity.EntityFrameworkCore.IdentityUserLogin<string>", b =>
                {
                    b.HasOne("SistemasLegales.Models.Entidades.ApplicationUser")
                        .WithMany("Logins")
                        .HasForeignKey("UserId")
                        .OnDelete(DeleteBehavior.Cascade);
                });

            modelBuilder.Entity("Microsoft.AspNetCore.Identity.EntityFrameworkCore.IdentityUserRole<string>", b =>
                {
                    b.HasOne("Microsoft.AspNetCore.Identity.EntityFrameworkCore.IdentityRole")
                        .WithMany("Users")
                        .HasForeignKey("RoleId")
                        .OnDelete(DeleteBehavior.Cascade);

                    b.HasOne("SistemasLegales.Models.Entidades.ApplicationUser")
                        .WithMany("Roles")
                        .HasForeignKey("UserId")
                        .OnDelete(DeleteBehavior.Cascade);
                });

            modelBuilder.Entity("SistemasLegales.Models.Entidades.Documento", b =>
                {
                    b.HasOne("SistemasLegales.Models.Entidades.RequisitoLegal", "RequisitoLegal")
                        .WithMany("Documento")
                        .HasForeignKey("IdRequisitoLegal")
                        .OnDelete(DeleteBehavior.Cascade);
                });

            modelBuilder.Entity("SistemasLegales.Models.Entidades.DocumentoRequisito", b =>
                {
                    b.HasOne("SistemasLegales.Models.Entidades.Requisito", "Requisito")
                        .WithMany("DocumentoRequisito")
                        .HasForeignKey("IdRequisito")
                        .HasConstraintName("FK_DocumentoRequisito_Requisito")
                        .OnDelete(DeleteBehavior.Cascade);
                });

            modelBuilder.Entity("SistemasLegales.Models.Entidades.Requisito", b =>
                {
                    b.HasOne("SistemasLegales.Models.Entidades.Actor", "ActorCustodioDocumento")
                        .WithMany("AdminRequisitoLegalIdActorCustodioDocumento")
                        .HasForeignKey("IdActorCustodioDocumento")
                        .HasConstraintName("FK_AdminRequisitoLegal_Actor2");

                    b.HasOne("SistemasLegales.Models.Entidades.Actor", "ActorDuennoProceso")
                        .WithMany("AdminRequisitoLegalIdActorDuennoProceso")
                        .HasForeignKey("IdActorDuennoProceso")
                        .HasConstraintName("FK_AdminRequisitoLegal_Actor");

                    b.HasOne("SistemasLegales.Models.Entidades.Actor", "ActorResponsableGestSeg")
                        .WithMany("AdminRequisitoLegalIdActorResponsableGestSeg")
                        .HasForeignKey("IdActorResponsableGestSeg")
                        .HasConstraintName("FK_AdminRequisitoLegal_Actor1");

                    b.HasOne("SistemasLegales.Models.Entidades.Ciudad", "Ciudad")
                        .WithMany("AdminRequisitoLegal")
                        .HasForeignKey("IdCiudad")
                        .OnDelete(DeleteBehavior.Cascade);

                    b.HasOne("SistemasLegales.Models.Entidades.Documento", "Documento")
                        .WithMany("Requisito")
                        .HasForeignKey("IdDocumento")
                        .OnDelete(DeleteBehavior.Cascade);

                    b.HasOne("SistemasLegales.Models.Entidades.Proceso", "Proceso")
                        .WithMany("AdminRequisitoLegal")
                        .HasForeignKey("IdProceso")
                        .OnDelete(DeleteBehavior.Cascade);

                    b.HasOne("SistemasLegales.Models.Entidades.Status", "Status")
                        .WithMany("AdminRequisitoLegal")
                        .HasForeignKey("IdStatus")
                        .OnDelete(DeleteBehavior.Cascade);
                });

            modelBuilder.Entity("SistemasLegales.Models.Entidades.RequisitoLegal", b =>
                {
                    b.HasOne("SistemasLegales.Models.Entidades.OrganismoControl", "OrganismoControl")
                        .WithMany("RequisitoLegal")
                        .HasForeignKey("IdOrganismoControl")
                        .HasConstraintName("FK_RequisitoLegal_OrganismoControl")
                        .OnDelete(DeleteBehavior.Cascade);
                });
        }
    }
}
