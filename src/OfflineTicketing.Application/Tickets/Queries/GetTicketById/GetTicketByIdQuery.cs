using MediatR;
using OfflineTicketing.Application.Tickets.Dtos;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OfflineTicketing.Application.Tickets.Queries.GetTicketById
{
    public class GetTicketByIdQuery : IRequest<TicketDto?>
    {
        public Guid TicketId { get; set; }
    }
}
