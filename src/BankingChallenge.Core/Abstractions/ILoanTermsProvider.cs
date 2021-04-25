using BankingChallenge.Core.Models;

namespace BankingChallenge.Core.Abstractions
{
    public interface ILoanTermsProvider
    {
        LoanTerms GetLoanTerms();
    }
}
