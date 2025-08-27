using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OfflineTicketing.Application.Tickets.Dtos
{
    public class TicketStatsDto
    {
        public int Open { get; set; }
        public int InProgress { get; set; }
        public int Closed { get; set; }
    }
}
