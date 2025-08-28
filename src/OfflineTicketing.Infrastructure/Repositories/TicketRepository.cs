using AutoMapper;
using Microsoft.EntityFrameworkCore;
using OfflineTicketing.Application.Common.Extensions;
using OfflineTicketing.Application.Common.Interfaces;
using OfflineTicketing.Application.Common.Models;
using OfflineTicketing.Application.Tickets.Dtos;
using OfflineTicketing.Domain.Entities;
using OfflineTicketing.Infrastructure.Persistence;


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

            _context.Tickets.Add(ticket);
            await _context.SaveChangesAsync();
            return ticket;
        }
        public IQueryable<Ticket> GetAllQueryable() => _context.Tickets.AsQueryable();
        public async Task<PaginatedList<TicketDto>> GetPagedAsync(
    int pageNumber,
    int pageSize,
    IMapper mapper,
    CancellationToken cancellationToken = default)
        {
            var query = _context.Tickets.AsQueryable();

            
            var mappedQuery = mapper.ProjectTo<TicketDto>(query);

            return await mappedQuery.ToPaginatedListAsync(pageNumber, pageSize, cancellationToken);
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

        public async Task<PaginatedList<TicketDto>> GetByUserIdAsync(
         Guid userId,
         int pageNumber,
         int pageSize,
         IMapper mapper,
         CancellationToken cancellationToken = default)
        {
            var query = _context.Tickets
                .Include(t => t.CreatedByUser)
                .Include(t => t.AssignedToUser)
                .Where(t => t.CreatedByUserId == userId && !t.IsDeleted)
                .OrderByDescending(t => t.CreatedAt)
                .AsNoTracking();
            var mappedQuery = mapper.ProjectTo<TicketDto>(query);

            return await mappedQuery.ToPaginatedListAsync(pageNumber, pageSize, cancellationToken);
         
        }

        public async Task UpdateAsync(Ticket ticket)
        {
            if (ticket == null) throw new ArgumentNullException(nameof(ticket));

            // Ensure the entity is attached so EF tracks changes
            var tracked = await _context.Tickets.FirstOrDefaultAsync(t => t.Id == ticket.Id);
            if (tracked == null) throw new KeyNotFoundException($"Ticket with id {ticket.Id} not found.");

         

            _context.Tickets.Update(tracked);
            await _context.SaveChangesAsync();
        }

        public async Task SoftDeleteAsync(Guid ticketId)
        {
            var ticket = await _context.Tickets.FirstOrDefaultAsync(t => t.Id == ticketId);
            if (ticket == null) return;

            _context.Tickets.Update(ticket);
            await _context.SaveChangesAsync();
        }


    }
}

