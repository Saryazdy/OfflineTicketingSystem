using MediatR;
using OfflineTicketing.Application.Common.Interfaces;
using OfflineTicketing.Application.Tickets.Dtos;
using OfflineTicketing.Domain.Enums;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OfflineTicketing.Application.Tickets.Queries.GetTicketStats
{
    public class GetTicketStatsQueryHandler : IRequestHandler<GetTicketStatsQuery, TicketStatsDto>
    {
        private readonly ITicketRepository _ticketRepository;

        public GetTicketStatsQueryHandler(ITicketRepository ticketRepository)
        {
            _ticketRepository = ticketRepository;
        }

        public async Task<TicketStatsDto> Handle(GetTicketStatsQuery request, CancellationToken cancellationToken)
        {
            var tickets = await _ticketRepository.GetAllAsync();

            return new TicketStatsDto
            {
                Open = tickets.Count(t => t.Status == TicketStatus.Open),
                InProgress = tickets.Count(t => t.Status == TicketStatus.InProgress),
                Closed = tickets.Count(t => t.Status == TicketStatus.Closed)
            };
        }
    }
}
