using MediatR;
using OfflineTicketing.Application.Common.Interfaces;
using OfflineTicketing.Application.Tickets.Dtos;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OfflineTicketing.Application.Tickets.Queries.GetAllTickets
{
    public class GetAllTicketsQueryHandler : IRequestHandler<GetAllTicketsQuery, IEnumerable<TicketDto>>
    {
        private readonly ITicketRepository _ticketRepository;

        public GetAllTicketsQueryHandler(ITicketRepository ticketRepository)
        {
            _ticketRepository = ticketRepository;
        }

        public async Task<IEnumerable<TicketDto>> Handle(GetAllTicketsQuery request, CancellationToken cancellationToken)
        {
            var tickets = await _ticketRepository.GetAllAsync();

            return tickets.Select(t => new TicketDto
            {
                Id = t.Id,
                Title = t.Title,
                Description = t.Description,
                Status = t.Status.ToString(),
                Priority = t.Priority.ToString(),
                CreatedAt = t.CreatedAt,
                UpdatedAt = t.UpdatedAt
            });
        }
    }
}

