using MediatR;
using OfflineTicketing.Domain.Entities;
using OfflineTicketing.Domain.Enums;

namespace OfflineTicketing.Application.Tickets.Commands.CreateTicket
{
    public record CreateTicketCommand(string Title, string Description, TicketPriority Priority, Guid CreatedByUserId) : IRequest<Ticket>;
}
