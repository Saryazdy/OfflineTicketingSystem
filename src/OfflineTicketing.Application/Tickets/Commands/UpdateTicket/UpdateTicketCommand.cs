using MediatR;
using OfflineTicketing.Domain.Enums;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OfflineTicketing.Application.Tickets.Commands.UpdateTicket
{
    public class UpdateTicketCommand : IRequest<bool>
    {
        public Guid Id { get; set; }
        public TicketStatus Status { get; set; }
        public Guid? AssignedToUserId { get; set; }
    }
}
