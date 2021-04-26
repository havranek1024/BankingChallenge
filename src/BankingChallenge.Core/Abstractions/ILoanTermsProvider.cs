using BankingChallenge.Application.Entities;

namespace BankingChallenge.Application.Abstractions
{
    public interface ILoanTermsProvider
    {
        LoanTerms GetLoanTerms();
    }
}
