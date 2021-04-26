using BankingChallenge.Application.Abstractions;
using BankingChallenge.Application.Infrastructure;
using BankingChallenge.Application.Services;
using Microsoft.Extensions.DependencyInjection;

namespace BankingChallenge.Application
{
    public static class ServiceCollectionExtensions
    {
        public static void AddApplication(this IServiceCollection services)
        {
            services.AddTransient<ILoanTermsProvider, AppSettingsLoanTermsProvider>();
            services.AddTransient<ILoanService, LoanService>();
        }
    }
}
