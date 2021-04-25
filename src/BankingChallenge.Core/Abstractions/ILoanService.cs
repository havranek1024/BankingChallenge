using BankingChallenge.Core.DTO;

namespace BankingChallenge.Core.Abstractions
{
    public interface ILoanService
    {
        PaymentOverviewDto CalculateLoan(LoanParametersDto parameters);
    }
}