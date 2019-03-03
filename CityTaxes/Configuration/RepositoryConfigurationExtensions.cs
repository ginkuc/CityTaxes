using CityTaxes.Repository;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

namespace CityTaxes.Configuration
{
    public static class RepositoryConfigurationExtensions
    {
        public static void ConfigureMyRepositories(this IServiceCollection services, IConfiguration configuration)
        {
            var developmentConnectionString = configuration.GetConnectionString("DefaultConnection");
            services.AddDbContext<CityTaxesContext>(options => options.UseSqlServer(developmentConnectionString));

            services.BuildServiceProvider().GetService<CityTaxesContext>().Database.EnsureCreated();
        }
    }
}