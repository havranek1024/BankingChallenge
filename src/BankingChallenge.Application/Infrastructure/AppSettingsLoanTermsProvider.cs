using BankingChallenge.Application.Abstractions;
using BankingChallenge.Application.Entities;
using Microsoft.Extensions.Configuration;

namespace BankingChallenge.Application.Infrastructure
{
    public class AppSettingsLoanTermsProvider : ILoanTermsProvider
    {
        private readonly IConfiguration _configuration;

        public AppSettingsLoanTermsProvider(IConfiguration configuration)
        {
            _configuration = configuration;
        }

        public LoanTerms GetLoanTerms()
        {
            var defaultTerms = LoanTerms.Default;

            var configuredTerms = _configuration.GetSection("LoanTerms").Get<LoanTermsSettingsModel>();
            if (configuredTerms == null)
            {
                return defaultTerms;
            }

            return new LoanTerms(
            
                AnnualInterestRate: configuredTerms.AnnualInterestRate ?? defaultTerms.AnnualInterestRate,
                AdministrationFeePercent: configuredTerms.AdministrationFeePercent ?? defaultTerms.AdministrationFeePercent,
                AdministrationFeeMaxAmount: configuredTerms.AdministrationFeeMaxAmount ?? defaultTerms.AdministrationFeeMaxAmount
            );
        }

        private class LoanTermsSettingsModel
        {
            public decimal? AnnualInterestRate { get; set; }
            public decimal? AdministrationFeePercent { get; set; }
            public decimal? AdministrationFeeMaxAmount { get; set; }
        }
    }
}
