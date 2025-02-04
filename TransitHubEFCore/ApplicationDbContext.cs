using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TransitHubRepo.Models;


namespace TransitHubEFCore
{
    public class ApplicationDbContext : IdentityDbContext<ApplicationUser>
    {
        
        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options) : base(options) 
        { }
        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

            //// Find the unique index for UserName and remove it
            //modelBuilder.Entity<ApplicationUser>()
            //    .HasIndex(u => u.UserName)
            //    .IsUnique(false);
        }
        public DbSet<Trip>? Trip { get; set; }
        public DbSet<LocalTransport>? LocalTransports { get; set; }
        public DbSet<Shipp>? Shipps { get; set; }
        public DbSet<UserImage>? UserImages { get; set; }
        public DbSet<OrderQR>? orderQRs { get; set; }
        public DbSet<Message>? Messages { get; set; }
        public DbSet<UserConnection>? userConnections { get; set; }
        public DbSet<Rate>? Rates { get; set; }

    }
}
