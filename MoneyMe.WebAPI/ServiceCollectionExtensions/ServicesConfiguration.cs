using MoneyMe.Repositories;
using MoneyMe.Repositories.Interfaces;
using MoneyMe.Services;
using MoneyMe.Services.Interfaces;
using MoneyMe.Services.Validators;

namespace MoneyMe.WebAPI.ServiceCollectionExtensions
{
    public static class ServicesConfiguration
    {
        public static void AddMoneyMeServices(this IServiceCollection services)
        {
            services.AddScoped<IQuoteService, QuoteService>();
            services.AddScoped<IQuoteValidator, QuoteValidator>();
            services.AddScoped<IQuoteRepository, QuoteRepository>();
            
            services.AddScoped<IProductRepository, ProductRepository>();
            services.AddScoped<IProductService, ProductService>();
        }
    }
}
