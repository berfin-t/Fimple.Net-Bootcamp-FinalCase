using BankSystem.Data.Enums;
using BankSystem.Persistence.Context;
using BankSystem.Data.Entities;
using AutoMapper;

namespace BankSystem.Business.Repositories
{
    public class PaymentRepository
    {
        private readonly BankingDbContext _context;
        private readonly IMapper _mapper;

        public PaymentRepository(BankingDbContext context, IMapper mapper)
        {
            _context = context;
            _mapper = mapper;
        }

        public async Task CreateAsync(PaymentModel paymentModel, string timePeriod)
        {
            if (!Enum.TryParse<TimePeriod>(timePeriod, out var timePeriodEnum))
            {
                throw new ArgumentException("Invalid payment type", nameof(timePeriod));
            }

            var newPayment = _mapper.Map<PaymentModel>(paymentModel);
            newPayment.TimePeriod = timePeriodEnum;

            _context.Payment.Add(newPayment);
            await _context.SaveChangesAsync();
        }

        public async Task UpdateAsync(int paymentId, decimal amount)
        {
            var payment = _context.Payment.Find(paymentId);
            if (payment != null) 
            {
                payment.Amount = amount;
                _context.SaveChanges();
            }
        }
        
        public async Task DeleteAsync()
        {

        }
    }
}
