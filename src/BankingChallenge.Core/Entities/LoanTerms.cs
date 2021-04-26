namespace BankingChallenge.Application.Entities
{
    public record LoanTerms(decimal AnnualInterestRate, decimal AdministrationFeePercent, decimal AdministrationFeeMaxAmount)
    {
        public static readonly LoanTerms Default = new(
            AnnualInterestRate: 5,
            AdministrationFeePercent: 1,
            AdministrationFeeMaxAmount: 10000
        );
    }
}
