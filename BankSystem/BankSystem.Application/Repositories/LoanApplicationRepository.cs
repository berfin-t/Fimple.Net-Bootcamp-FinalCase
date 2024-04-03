using AutoMapper;
using BankSystem.Data.Enums;
using BankSystem.Domain.Entities;
using BankSystem.Persistence.Context;
using System.Security.Claims;

namespace BankSystem.Business.Repositories
{
    public class LoanApplicationRepository
    {
        private readonly BankingDbContext _context;
        private readonly IMapper _mapper;
        private readonly CreditScoreRepository _creditScoreRepository;

        public LoanApplicationRepository(BankingDbContext context, 
            IMapper mapper,
            CreditScoreRepository creditScoreRepository
            )
        {
            _context = context;
            _mapper = mapper;
            _creditScoreRepository = creditScoreRepository;
        }
        private int UserIdClaimControl(ClaimsPrincipal user)
        {
            var userIdClaim = user.FindFirst(c => c.Type == ClaimTypes.NameIdentifier);
            return userIdClaim != null ? int.Parse(userIdClaim.Value) : 0;
        }

        public async Task CreateAsync(LoanApplicationModel loanApplicationModel, string loanType, string loanApplicationStatus, ClaimsPrincipal user)
        {
            var userIdClaim = UserIdClaimControl(user);

                if (!Enum.TryParse<LoanType>(loanType, out var loanTypeEnum))
                {
                    throw new ArgumentException("Invalid account type", nameof(loanType));
                }
            if (!Enum.TryParse<LoanApplicationStatus>(loanApplicationStatus, out var loanApplicationStatusEnum))
            {
                throw new ArgumentException("Invalid account type", nameof(loanApplicationStatus));
            }
            var newLoanApplication = _mapper.Map<LoanApplicationModel>(loanApplicationModel);
            newLoanApplication.LoanType = loanTypeEnum;
            newLoanApplication.LoanApplicationStatus = loanApplicationStatusEnum;
            newLoanApplication.UserId = userIdClaim;

            _context.LoanApplication.Add(newLoanApplication);
            await _context.SaveChangesAsync();
        }

        public async Task ApproveApplicationAsync(int aplicationId)
        {          

            var application = _context.LoanApplication.FirstOrDefault(a => a.Id == aplicationId);
            var user = _context.User.FirstOrDefault(a => a.UserId == application.UserId);
            var loans = _context.Loan.Where(a => a.UserId == user.UserId).ToList();

            decimal requiredCreditScore = _creditScoreRepository.CalculateMinimumRequiredCreditScoreForLoanApplication(application);
            decimal userCreditScore = _creditScoreRepository.CalculateCreditScore(user, loans);

            if (userCreditScore >= requiredCreditScore)
            {
                application.LoanApplicationStatus = LoanApplicationStatus.Approved;
            }
            else
            {
                application.LoanApplicationStatus = LoanApplicationStatus.Rejected;

            }
            await _context.SaveChangesAsync();

        }

        public async Task<LoanApplicationModel?> GetLoanApplicationByStatusAsync(int statusId)
        {
            return _context.LoanApplication.Find(statusId);
        }

        public async Task ProcessApplicationAsync()
        {

        }

        public async Task RejectApplicationAsync()
        {

        }

    }
}
