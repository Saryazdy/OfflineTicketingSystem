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
          

  
            var ticket = new Ticket(
                title: request.Title,
                description: request.Description,
                createdByUserId: request. CreatedByUserId,
                priority: request.Priority
            );

          
            return await _ticketRepo.AddAsync(ticket);
        }
    }
    }

