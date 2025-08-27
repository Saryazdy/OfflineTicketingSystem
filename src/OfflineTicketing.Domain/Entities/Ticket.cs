using OfflineTicketing.Domain.Enums;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OfflineTicketing.Domain.Entities
{

    public class Ticket
    {
        public Guid Id { get; set; } = Guid.NewGuid();
        public string Title { get; set; } = null!;
        public string Description { get; set; } = null!;
        public TicketStatus Status { get; set; } = TicketStatus.Open;
        public TicketPriority Priority { get; set; } = TicketPriority.Medium;
        public DateTime CreatedAt { get; set; } = DateTime.UtcNow;
        public DateTime UpdatedAt { get; set; } = DateTime.UtcNow;
        public DateTime? DueDate { get; set; }

        public Guid CreatedByUserId { get; set; }
        public User CreatedByUser { get; set; } = null!;
        public Guid? AssignedToUserId { get; set; }
        public User? AssignedToUser { get; set; }

        public bool IsDeleted { get; set; } = false;
    }
}
