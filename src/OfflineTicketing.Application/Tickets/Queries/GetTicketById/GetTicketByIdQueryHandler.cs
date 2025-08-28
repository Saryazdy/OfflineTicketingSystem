using AutoMapper;
using MediatR;
using OfflineTicketing.Application.Common.Interfaces;
using OfflineTicketing.Application.Common.Models;
using OfflineTicketing.Application.Tickets.Dtos;
using OfflineTicketing.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OfflineTicketing.Application.Tickets.Queries.GetTicketById
{
    public class GetTicketByIdQueryHandler : IRequestHandler<GetTicketByIdQuery, Result<TicketDto?>>
    {
        private readonly ITicketRepository _ticketRepository;
        private readonly IMapper _mapper;

        public GetTicketByIdQueryHandler(ITicketRepository ticketRepository, IMapper mapper)
        {
            _ticketRepository = ticketRepository;
            _mapper = mapper;
        }

        public async Task<Result<TicketDto?>> Handle(GetTicketByIdQuery request, CancellationToken cancellationToken)
        {
            var ticket = await _ticketRepository.GetByIdAsync(request.TicketId);
            if (ticket == null) return null;

            return Result<TicketDto>.Ok(  _mapper.Map<TicketDto>(ticket));
        }
    }
}