using BankingChallenge.Application.Abstractions;
using BankingChallenge.Application.DTO;
using BankingChallenge.Application.Entities;
using System;

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
                EffectiveApr = FormatAmount(loan.CalculateEffectiveApr()),
                MonthlyCost = FormatAmount(loan.Installment),
                TotalInterestAmount = FormatAmount(loan.CalculateTotalInterest()),
                TotalAdministrativeAmount = FormatAmount(loan.CalculateAdministrationFee())
            };
        }

        private decimal FormatAmount(decimal amount) => Math.Round(amount, 2);
    }
}
