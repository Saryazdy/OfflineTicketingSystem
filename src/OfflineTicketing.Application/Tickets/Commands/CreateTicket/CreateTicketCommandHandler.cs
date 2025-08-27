using MediatR;
using OfflineTicketing.Application.Common.Interfaces;
using OfflineTicketing.Domain.Entities;
using OfflineTicketing.Domain.Enums;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OfflineTicketing.Application.Tickets.Commands.CreateTicket
{
    public class CreateTicketCommandHandler : IRequestHandler<CreateTicketCommand, Ticket>
    {
            private readonly ITicketRepository _ticketRepo;
            public CreateTicketCommandHandler(ITicketRepository ticketRepo) => _ticketRepo = ticketRepo;

            public async Task<Ticket> Handle(CreateTicketCommand request, CancellationToken cancellationToken)
            {
                var ticket = new Ticket
                {
                    Title = request.Title,
                    Description = request.Description,
                    Priority = request.Priority,
                    CreatedByUserId = request.CreatedByUserId
                };

                return await _ticketRepo.AddAsync(ticket);

            }
        }
    }

