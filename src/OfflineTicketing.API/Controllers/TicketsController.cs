using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using OfflineTicketing.Application.Tickets.Commands.CreateTicket;
using OfflineTicketing.Application.Tickets.Commands.DeleteTicket;
using OfflineTicketing.Application.Tickets.Commands.UpdateTicket;
using OfflineTicketing.Application.Tickets.Queries.GetAllTickets;
using OfflineTicketing.Application.Tickets.Queries.GetMyTickets;
using OfflineTicketing.Application.Tickets.Queries.GetTicketById;
using OfflineTicketing.Application.Tickets.Queries.GetTicketStats;
using System.Security.Claims;

namespace OfflineTicketing.API.Controllers
{
    [ApiController]
    [Route("tickets")]
    [Authorize]
    public class TicketsController : ControllerBase
    {
        private readonly IMediator _mediator;

        public TicketsController(IMediator mediator)
        {
            _mediator = mediator;
        }

        private Guid CurrentUserId => Guid.Parse(User.FindFirstValue(ClaimTypes.NameIdentifier) ?? Guid.Empty.ToString());

        [HttpPost]
        [Authorize(Roles = "Employee")]
        public async Task<IActionResult> Create([FromBody] CreateTicketCommand command)
        {
            // ست کردن کاربر جاری
            var commandWithUser = command with { CreatedByUserId = CurrentUserId };

            var ticket = await _mediator.Send(commandWithUser);
            return Ok(ticket);
        }

        [HttpGet("my")]
        [Authorize(Roles = "Admin,Employee")]
        public async Task<IActionResult> GetMyTickets([FromQuery] int pageNumber = 1, [FromQuery] int pageSize = 10)
        {
            var query = new GetMyTicketsQuery
            {
                UserId = CurrentUserId,
                PageNumber = pageNumber,
                PageSize = pageSize
            };

            var tickets = await _mediator.Send(query);
            return Ok(tickets);
        }

        [HttpGet]
        [Authorize(Roles = "Admin")]
        public async Task<IActionResult> GetAll([FromQuery] int pageNumber = 1, [FromQuery] int pageSize = 10)
        {
            var query = new GetAllTicketsQuery
            {
                PageNumber = pageNumber,
                PageSize = pageSize
            };

            var tickets = await _mediator.Send(query);
            return Ok(tickets);
        }

        [HttpGet("{id}")]
        [Authorize(Roles = "Admin,Employee")]
        public async Task<IActionResult> GetById(Guid id)
        {
            var ticket = await _mediator.Send(new GetTicketByIdQuery { TicketId = id });
            if (ticket == null) return NotFound();
            return Ok(ticket);
        }

        [HttpPut("{id}")]
        [Authorize(Roles = "Admin")]
        public async Task<IActionResult> Update(Guid id, [FromBody] UpdateTicketCommand command)
        {
            command.Id = id;
            var result = await _mediator.Send(command);
            if (!result) return NotFound();
            return NoContent();
        }

        [HttpDelete("{id}")]
        [Authorize(Roles = "Admin")]
        public async Task<IActionResult> Delete(Guid id)
        {
            var result = await _mediator.Send(new DeleteTicketCommand { Id = id });
            if (!result) return NotFound();
            return NoContent();
        }

        [HttpGet("stats")]
        [Authorize(Roles = "Admin")]
        public async Task<IActionResult> GetStats()
        {
            var stats = await _mediator.Send(new GetTicketStatsQuery());
            return Ok(stats);
        }
    }
}

