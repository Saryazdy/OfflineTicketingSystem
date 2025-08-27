using Microsoft.EntityFrameworkCore;
using OfflineTicketing.Application.Common.Interfaces;
using OfflineTicketing.Domain.Entities;
using OfflineTicketing.Infrastructure.Data;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OfflineTicketing.Infrastructure.Repositories
{
    public class TicketRepository : ITicketRepository
    {
        private readonly AppDbContext _context;

        public TicketRepository(AppDbContext context)
        {
            _context = context ?? throw new ArgumentNullException(nameof(context));
        }

        public async Task<Ticket> AddAsync(Ticket ticket)
        {
            if (ticket == null) throw new ArgumentNullException(nameof(ticket));

            ticket.CreatedAt = DateTime.UtcNow;
            ticket.UpdatedAt = DateTime.UtcNow;

            _context.Tickets.Add(ticket);
            await _context.SaveChangesAsync();
            return ticket;
        }

        public async Task<Ticket?> GetByIdAsync(Guid id)
        {
            return await _context.Tickets
                .Include(t => t.CreatedByUser)
                .Include(t => t.AssignedToUser)
                .AsNoTracking()
                .FirstOrDefaultAsync(t => t.Id == id && !t.IsDeleted);
        }

        public async Task<IEnumerable<Ticket>> GetAllAsync()
        {
            return await _context.Tickets
                .Include(t => t.CreatedByUser)
                .Include(t => t.AssignedToUser)
                .Where(t => !t.IsDeleted)
                .AsNoTracking()
                .OrderByDescending(t => t.CreatedAt)
                .ToListAsync();
        }

        public async Task<IEnumerable<Ticket>> GetByUserIdAsync(Guid userId)
        {
            return await _context.Tickets
                .Include(t => t.CreatedByUser)
                .Include(t => t.AssignedToUser)
                .Where(t => t.CreatedByUserId == userId && !t.IsDeleted)
                .AsNoTracking()
                .OrderByDescending(t => t.CreatedAt)
                .ToListAsync();
        }

        public async Task UpdateAsync(Ticket ticket)
        {
            if (ticket == null) throw new ArgumentNullException(nameof(ticket));

            // Ensure the entity is attached so EF tracks changes
            var tracked = await _context.Tickets.FirstOrDefaultAsync(t => t.Id == ticket.Id);
            if (tracked == null) throw new KeyNotFoundException($"Ticket with id {ticket.Id} not found.");

            // update only allowed fields
            tracked.Title = ticket.Title;
            tracked.Description = ticket.Description;
            tracked.Status = ticket.Status;
            tracked.Priority = ticket.Priority;
            tracked.AssignedToUserId = ticket.AssignedToUserId;
            tracked.UpdatedAt = DateTime.UtcNow;

            _context.Tickets.Update(tracked);
            await _context.SaveChangesAsync();
        }

        public async Task SoftDeleteAsync(Guid ticketId)
        {
            var ticket = await _context.Tickets.FirstOrDefaultAsync(t => t.Id == ticketId);
            if (ticket == null) return;

            ticket.IsDeleted = true;
            ticket.UpdatedAt = DateTime.UtcNow;
            _context.Tickets.Update(ticket);
            await _context.SaveChangesAsync();
        }

        public async Task AddHistoryAsync(TicketHistory history)
        {
            if (history == null) throw new ArgumentNullException(nameof(history));

            history.ChangedAt = DateTime.UtcNow;
            _context.TicketHistories.Add(history);
            await _context.SaveChangesAsync();
        }
    }
}

