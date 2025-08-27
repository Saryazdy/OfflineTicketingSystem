using OfflineTicketing.Domain.Entities;
using OfflineTicketing.Domain.Enums;
using OfflineTicketing.Infrastructure.Data;

namespace OfflineTicketing.Infrastructure.DataSeed
{
    public static class DbSeeder
    {
        public static async Task SeedAsync(AppDbContext context)
        {
            if (!context.Users.Any())
            {
                var users = new List<User>
            {
                new User { FullName="Admin User", Email="admin@example.com", PasswordHash=BCrypt.Net.BCrypt.HashPassword("Admin123!"), Role=Role.Admin },
                new User { FullName="Employee User", Email="employee@example.com", PasswordHash=BCrypt.Net.BCrypt.HashPassword("Employee123!"), Role=Role.Employee },
                new User { FullName="Employee2", Email="employee2@example.com", PasswordHash=BCrypt.Net.BCrypt.HashPassword("Employee123!"), Role=Role.Employee }
            };
                context.Users.AddRange(users);
                await context.SaveChangesAsync();

                var tickets = new List<Ticket>
            {
                new Ticket{ Title="Computer Issue", Description="PC not working", CreatedByUserId=users[1].Id, Priority=TicketPriority.High },
                new Ticket{ Title="Software Install", Description="Need VS Code", CreatedByUserId=users[2].Id, Priority=TicketPriority.Medium }
            };
                context.Tickets.AddRange(tickets);
                await context.SaveChangesAsync();
            }
        }
    }
}
