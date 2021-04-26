using BankingChallenge.Application.DTO;

namespace BankingChallenge.Application.Abstractions
{
    public interface ILoanService
    {
        PaymentOverviewDto CalculateLoan(LoanParametersDto parameters);
    }
}