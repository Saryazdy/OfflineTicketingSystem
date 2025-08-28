using MediatR;
using Microsoft.EntityFrameworkCore;
using OfflineTicketing.Application.Common.Interfaces;
using OfflineTicketing.Application.Common.Models;
using OfflineTicketing.Application.Tickets.Dtos;
using OfflineTicketing.Domain.Enums;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OfflineTicketing.Application.Tickets.Queries.GetTicketStats
{
    public class GetTicketStatsQueryHandler : IRequestHandler<GetTicketStatsQuery, Result< TicketStatsDto>>
    {
        private readonly ITicketRepository _ticketRepository;

        public GetTicketStatsQueryHandler(ITicketRepository ticketRepository)
        {
            _ticketRepository = ticketRepository;
        }

        public async Task<Result<TicketStatsDto>> Handle(GetTicketStatsQuery request, CancellationToken cancellationToken)
        {
            var query = _ticketRepository.GetAllQueryable();

            var openCount = await query.CountAsync(t => t.Status == TicketStatus.Open);
            var inProgressCount = await query.CountAsync(t => t.Status == TicketStatus.InProgress);
            var closedCount = await query.CountAsync(t => t.Status == TicketStatus.Closed);

            var dto =  new TicketStatsDto
            {
                Open = openCount,
                InProgress = inProgressCount,
                Closed = closedCount
            };

          return  Result<TicketStatsDto> .Ok(dto);
        }
    }
}
