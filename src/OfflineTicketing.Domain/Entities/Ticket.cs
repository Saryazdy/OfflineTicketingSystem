
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
        public Guid Id { get; private set; }
        public string Title { get; private set; }
        public string Description { get; private set; }
        public TicketStatus Status { get; private set; }
        public TicketPriority Priority { get; private set; }
        public DateTime CreatedAt { get; private set; }
        public DateTime UpdatedAt { get; private set; }

        public Guid CreatedByUserId { get; private set; }
        public User CreatedByUser { get; private set; }

        public Guid? AssignedToUserId { get; private set; }
        public User? AssignedToUser { get; private set; }

        public bool IsDeleted { get; private set; }

        private Ticket() { } // EF Core

        public Ticket(string title, string description, Guid createdByUserId, TicketPriority priority = TicketPriority.Medium)
        {
            Id = Guid.NewGuid();
            Title = title ?? throw new ArgumentNullException(nameof(title));
            Description = description ?? throw new ArgumentNullException(nameof(description));
            CreatedByUserId = createdByUserId;
            Priority = priority;
            Status = TicketStatus.Open;
            CreatedAt = DateTime.Now;
            UpdatedAt = DateTime.Now;
     
        }

        public void AssignToUser(User user)
        {
            AssignedToUser = user ?? throw new ArgumentNullException(nameof(user));
            AssignedToUserId = user.Id;
            UpdatedAt = DateTime.UtcNow;
        }

        public void ChangeStatus(TicketStatus newStatus)
        {
            Status = newStatus;
            UpdatedAt = DateTime.UtcNow;
        }

        public void MarkAsDeleted()
        {
            IsDeleted = true;
            UpdatedAt = DateTime.UtcNow;
        }

        public void UpdateDetails(string title, string description, TicketPriority priority)
        {
            Title = title ?? throw new ArgumentNullException(nameof(title));
            Description = description ?? throw new ArgumentNullException(nameof(description));
            Priority = priority;
       
            UpdatedAt = DateTime.UtcNow;
        }
    }

}
