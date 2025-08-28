using AutoMapper;
using MediatR;
using OfflineTicketing.Application.Common.Interfaces;
using OfflineTicketing.Application.Common.Models;
using OfflineTicketing.Application.Tickets.Dtos;
using OfflineTicketing.Application.Common.Extensions; // اگه اکستنشن اونجاست
using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;

namespace OfflineTicketing.Application.Tickets.Queries.GetAllTickets
{
 public class GetAllTicketsQueryHandler 
    : IRequestHandler<GetAllTicketsQuery, PaginatedList<TicketDto>>
    {
        private readonly ITicketRepository _ticketRepository;
        private readonly IMapper _mapper;

        public GetAllTicketsQueryHandler(ITicketRepository ticketRepository, IMapper mapper)
        {
            _ticketRepository = ticketRepository;
            _mapper = mapper;
        }

        public async Task<PaginatedList<TicketDto>> Handle(GetAllTicketsQuery request, CancellationToken cancellationToken)
        {
            return await _ticketRepository.GetPagedAsync(request.PageNumber, request.PageSize, _mapper, cancellationToken);
        }
    }
}
