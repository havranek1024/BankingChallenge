namespace BankingChallenge.Core.DTO
{
    public class PaymentOverviewDto
    {
        public decimal EffectiveApr { get; set; }
        public decimal MonthlyCost { get; set; }
        public decimal TotalInterestAmount { get; set; }
        public decimal TotalAdministrativeAmount { get; set; }
    }
}