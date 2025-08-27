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

            // اگر ادمین خواست کسی رو Assign کنه، بررسی وجود یوزر
            if (request.AssignedToUserId.HasValue)
            {
                var user = await _userRepository.GetByIdAsync(request.AssignedToUserId.Value);
                if (user == null || user.Role != Domain.Enums.Role.Admin)
                    throw new UnauthorizedAccessException("Assigned user must be a valid Admin");

                ticket.AssignedToUserId = request.AssignedToUserId.Value;
            }

            ticket.Status = request.Status;
            ticket.UpdatedAt = DateTime.UtcNow;

            await _ticketRepository.UpdateAsync(ticket);

            // تاریخچه برای Audit
            var history = new TicketHistory
            {
                Id = Guid.NewGuid(),
                TicketId = ticket.Id,
                ChangedAt = DateTime.UtcNow,
                //Status = ticket.Status,
                //AssignedToUserId = ticket.AssignedToUserId
            };

            await _ticketRepository.AddHistoryAsync(history);

            return true;
        }
    }
}
