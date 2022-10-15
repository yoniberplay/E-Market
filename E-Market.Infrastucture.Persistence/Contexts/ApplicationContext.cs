using Microsoft.EntityFrameworkCore;
using E_Market.Core.Domain.Common;
using E_Market.Core.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace E_Market.Infrastructure.Persistence.Contexts
{
    public class ApplicationContext : DbContext
    {
        public ApplicationContext(DbContextOptions<ApplicationContext> options) : base(options) { }

        public DbSet<Anuncio> Anuncios { get; set; }
        public DbSet<Category> Categories { get; set; }
        public DbSet<User> Users { get; set; }

        public DbSet<Fotos> Fotos { get; set; }

        public override Task<int> SaveChangesAsync(CancellationToken cancellationToken = new CancellationToken())
        {
            foreach(var entry in ChangeTracker.Entries<AuditableBaseEntity>())
            {
                switch (entry.State)
                {
                    case EntityState.Added:
                        entry.Entity.Created = DateTime.Now;
                        entry.Entity.CreatedBy = "DefaultAppUser";
                        break;
                    case EntityState.Modified:
                        entry.Entity.LastModified = DateTime.Now;
                        entry.Entity.LastModifiedBy = "DefaultAppUser";
                        break;
                }
            }

            return base.SaveChangesAsync(cancellationToken);
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            //FLUENT API

            #region tables

            modelBuilder.Entity<Anuncio>()
                .ToTable("Anuncios");

            modelBuilder.Entity<Category>()
                .ToTable("Categories");

            modelBuilder.Entity<User>()
                .ToTable("Users");

            modelBuilder.Entity<Fotos>()
                .ToTable("Fotos");

            #endregion

            #region "primary keys"
            modelBuilder.Entity<Anuncio>()
                .HasKey(anuncio => anuncio.Id);

            modelBuilder.Entity<Category>()
                .HasKey(category => category.Id);

            modelBuilder.Entity<User>()
                .HasKey(user => user.Id);

            modelBuilder.Entity<Fotos>()
               .HasKey(foto => foto.Id);
            #endregion

            #region "Relationships"
            modelBuilder.Entity<Category>()
                .HasMany<Anuncio>(category => category.Anuncios)
                .WithOne(anuncio => anuncio.Category)
                .HasForeignKey(anuncio => anuncio.CategoryId)
                .OnDelete(DeleteBehavior.Cascade);

            modelBuilder.Entity<User>()
            .HasMany<Anuncio>(user => user.Anuncios)
            .WithOne(anuncio => anuncio.User)
            .HasForeignKey(anuncio => anuncio.UserId)
            .OnDelete(DeleteBehavior.Cascade);

            modelBuilder.Entity<Anuncio>()
                .HasMany<Fotos>(a => a.Fotos)
                .WithOne(f => f.anuncio)
                .HasForeignKey(f => f.AnuncioID)
                .OnDelete(DeleteBehavior.Cascade);

           
            #endregion

            #region "Property configurations"

            #region anuncios

            modelBuilder.Entity<Anuncio>().
                Property(anuncio => anuncio.Name)
                .IsRequired();

            modelBuilder.Entity<Anuncio>().
               Property(anuncio => anuncio.Price)
               .IsRequired();

            #endregion

            #region categories
            modelBuilder.Entity<Category>().
              Property(category => category.Name)
              .IsRequired()
              .HasMaxLength(100);
            #endregion

            #region users

            modelBuilder.Entity<User>().
                Property(user => user.Name)
                .IsRequired();

            modelBuilder.Entity<User>().
               Property(user => user.Username)
               .IsRequired();

            modelBuilder.Entity<User>()
    .HasIndex(user => user.Username)
      .IsUnique();

            modelBuilder.Entity<User>().
              Property(user => user.Password)
              .IsRequired();

            modelBuilder.Entity<User>().
              Property(user => user.Email)
              .IsRequired();

            modelBuilder.Entity<User>().
               Property(user => user.Phone)
               .IsRequired();



            #endregion

            #region Fotos

            modelBuilder.Entity<Fotos>().
              Property(f => f.AnuncioID)
              .IsRequired();

            modelBuilder.Entity<Fotos>().
               Property(f => f.ImageUrl)
               .IsRequired();
            #endregion

            #endregion

        }

    }
}
