using MoneyMe.Repositories;
using MoneyMe.Repositories.Interfaces;
using MoneyMe.Services;
using MoneyMe.Services.Interfaces;

namespace MoneyMe.WebAPI.ServiceCollectionExtensions
{
    public static class ServicesConfiguration
    {
        public static void AddMoneyMeServices(this IServiceCollection services)
        {
            services.AddScoped<IQuoteService, QuoteService>();
            services.AddScoped<IQuoteRepository, QuoteRepository>();
        }
    }
}
