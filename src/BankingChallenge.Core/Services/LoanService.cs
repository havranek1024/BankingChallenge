using BankingChallenge.Application.Abstractions;
using BankingChallenge.Application.DTO;
using BankingChallenge.Application.Entities;

namespace BankingChallenge.Application.Services
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
            var terms = _termsProvider.GetLoanTerms();
            var loan = Loan.Create(parameters.Amount, parameters.DurationInMonths, terms);

            return new PaymentOverviewDto
            {
                EffectiveApr = 5,
                MonthlyCost = loan.Installment,
                TotalInterestAmount = loan.CalculateTotalInterest(),
                TotalAdministrativeAmount = loan.CalculateAdministrationFee()
            };
        }
    }
}
