using BankingChallenge.Application.Abstractions;
using BankingChallenge.Application.DTO;
using BankingChallenge.Application.Models;
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
            //validation - create new exception type

            var terms = _termsProvider.GetLoanTerms();

            // calculations taken from https://www.youtube.com/watch?v=3WK3Bc07MJw

            var administrationFee = GetAdministrationFee(parameters.Amount, terms);

            var interest = 1 + (terms.AnnualInterestRate / 12) / 100;
            var powerOfInterest = (decimal)Math.Pow((double)interest, parameters.DurationInMonths);

            var installment = parameters.Amount * powerOfInterest * ((interest - 1) / (powerOfInterest - 1));
            var totalAmountToPay = installment * parameters.DurationInMonths;
            var totalInterest = totalAmountToPay - parameters.Amount;

            //EMI = [P x R x(1 + R) ^ N] /[(1 + R) ^ N - 1], where P stands for the loan amount or principal, R is the interest rate per month[if the interest rate per annum is 11 %, then the rate of interest will be 11 / (12 x 100)], and N is the number of monthly instalments.

            return new PaymentOverviewDto
            {
                EffectiveApr = 5,
                MonthlyCost = installment,
                TotalInterestAmount = totalInterest,
                TotalAdministrativeAmount = administrationFee
            };
        }

        private decimal GetAdministrationFee(decimal amount, LoanTerms terms)
        {
            var administrationFee = amount * terms.AdministrationFeePercent / 100;
            return Math.Min(administrationFee, terms.AdministrationFeeMaxAmount);
        }
    }
}
