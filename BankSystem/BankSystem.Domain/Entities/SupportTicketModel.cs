using BankSystem.Domain.Entities;
using DocumentFormat.OpenXml.Spreadsheet;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using BankSystem.Data.Enums;

namespace BankSystem.Data.Entities
{
    public class SupportTicketModel
    {
        public int Id { get; set; }
        public string Title { get; set; }
        public string Description { get; set; }
        public TicketPriority TicketPriority { get; set; }
        public TicketStatus TicketStatus { get; set; }
        public int UserId { get; set; }

        //Navigation Properties
        public UserModel User { get; set; }
    }
}
