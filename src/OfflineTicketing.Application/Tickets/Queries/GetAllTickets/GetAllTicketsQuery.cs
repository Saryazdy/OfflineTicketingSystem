using MediatR;
using OfflineTicketing.Application.Common.Models;
using OfflineTicketing.Application.Tickets.Dtos;

namespace OfflineTicketing.Application.Tickets.Queries.GetAllTickets
{
    public class GetAllTicketsQuery : IRequest<PaginatedList<TicketDto>>
    {
        public int PageNumber { get; set; } = 1;
        public int PageSize { get; set; } = 10;
    }
}
