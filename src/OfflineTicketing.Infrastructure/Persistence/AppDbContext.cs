using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Options;
using OfflineTicketing.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Sockets;
using System.Text;
using System.Threading.Tasks;

namespace OfflineTicketing.Infrastructure.Persistence
{

    public class AppDbContext : DbContext
    {
        public DbSet<User> Users { get; set; } = null!;
        public DbSet<Ticket> Tickets { get; set; } = null!;

        public AppDbContext(DbContextOptions<AppDbContext> options)
          : base(options)
        {
        }
        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);


            modelBuilder.Entity<User>()
                .HasIndex(u => u.Email)
                .IsUnique();

            modelBuilder.Entity<User>()
                .HasMany(u => u.CreatedTickets)
                .WithOne(t => t.CreatedByUser)
                .HasForeignKey(t => t.CreatedByUserId);

            modelBuilder.Entity<User>()
                .HasMany(u => u.AssignedTickets)
                .WithOne(t => t.AssignedToUser)
                .HasForeignKey(t => t.AssignedToUserId)
                .OnDelete(DeleteBehavior.SetNull);
        }
    }
}
