using BankingChallenge.Core.Abstractions;
using BankingChallenge.Core.DTO;

namespace BankingChallenge.Core.Services
{
    public class LoanService : ILoanService
    {
        private readonly ILoanTermsProvider _termsProvider;

        public LoanService(ILoanTermsProvider termsProvider)
        {
            _termsProvider = termsProvider;
        }

        public PaymentOverviewDto CalculateLoan(LoanParametersDto parameters)
        {
            return new PaymentOverviewDto
            {
                EffectiveApr = 5,
                MonthlyCost = 1100,
                TotalInterestAmount = 15,
                TotalAdministrativeAmount = 30
            };
        }
    }
}
