using Core.Entities;
using Microsoft.EntityFrameworkCore;

namespace Infrastructure.Persistence
{
    public class AppDbContext : DbContext
    {
        public AppDbContext(DbContextOptions<AppDbContext> options) : base(options) { }

        public DbSet<Booking> Booking { get; set; }
        public DbSet<Ad> Ad { get; set; }
        public DbSet<Customer> Customer { get; set; }
        public DbSet<Image> Image { get; set; }
        public DbSet<Room> Room { get; set; }
        public DbSet<RoomType> RoomType { get; set; }
        public DbSet<Page> Page { get; set; }
        public DbSet<Promotion> Promotion { get; set; }
        public DbSet<Season> Season { get; set; }
        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<PageImage>()
                .HasKey(pi => new { pi.PageID, pi.ImageID });

            // Opcional: Configurar las relaciones
            modelBuilder.Entity<PageImage>()
                .HasOne(pi => pi.Page)
                .WithMany(p => p.PageImages)
                .HasForeignKey(pi => pi.PageID);

            modelBuilder.Entity<PageImage>()
                .HasOne(pi => pi.Image)
                .WithMany(i => i.PageImages)
                .HasForeignKey(pi => pi.ImageID);

        }
    }
}
