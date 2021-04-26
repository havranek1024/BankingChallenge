using BankingChallenge.Application.Models;

namespace BankingChallenge.Application.Abstractions
{
    public interface ILoanTermsProvider
    {
        LoanTerms GetLoanTerms();
    }
}
