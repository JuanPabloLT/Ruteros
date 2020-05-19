using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using Ruteros.Web.Data.Entities;

namespace Ruteros.Web.Data
{
    public class DataContext : IdentityDbContext<UserEntity>
    {
        public DataContext(DbContextOptions<DataContext> options) : base(options)
        { 
        }
        public DbSet<ShippingEntity> Shippings { get; set; }
        public DbSet<ShippingDetailEntity> ShippingDetails { get; set; }
        public DbSet<TripDetailEntity> TripDetails { get; set; }
        public DbSet<TripEntity> Trips { get; set; }
        public DbSet<VehicleEntity> Vehicles { get; set; }
        public DbSet<WarehouseEntity> Warehouses { get; set; }
        
    }
}

