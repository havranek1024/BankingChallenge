using BankingChallenge.Application.Exceptions;
using System;

namespace BankingChallenge.Application.Entities
{
    public class Loan
    {
        private decimal? _installment;

        public LoanTerms Terms { get; private set; }
        public decimal Amount { get; private set; }
        public int DurationInMonths { get; private set; }
        public decimal Installment => _installment ??= CalculateInstallment();

        public static Loan Create(decimal amount, int durationInMonths, LoanTerms terms)
        {
            if (amount <= 0)
            {
                throw new ValidationException("Loan amount must be greater than 0");
            }

            if (durationInMonths <= 0)
            {
                throw new ValidationException("Loan duration must be at least one month");
            }

            if (terms == null)
            {
                throw new ValidationException("Loan terms must be provided");
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

        public decimal CalculateEffectiveApr()
        {
            // formulas taken from https://www.omnicalculator.com/finance/apr

            var apr = CalculateApr();

            var periodicApr = 1 + apr / (12 * 100);
            var effectiveApr = (decimal)Math.Pow((double)periodicApr, 12) - 1;

            return effectiveApr * 100;
        }

        private decimal CalculateApr()
        {
            var guess = Terms.AnnualInterestRate;
            var difference = 1m;
            var amountToAdd = 0.0001m;

            var borrowedAmount = Amount - CalculateAdministrationFee();

            while (difference != 0)
            {
                var terms = new LoanTerms(guess, 0, 0);
                var loan = new Loan(borrowedAmount, DurationInMonths, terms);

                difference = Installment - loan.Installment;

                if (difference <= 0.0000001m && difference >= -0.0000001m)
                {
                    break;
                }

                if (difference > 0)
                {
                    amountToAdd = amountToAdd * 2;
                    guess = guess + amountToAdd;
                }

                else
                {
                    amountToAdd = amountToAdd / 2;
                    guess = guess - amountToAdd;
                }
            }

            return guess;
        }

        private decimal CalculateInstallment()
        {
            // formulas taken from https://www.youtube.com/watch?v=3WK3Bc07MJw

            var interestMultiplier = 1 + Terms.AnnualInterestRate / (12 * 100);
            var powerOfInterest = (decimal)Math.Pow((double)interestMultiplier, DurationInMonths);

            var installment = Amount * powerOfInterest * ((interestMultiplier - 1) / (powerOfInterest - 1));
            
            return installment;
        }
    }
}
