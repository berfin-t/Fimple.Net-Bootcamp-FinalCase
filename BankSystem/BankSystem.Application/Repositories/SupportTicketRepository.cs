using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using BankSystem.Persistence.Context;

namespace BankSystem.Business.Repositories
{
    public class SupportTicketRepository
    {
        private readonly BankingDbContext _context;
        
        public SupportTicketRepository(BankingDbContext context)
        {
            _context = context;
        }

        public async Task GetByIdAsync()
        {

        }

        public async Task GettAllUserAsync()
        {

        }
        public async Task GetAllAsync()
        {

        }
        public async Task UpdateTicketStatusAsync()
        {

        }

        public async Task UpdateTicketPriorityAsync()
        {

        }
    }

}
