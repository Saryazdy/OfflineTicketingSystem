using OfflineTicketing.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OfflineTicketing.Application.Common.Interfaces
{
    public interface ITicketRepository
    {
        Task<Ticket> AddAsync(Ticket ticket);
        Task<Ticket?> GetByIdAsync(Guid id);
        Task<IEnumerable<Ticket>> GetAllAsync();
        Task<IEnumerable<Ticket>> GetByUserIdAsync(Guid userId);
        Task UpdateAsync(Ticket ticket);
        Task SoftDeleteAsync(Guid ticketId);

        Task AddHistoryAsync(TicketHistory history);
    }
}
