
using OfflineTicketing.Domain.Enums;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OfflineTicketing.Domain.Entities
{
    public class User
    {
        public Guid Id { get; private set; }
        public string FullName { get; private set; }
        public string Email { get; private set; }
        public string Password { get; private set; }
        public Role Role { get; private set; }

        private readonly List<Ticket> _createdTickets = new();
        private readonly List<Ticket> _assignedTickets = new();

        public IReadOnlyCollection<Ticket> CreatedTickets => _createdTickets.AsReadOnly();
        public IReadOnlyCollection<Ticket> AssignedTickets => _assignedTickets.AsReadOnly();

        private User() { } // EF Core

        public User(string fullName, string email, string password, Role role)
        {
            Id = Guid.NewGuid();
            FullName = fullName ?? throw new ArgumentNullException(nameof(fullName));
            Email = email ?? throw new ArgumentNullException(nameof(email));
            Password = password ?? throw new ArgumentNullException(nameof(password));
            if (!Enum.IsDefined(typeof(Role), role))
                throw new ArgumentException("Invalid role value", nameof(role));
        }

        public void AddCreatedTicket(Ticket ticket)
        {
            if (ticket == null) throw new ArgumentNullException(nameof(ticket));
            _createdTickets.Add(ticket);
        }

        public void AssignTicket(Ticket ticket)
        {
            if (ticket == null) throw new ArgumentNullException(nameof(ticket));
            _assignedTickets.Add(ticket);
            ticket.AssignToUser(this);
        }
    }

}
