using Microsoft.EntityFrameworkCore;
using OfflineTicketing.Application.Common.Interfaces;
using OfflineTicketing.Domain.Entities;
using OfflineTicketing.Infrastructure.Persistence;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OfflineTicketing.Infrastructure.Repositories
{
    public class UserRepository : IUserRepository
    {
        private readonly AppDbContext _context;

        public UserRepository(AppDbContext context)
        {
            _context = context ?? throw new ArgumentNullException(nameof(context));
        }

        public async Task<User?> GetByIdAsync(Guid id)
        {
            if (id == Guid.Empty)
                return null;

            return await _context.Users
                .AsNoTracking()
                .FirstOrDefaultAsync(u => u.Id == id);
        }
        public async Task<User?> GetByEmailAsync(string email)
        {
            if (string.IsNullOrWhiteSpace(email))
                return null;

            // Normalize input email
            var normalizedEmail = email.Trim().ToLower();

            return await _context.Users
                .AsNoTracking()
                .FirstOrDefaultAsync(u => u.Email.Trim().ToLower() == normalizedEmail);
        }
    }
}

