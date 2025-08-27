using FluentValidation;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OfflineTicketing.Application.Tickets.Commands.UpdateTicket
{
    public class UpdateTicketCommandValidator : AbstractValidator<UpdateTicketCommand>
    {
        public UpdateTicketCommandValidator()
        {
            RuleFor(x => x.Id)
                .NotEmpty().WithMessage("Ticket Id is required");

            RuleFor(x => x.Status)
                .IsInEnum().WithMessage("Invalid status value");

            RuleFor(x => x.AssignedToUserId)
                .Must(id => id == null || id != Guid.Empty)
                .WithMessage("AssignedToUserId must be a valid Guid if provided");
        }
    }
}
