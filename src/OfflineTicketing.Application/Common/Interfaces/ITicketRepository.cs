using AutoMapper;
using OfflineTicketing.Application.Common.Models;
using OfflineTicketing.Application.Tickets.Dtos;
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
        Task<PaginatedList<TicketDto>> GetByUserIdAsync(
        Guid userId,
        int pageNumber,
        int pageSize, IMapper mapper,
        CancellationToken cancellationToken = default);
        Task<PaginatedList<TicketDto>> GetPagedAsync(
                                                    int pageNumber,
                                                    int pageSize,
                                                    IMapper mapper,
                                                    CancellationToken cancellationToken = default);

        Task UpdateAsync(Ticket ticket);
        Task SoftDeleteAsync(Guid ticketId);
        IQueryable<Ticket> GetAllQueryable();
    }
}
