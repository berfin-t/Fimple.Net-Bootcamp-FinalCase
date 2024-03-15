using AutoMapper;
using BankSystem.Application.Dto;
using BankSystem.Data.Enums;
using BankSystem.Domain.Entities;
using BankSystem.Persistence.Context;
using DocumentFormat.OpenXml.Spreadsheet;
using Microsoft.Identity.Client;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BankSystem.Business.Repositories
{
    public class LoanApplicationRepository
    {
        private readonly BankingDbContext _context;
        private readonly IMapper _mapper;
        private readonly CreditScoreRepository creditScoreRepository;

        public LoanApplicationRepository(BankingDbContext context, IMapper mapper, CreditScoreRepository creditScoreRepository)
        {
            _context = context;
            _mapper = mapper;
            this.creditScoreRepository = creditScoreRepository;
        }

        public async Task CreateAsync(LoanApplicationModel loanApplicationModel, string loanType, string loanApplicationStatus)
        {
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

            _context.LoanApplication.Add(newLoanApplication);
            await _context.SaveChangesAsync();
        }

        public async Task ApproveApplicationAsync(int aplicationId)
        {
            var application = _context.LoanApplication.FirstOrDefault(a => a.Id == aplicationId);
           
            decimal requiredCreditScore = creditScoreRepository.CalculateMinimumRequiredCreditScoreForLoanApplication(application);
            decimal userCreditScore = creditScoreRepository.CalculateCreditScore(application.User);

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

            public async Task GetLoanApplicationByStatusAsync()
        {

        }

        public async Task ProcessApplicationAsync()
        {

        }

        public async Task RejectApplicationAsync()
        {

        }

    }
}
