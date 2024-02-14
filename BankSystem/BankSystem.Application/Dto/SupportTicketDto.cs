using BankSystem.Application.Dto;
using BankSystem.Data.Enums;
using BankSystem.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BankSystem.Business.Dto
{
    public class SupportTicketDto
    {
        public int Id { get; set; }
        public string Title { get; set; }

        public string Description { get; set; }

        public TicketPriority TicketPriority { get; set; }

        public TicketStatus TicketStatus { get; set; }

        public string UserId { get; set; }

        //Navigation Properties
        public UserDto User { get; set; }
    }
}
