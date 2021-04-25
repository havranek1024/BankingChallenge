using BankingChallenge.Core.Abstractions;
using Microsoft.Extensions.DependencyInjection;

namespace BankingChallenge.Infrastructure
{
    public static class ServiceCollectionExtensions
    {
        public static void AddInfrastructure(this IServiceCollection services)
        {
            services.AddTransient<ILoanTermsProvider, AppSettingsLoanTermsProvider>();
        }
    }
}
