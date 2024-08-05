using Nexer.Business.Interfaces;
using Nexer.Business.Notifications;
using Nexer.Business.Services;
using Nexer.Data.Context;
using Nexer.Data.Repository;

namespace Nexer.Api.Configurations
{
    public static class DependencyInjectionConfig
    {
        public static IServiceCollection ResolveDependencies(this IServiceCollection services)
        {
            //Data
            services.AddScoped<MyDbContext>();
            services.AddScoped<IProductRepository, ProductRepository>();
            services.AddScoped<ICustomerRepository, CustomerRepository>();

            //Business
            services.AddScoped<IProductService, ProductService>();
            services.AddScoped<ICustomerService, CustomerService>();
            services.AddScoped<INotificator, Notificator>();

            services.AddScoped<IBillingRepository, BillingRepository>();
            services.AddScoped<IBillingService, BillingService>();

            return services;
        }
    }
}
