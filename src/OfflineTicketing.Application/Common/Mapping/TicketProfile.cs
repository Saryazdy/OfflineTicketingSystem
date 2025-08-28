using AutoMapper;
using OfflineTicketing.Application.Tickets.Dtos;
using OfflineTicketing.Domain.Entities;
using OfflineTicketing.Domain.Enums;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using static System.Runtime.InteropServices.JavaScript.JSType;

namespace OfflineTicketing.Application.Common.Mapper
{
    public class TicketProfile : Profile
    {
        public TicketProfile()
        {
            CreateMap<Ticket, TicketDto>()
                .ForMember(dest => dest.Status, opt => opt.MapFrom(src => src.Status.ToString()))
                .ForMember(dest => dest.Priority, opt => opt.MapFrom(src => src.Priority.ToString()))
                .ReverseMap()
                .ForMember(dest => dest.Status, opt => opt.MapFrom(src => Enum.Parse<TicketStatus>(src.Status)))
                .ForMember(dest => dest.Priority, opt => opt.MapFrom(src => Enum.Parse<TicketPriority>(src.Priority)));
        }
    }
}
