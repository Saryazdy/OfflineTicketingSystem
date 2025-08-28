using MediatR;
using OfflineTicketing.Application.Common.Interfaces;
using OfflineTicketing.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OfflineTicketing.Application.Tickets.Commands.UpdateTicket
{
    public class UpdateTicketCommandHandler : IRequestHandler<UpdateTicketCommand, bool>
    {
        private readonly ITicketRepository _ticketRepository;
        private readonly IUserRepository _userRepository;

        public UpdateTicketCommandHandler(ITicketRepository ticketRepository, IUserRepository userRepository)
        {
            _ticketRepository = ticketRepository;
            _userRepository = userRepository;
        }

        public async Task<bool> Handle(UpdateTicketCommand request, CancellationToken cancellationToken)
        {
            var ticket = await _ticketRepository.GetByIdAsync(request.Id);
            if (ticket == null || ticket.IsDeleted)
                return false;

            if (request.AssignedToUserId.HasValue)
            {
                var user = await _userRepository.GetByIdAsync(request.AssignedToUserId.Value);
                if (user == null )
                    throw new UnauthorizedAccessException("user not found");

                ticket.AssignToUser(user);
            }
            ticket.ChangeStatus(request.Status);

            await _ticketRepository.UpdateAsync(ticket);



            return true;
        }
    }
}
