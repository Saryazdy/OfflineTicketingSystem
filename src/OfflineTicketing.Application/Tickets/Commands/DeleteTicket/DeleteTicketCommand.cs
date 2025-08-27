using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OfflineTicketing.Application.Tickets.Commands.DeleteTicket
{
    public class DeleteTicketCommand : IRequest<bool>
    {
        public Guid Id { get; set; }
    }
}
