using BankSystem.Data.Enums;
using DocumentFormat.OpenXml.Spreadsheet;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using BankSystem.Domain.Entities;

namespace BankSystem.Business.Repositories
{
    public class CreditScoreRepository
    {
        private const decimal IncomeScoreMultiplier = 20m;
        private const decimal AssetScoreMultiplier = 15m;
        private const decimal DebtBurdenScoreMultiplier = 25m;
        private const decimal PaymentPerformanceScoreMultiplier = 30m;
        private const decimal LoanAmountScoreMultiplier = 5m;
        private const decimal LoanTermScoreMultiplier = 10m;
        private const int ThousandDollars = 1000;
        private const int TenThousandDollars = 10000;
        private const int TwelveMonths = 12;

        private const int MortgageScore = 30;
        private const int EducationLoanScore = 25;
        private const int PersonalLoanScore = 15;
        private const int VehicleLoanScore = 20;
        private const int SmallBusinessLoanScore = 35;

        public decimal CalculateCreditScore(UserModel userModel, List<LoanModel> loans)
        {
            var incomeScore = userModel.AnnualIncome / ThousandDollars * IncomeScoreMultiplier;
            var totalAssetsScore = userModel.TotalAssets / ThousandDollars * AssetScoreMultiplier;

            decimal loanTypeScore = 0;
            decimal remainingDebtBurdenScore = 0;
            decimal paymentPerformanceScore = 0;

            if (loans.Any())
            {
                foreach (var userLoan in loans)
                {
                    switch (userLoan.LoanType)
                    {
                        case LoanType.Mortgage:
                            loanTypeScore += MortgageScore;
                            break;
                        case LoanType.Education:
                            loanTypeScore += EducationLoanScore;
                            break;
                        case LoanType.Personal:
                            loanTypeScore += PersonalLoanScore;
                            break;
                        case LoanType.Vehicle:
                            loanTypeScore += VehicleLoanScore;
                            break;
                        case LoanType.SmallBusiness:
                            loanTypeScore += SmallBusinessLoanScore;
                            break;
                    }

                    remainingDebtBurdenScore = (1 - userLoan.RemainingDebt / userLoan.LoanAmount) * DebtBurdenScoreMultiplier;
                }
            }

            var loansStartedToBePaid = loans.Where(loan => loan.NumberOfTotalPayments > 0).ToList();

            if (loansStartedToBePaid.Any())
            {
                paymentPerformanceScore = loansStartedToBePaid
                    .Sum(loan => loan.NumberOfTimelyPayments / loan.NumberOfTotalPayments * PaymentPerformanceScoreMultiplier);
            }

            return incomeScore + totalAssetsScore + paymentPerformanceScore + remainingDebtBurdenScore + loanTypeScore;
        }

        public decimal CalculateMinimumRequiredCreditScoreForLoanApplication(LoanApplicationModel loanApplicationModel)
        {
            decimal score = 0;

            switch (loanApplicationModel.LoanType)
            {
                case LoanType.Mortgage:
                    score += MortgageScore;
                    break;
                case LoanType.Education:
                    score += EducationLoanScore;
                    break;
                case LoanType.Personal:
                    score += PersonalLoanScore;
                    break;
                case LoanType.Vehicle:
                    score += VehicleLoanScore;
                    break;
                case LoanType.SmallBusiness:
                    score += SmallBusinessLoanScore;
                    break;
            }

            score += loanApplicationModel.LoanAmount / TenThousandDollars * LoanAmountScoreMultiplier;
            score += loanApplicationModel.LoanTerm / TwelveMonths * LoanTermScoreMultiplier;

            return score;
        }
    }
}
