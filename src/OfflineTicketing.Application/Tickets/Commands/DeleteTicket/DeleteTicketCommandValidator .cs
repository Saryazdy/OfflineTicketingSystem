using FluentValidation;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OfflineTicketing.Application.Tickets.Commands.DeleteTicket
{
    public class DeleteTicketCommandValidator : AbstractValidator<DeleteTicketCommand>
    {
        public DeleteTicketCommandValidator()
        {
            RuleFor(x => x.Id)
                .NotEmpty().WithMessage("Ticket Id is required");
        }
    }
}
