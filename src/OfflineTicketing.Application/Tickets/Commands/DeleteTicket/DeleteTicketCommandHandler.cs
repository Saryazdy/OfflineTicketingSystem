using MediatR;
using OfflineTicketing.Application.Common.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OfflineTicketing.Application.Tickets.Commands.DeleteTicket
{
    public class DeleteTicketCommandHandler : IRequestHandler<DeleteTicketCommand, bool>
    {
        private readonly ITicketRepository _ticketRepository;

        public DeleteTicketCommandHandler(ITicketRepository ticketRepository)
        {
            _ticketRepository = ticketRepository;
        }

        public async Task<bool> Handle(DeleteTicketCommand request, CancellationToken cancellationToken)
        {
            var ticket = await _ticketRepository.GetByIdAsync(request.Id);
            if (ticket == null || ticket.IsDeleted)
                return false;

            await _ticketRepository.SoftDeleteAsync(request.Id);
            return true;
        }
    }
}
