using BankingChallenge.Application.Exceptions;
using System;

namespace BankingChallenge.Application.Entities
{
    // calculations taken from https://www.youtube.com/watch?v=3WK3Bc07MJw

    public class Loan
    {
        private decimal? _installment;

        public decimal Amount { get; private set; }
        public int DurationInMonths { get; private set; }
        public LoanTerms Terms { get; private set; }
        public decimal Installment => _installment ??= CalculateInstallment();

        public static Loan Create(decimal amount, int durationInMonths, LoanTerms terms)
        {
            if (amount <= 0)
            {
                throw new ValidationException("Loan amount must be greater than 0");
            }

            if(durationInMonths <= 0)
            {
                throw new ValidationException("Loan duration must be at least one month");
            }

            return new Loan(amount, durationInMonths, terms);
        }

        private Loan(decimal amount, int durationInMonths, LoanTerms terms)
        {
            Amount = amount;
            DurationInMonths = durationInMonths;
            Terms = terms;
        }

        public decimal CalculateAdministrationFee()
        {
            var administrationFee = Amount * Terms.AdministrationFeePercent / 100;
            return Math.Min(administrationFee, Terms.AdministrationFeeMaxAmount);
        }

        public decimal CalculateTotalInterest()
        {
            var totalAmountToPay = Installment * DurationInMonths;
            var totalInterest = totalAmountToPay - Amount;

            return totalInterest;
        }

        private decimal CalculateInstallment()
        {
            var interestMultiplier = 1 + Terms.AnnualInterestRate / (12 * 100);
            var powerOfInterest = (decimal)Math.Pow((double)interestMultiplier, DurationInMonths);

            var installment = Amount * powerOfInterest * ((interestMultiplier - 1) / (powerOfInterest - 1));
            
            return installment;
        }
    }
}
