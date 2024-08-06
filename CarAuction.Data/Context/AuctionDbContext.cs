using CarAuction.Core.Models;
using CarAuction.Core.Models.BaseModels;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CarAuction.Data.Context
{
    public class AuctionDbContext : IdentityDbContext<AppUser, AppRole, string>
    {
        public AuctionDbContext(DbContextOptions<AuctionDbContext> options) : base(options)
        {
        }
        public DbSet<Category> Categories { get; set; }
        public DbSet<Tag> Tags { get; set; }
        public DbSet<Model> Models { get; set; }
        public DbSet<Car> Cars { get; set; }
        public DbSet<CarAuctionDetail> CarAuctionDetails { get; set; }
        public DbSet<CarImage> CarImages { get; set; }
        public DbSet<Status> Statuses { get; set; }
        public DbSet<Brand> Brands { get; set; }
        public DbSet<Fuel> Fuels { get; set; }
        public DbSet<Color> Colors { get; set; }
        public DbSet<Ban> Bans { get; set; }
        public DbSet<Blog> Blogs { get; set; }
        public DbSet<BlogTag> BlogTags { get; set; }
        public DbSet<Subscribe> Subscribes { get; set; }
        public DbSet<Setting> Settings { get; set; }
        public DbSet<Comment> Comments { get; set; }
        public override Task<int> SaveChangesAsync(CancellationToken cancellationToken = default)
        {
            var datas = ChangeTracker.Entries<BaseEntity>();

            foreach (var data in datas)
            {
                if (data.State == EntityState.Added)
                    data.Entity.CreatedAt = DateTime.UtcNow;
                else if (data.State == EntityState.Modified)
                    data.Entity.UpdatedAt = DateTime.UtcNow;
            }
            return base.SaveChangesAsync(cancellationToken);
        }
        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<BlogTag>()
                  .HasKey(bt => new { bt.TagId, bt.BlogId });

            modelBuilder.Entity<CarAuctionDetail>()
                 .HasKey(cd => cd.Id);

            modelBuilder.Entity<CarAuctionDetail>()
         .HasOne(s => s.Car)
         .WithOne(ad => ad.CarAuctionDetail)
         .HasForeignKey<CarAuctionDetail>(ad => ad.Id);
            base.OnModelCreating(modelBuilder);
        }
    }
}
