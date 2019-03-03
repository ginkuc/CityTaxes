using CityTaxes.Services;
using CityTaxes.Services.Utilities;
using Microsoft.Extensions.DependencyInjection;

namespace CityTaxes.Configuration
{
    public static class DependencyConfigurationExtensions
    {
        public static void ConfigureMyServices(this IServiceCollection services)
        {
            services.AddTransient<IScheduledTaxSelector, ScheduledTaxSelector>();
            services.AddTransient<ITaxRecordModificationValidator, TaxRecordModificationValidator>();
            services.AddTransient<ICityTaxService, CityTaxService>();
        }
    }
}