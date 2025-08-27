using OfflineTicketing.Domain.Enums;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OfflineTicketing.Domain.Entities
{
    public class TicketHistory
    {
        public Guid Id { get; set; } = Guid.NewGuid();
        public Guid TicketId { get; set; }
        public TicketStatus OldStatus { get; set; }
        public TicketStatus NewStatus { get; set; }
        public DateTime ChangedAt { get; set; } = DateTime.UtcNow;
        public Guid ChangedByUserId { get; set; }
    }
}
