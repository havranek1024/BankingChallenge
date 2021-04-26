namespace BankingChallenge.Application.Models
{
    public class LoanTerms
    {
        public static readonly LoanTerms Default = new LoanTerms
        {
            AnnualInterestRate = 5,
            AdministrationFeePercent = 1,
            AdministrationFeeMaxAmount = 10000
        };

        public decimal AnnualInterestRate { get; set; }
        public decimal AdministrationFeePercent { get; set; }
        public decimal AdministrationFeeMaxAmount { get; set; }
    }
}
