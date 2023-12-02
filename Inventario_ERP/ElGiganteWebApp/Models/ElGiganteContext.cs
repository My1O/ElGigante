using System;
using System.Collections.Generic;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata;

namespace ElGiganteWebApp.Models
{
    public partial class ElGiganteContext : DbContext
    {
        public ElGiganteContext()
        {
        }

        public ElGiganteContext(DbContextOptions<ElGiganteContext> options)
            : base(options)
        {
        }

        public virtual DbSet<AspNetRole> AspNetRoles { get; set; } = null!;
        public virtual DbSet<AspNetRoleClaim> AspNetRoleClaims { get; set; } = null!;
        public virtual DbSet<AspNetUser> AspNetUsers { get; set; } = null!;
        public virtual DbSet<AspNetUserClaim> AspNetUserClaims { get; set; } = null!;
        public virtual DbSet<AspNetUserLogin> AspNetUserLogins { get; set; } = null!;
        public virtual DbSet<AspNetUserToken> AspNetUserTokens { get; set; } = null!;
        public virtual DbSet<Bodega> Bodegas { get; set; } = null!;
        public virtual DbSet<Categorium> Categoria { get; set; } = null!;
        public virtual DbSet<Pai> Pais { get; set; } = null!;
        public virtual DbSet<Producto> Productos { get; set; } = null!;
        public virtual DbSet<Proveedor> Proveedors { get; set; } = null!;

       
        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<AspNetRole>(entity =>
            {
                entity.HasIndex(e => e.NormalizedName, "RoleNameIndex")
                    .IsUnique()
                    .HasFilter("([NormalizedName] IS NOT NULL)");

                entity.Property(e => e.Name).HasMaxLength(256);

                entity.Property(e => e.NormalizedName).HasMaxLength(256);
            });

            modelBuilder.Entity<AspNetRoleClaim>(entity =>
            {
                entity.HasIndex(e => e.RoleId, "IX_AspNetRoleClaims_RoleId");

                entity.HasOne(d => d.Role)
                    .WithMany(p => p.AspNetRoleClaims)
                    .HasForeignKey(d => d.RoleId);
            });

            modelBuilder.Entity<AspNetUser>(entity =>
            {
                entity.HasIndex(e => e.NormalizedEmail, "EmailIndex");

                entity.HasIndex(e => e.NormalizedUserName, "UserNameIndex")
                    .IsUnique()
                    .HasFilter("([NormalizedUserName] IS NOT NULL)");

                entity.Property(e => e.Email).HasMaxLength(256);

                entity.Property(e => e.NormalizedEmail).HasMaxLength(256);

                entity.Property(e => e.NormalizedUserName).HasMaxLength(256);

                entity.Property(e => e.UserName).HasMaxLength(256);

                entity.HasMany(d => d.Roles)
                    .WithMany(p => p.Users)
                    .UsingEntity<Dictionary<string, object>>(
                        "AspNetUserRole",
                        l => l.HasOne<AspNetRole>().WithMany().HasForeignKey("RoleId"),
                        r => r.HasOne<AspNetUser>().WithMany().HasForeignKey("UserId"),
                        j =>
                        {
                            j.HasKey("UserId", "RoleId");

                            j.ToTable("AspNetUserRoles");

                            j.HasIndex(new[] { "RoleId" }, "IX_AspNetUserRoles_RoleId");
                        });
            });

            modelBuilder.Entity<AspNetUserClaim>(entity =>
            {
                entity.HasIndex(e => e.UserId, "IX_AspNetUserClaims_UserId");

                entity.HasOne(d => d.User)
                    .WithMany(p => p.AspNetUserClaims)
                    .HasForeignKey(d => d.UserId);
            });

            modelBuilder.Entity<AspNetUserLogin>(entity =>
            {
                entity.HasKey(e => new { e.LoginProvider, e.ProviderKey });

                entity.HasIndex(e => e.UserId, "IX_AspNetUserLogins_UserId");

                entity.Property(e => e.LoginProvider).HasMaxLength(128);

                entity.Property(e => e.ProviderKey).HasMaxLength(128);

                entity.HasOne(d => d.User)
                    .WithMany(p => p.AspNetUserLogins)
                    .HasForeignKey(d => d.UserId);
            });

            modelBuilder.Entity<AspNetUserToken>(entity =>
            {
                entity.HasKey(e => new { e.UserId, e.LoginProvider, e.Name });

                entity.Property(e => e.LoginProvider).HasMaxLength(128);

                entity.Property(e => e.Name).HasMaxLength(128);

                entity.HasOne(d => d.User)
                    .WithMany(p => p.AspNetUserTokens)
                    .HasForeignKey(d => d.UserId);
            });

            modelBuilder.Entity<Bodega>(entity =>
            {
                entity.HasKey(e => e.IdBodega);

                entity.ToTable("Bodega");

                entity.Property(e => e.IdBodega).HasColumnName("idBodega");

                entity.Property(e => e.NombreAlmacen)
                    .HasMaxLength(50)
                    .IsUnicode(false)
                    .HasColumnName("nombreAlmacen");
            });

            modelBuilder.Entity<Categorium>(entity =>
            {
                entity.HasKey(e => e.IdCategoria);

                entity.Property(e => e.IdCategoria)
                    .ValueGeneratedNever()
                    .HasColumnName("idCategoria");

                entity.Property(e => e.NombreProducto)
                    .HasMaxLength(100)
                    .HasColumnName("nombreProducto");

                entity.Property(e => e.Status).HasColumnName("status");
            });

            modelBuilder.Entity<Pai>(entity =>
            {
                entity.HasKey(e => e.IdPais);

                entity.Property(e => e.IdPais).HasColumnName("idPais");

                entity.Property(e => e.NombrePais)
                    .HasMaxLength(250)
                    .IsUnicode(false)
                    .HasColumnName("nombrePais");
            });

            modelBuilder.Entity<Producto>(entity =>
            {
                entity.HasKey(e => e.IdProducto);

                entity.ToTable("Producto");

                entity.Property(e => e.IdProducto).HasColumnName("idProducto");

                entity.Property(e => e.CantidadExistencia).HasColumnName("cantidadExistencia");

                entity.Property(e => e.DescripcionProducto)
                    .HasMaxLength(250)
                    .IsUnicode(false)
                    .HasColumnName("descripcionProducto");

                entity.Property(e => e.IdBodega).HasColumnName("idBodega");

                entity.Property(e => e.IdCategoria).HasColumnName("idCategoria");

                entity.Property(e => e.IdProveedor).HasColumnName("idProveedor");

                entity.Property(e => e.NombreProducto)
                    .HasMaxLength(100)
                    .IsUnicode(false)
                    .HasColumnName("nombreProducto");

                entity.Property(e => e.UnidadMedida)
                    .HasMaxLength(50)
                    .IsUnicode(false)
                    .HasColumnName("unidadMedida");

                entity.HasOne(d => d.IdBodegaNavigation)
                    .WithMany(p => p.Productos)
                    .HasForeignKey(d => d.IdBodega)
                    .HasConstraintName("FK_Producto_Bodega");

                entity.HasOne(d => d.IdCategoriaNavigation)
                    .WithMany(p => p.Productos)
                    .HasForeignKey(d => d.IdCategoria)
                    .HasConstraintName("FK_Producto_Categoria");

                entity.HasOne(d => d.IdProveedorNavigation)
                    .WithMany(p => p.Productos)
                    .HasForeignKey(d => d.IdProveedor)
                    .HasConstraintName("FK_Producto_Proveedor");
            });

            modelBuilder.Entity<Proveedor>(entity =>
            {
                entity.HasKey(e => e.IdProveedor);

                entity.ToTable("Proveedor");

                entity.Property(e => e.IdProveedor).HasColumnName("idProveedor");

                entity.Property(e => e.IdPais).HasColumnName("idPais");

                entity.Property(e => e.NombreProveedor)
                    .HasMaxLength(50)
                    .IsUnicode(false)
                    .HasColumnName("nombreProveedor");

                entity.HasOne(d => d.IdPaisNavigation)
                    .WithMany(p => p.Proveedors)
                    .HasForeignKey(d => d.IdPais)
                    .HasConstraintName("FK_Proveedor_Pais");
            });

            OnModelCreatingPartial(modelBuilder);
        }

        partial void OnModelCreatingPartial(ModelBuilder modelBuilder);
    }
}
