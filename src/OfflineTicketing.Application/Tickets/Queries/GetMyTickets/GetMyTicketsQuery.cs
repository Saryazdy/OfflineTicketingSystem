using MediatR;
using OfflineTicketing.Application.Tickets.Dtos;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OfflineTicketing.Application.Tickets.Queries.GetMyTickets
{
    public class GetMyTicketsQuery : IRequest<IEnumerable<TicketDto>>
    {
        public Guid UserId { get; set; }   // از JWT میاد
    }

}
