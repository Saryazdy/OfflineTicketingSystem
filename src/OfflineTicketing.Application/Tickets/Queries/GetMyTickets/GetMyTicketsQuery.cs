using MediatR;
using OfflineTicketing.Application.Common.Models;
using OfflineTicketing.Application.Tickets.Dtos;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OfflineTicketing.Application.Tickets.Queries.GetMyTickets
{
    public class GetMyTicketsQuery : IRequest<PaginatedList<TicketDto>>
    {
        public Guid UserId { get; set; }   // از JWT میاد
        public int PageNumber { get; set; } = 1;
        public int PageSize { get; set; } = 10;
    }

}
