using System;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata;

#nullable disable

namespace entityNuget.Models.DB
{
    public partial class botDbContext : DbContext
    {
        public botDbContext()
        {
        }

        public botDbContext(DbContextOptions<botDbContext> options)
            : base(options)
        {
        }

        public virtual DbSet<TbBebida> TbBebidas { get; set; }
        public virtual DbSet<TbOferta> TbOfertas { get; set; }
        public virtual DbSet<TbProducto> TbProductos { get; set; }
        public virtual DbSet<TbUsuario> TbUsuarios { get; set; }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            if (!optionsBuilder.IsConfigured)
            {
#warning To protect potentially sensitive information in your connection string, you should move it out of source code. You can avoid scaffolding the connection string by using the Name= syntax to read it from configuration - see https://go.microsoft.com/fwlink/?linkid=2131148. For more guidance on storing connection strings, see http://go.microsoft.com/fwlink/?LinkId=723263.
                optionsBuilder.UseSqlServer("Server=localhost;Database=botDb;Trusted_Connection=True;");
            }
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.HasAnnotation("Relational:Collation", "Modern_Spanish_CI_AS");

            modelBuilder.Entity<TbBebida>(entity =>
            {
                entity.ToTable("tbBebidas");

                entity.Property(e => e.Id).HasColumnName("id");

                entity.Property(e => e.Descripcion)
                    .HasMaxLength(100)
                    .HasColumnName("descripcion")
                    .IsFixedLength(true);

                entity.Property(e => e.ImageUrl)
                    .HasMaxLength(100)
                    .HasColumnName("imageUrl")
                    .IsFixedLength(true);

                entity.Property(e => e.NombreBebida)
                    .HasMaxLength(25)
                    .HasColumnName("nombreBebida")
                    .IsFixedLength(true);

                entity.Property(e => e.Precio).HasColumnName("precio");
            });

            modelBuilder.Entity<TbOferta>(entity =>
            {
                entity.ToTable("tbOfertas");

                entity.Property(e => e.Id).HasColumnName("id");

                entity.Property(e => e.Descripcion)
                    .HasMaxLength(200)
                    .IsUnicode(false)
                    .HasColumnName("descripcion");

                entity.Property(e => e.Precio).HasColumnName("precio");

                entity.Property(e => e.Titulo)
                    .HasMaxLength(50)
                    .IsUnicode(false)
                    .HasColumnName("titulo");
            });

            modelBuilder.Entity<TbProducto>(entity =>
            {
                entity.ToTable("tbProducto");

                entity.Property(e => e.Id).HasColumnName("id");

                entity.Property(e => e.Descripcion)
                    .HasMaxLength(200)
                    .IsUnicode(false)
                    .HasColumnName("descripcion");

                entity.Property(e => e.ImageUrl)
                    .HasMaxLength(200)
                    .IsUnicode(false)
                    .HasColumnName("imageUrl");

                entity.Property(e => e.NombreProducto)
                    .HasMaxLength(50)
                    .IsUnicode(false)
                    .HasColumnName("nombreProducto");

                entity.Property(e => e.Precio).HasColumnName("precio");
            });

            modelBuilder.Entity<TbUsuario>(entity =>
            {
                entity.ToTable("tbUsuario");

                entity.Property(e => e.Id).HasColumnName("id");

                entity.Property(e => e.Apellido)
                    .HasMaxLength(25)
                    .HasColumnName("apellido")
                    .IsFixedLength(true);

                entity.Property(e => e.Mensaje)
                    .HasMaxLength(100)
                    .HasColumnName("mensaje")
                    .IsFixedLength(true);

                entity.Property(e => e.Nombre)
                    .HasMaxLength(25)
                    .HasColumnName("nombre")
                    .IsFixedLength(true);
            });

            OnModelCreatingPartial(modelBuilder);
        }

        partial void OnModelCreatingPartial(ModelBuilder modelBuilder);
    }
}
