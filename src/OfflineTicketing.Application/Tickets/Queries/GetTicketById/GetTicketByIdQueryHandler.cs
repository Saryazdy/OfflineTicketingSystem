using MediatR;
using OfflineTicketing.Application.Common.Interfaces;
using OfflineTicketing.Application.Tickets.Dtos;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OfflineTicketing.Application.Tickets.Queries.GetTicketById
{
    public class GetTicketByIdQueryHandler : IRequestHandler<GetTicketByIdQuery, TicketDto?>
    {
        private readonly ITicketRepository _ticketRepository;

        public GetTicketByIdQueryHandler(ITicketRepository ticketRepository)
        {
            _ticketRepository = ticketRepository;
        }

        public async Task<TicketDto?> Handle(GetTicketByIdQuery request, CancellationToken cancellationToken)
        {
            var ticket = await _ticketRepository.GetByIdAsync(request.TicketId);
            if (ticket == null) return null;

            return new TicketDto
            {
                Id = ticket.Id,
                Title = ticket.Title,
                Description = ticket.Description,
                Status = ticket.Status.ToString(),
                Priority = ticket.Priority.ToString(),
                CreatedAt = ticket.CreatedAt,
                UpdatedAt = ticket.UpdatedAt
            };
        }
    }
}