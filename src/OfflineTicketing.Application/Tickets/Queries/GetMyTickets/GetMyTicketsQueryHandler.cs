using AutoMapper;
using MediatR;
using OfflineTicketing.Application.Common.Interfaces;
using OfflineTicketing.Application.Common.Models;
using OfflineTicketing.Application.Tickets.Dtos;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OfflineTicketing.Application.Tickets.Queries.GetMyTickets
{

    public class GetMyTicketsQueryHandler : IRequestHandler<GetMyTicketsQuery, PaginatedList<TicketDto>>
    {
        private readonly ITicketRepository _ticketRepository;
        private readonly IMapper _mapper;

        public GetMyTicketsQueryHandler(ITicketRepository ticketRepository, IMapper mapper)
        {
            _ticketRepository = ticketRepository;
            _mapper = mapper;
        }

        public async Task<PaginatedList<TicketDto>> Handle(GetMyTicketsQuery request, CancellationToken cancellationToken)
        {
             return await _ticketRepository.GetByUserIdAsync(request.UserId, request.PageNumber, request.PageSize, _mapper, cancellationToken);


           
        }
    }
}