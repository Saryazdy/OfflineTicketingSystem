using Microsoft.EntityFrameworkCore;
using OfflineTicketing.Domain.Entities;
using OfflineTicketing.Domain.Enums;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Cryptography;
using System.Text;
using System.Threading.Tasks;

namespace OfflineTicketing.Infrastructure.Persistence
{
  public static class DbInitializer
    {
        public static void Initialize(AppDbContext context)
        {
            context.Database.Migrate();

            if (!context.Users.Any())
            {
                var users = new List<User>
                {
                    new User("Admin User", "admin@example.com", BCrypt.Net.BCrypt.HashPassword("Admin123!"), Role.Admin),
                    new User("Employee User", "employee@example.com", BCrypt.Net.BCrypt.HashPassword("Employee123!"), Role.Employee),
                    new User("Employee2", "employee2@example.com", BCrypt.Net.BCrypt.HashPassword("Employee123!"), Role.Employee)
                };

                context.Users.AddRange(users);
                 context.SaveChangesAsync();

                var tickets = new List<Ticket>
                {
                    new Ticket("Computer Issue", "PC not working", users[1].Id, TicketPriority.High),
                    new Ticket("Software Install", "Need VS Code", users[2].Id, TicketPriority.Medium)
                };

                context.Tickets.AddRange(tickets);
                 context.SaveChangesAsync();
            }
        }
    }
}
