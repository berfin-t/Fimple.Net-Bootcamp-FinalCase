using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using BankSystem.Persistence.Context;

namespace BankSystem.Business.Repositories
{
    public class PaymentRepository
    {
        private readonly BankingDbContext _context;

        public PaymentRepository(BankingDbContext context)
        {
            _context = context;
        }
        
        public async Task CreateAsync()
        {

        }

        public async Task UpdateAsync()
        {

        }
        
        public async Task DeleteAsync()
        {

        }
    }
}
