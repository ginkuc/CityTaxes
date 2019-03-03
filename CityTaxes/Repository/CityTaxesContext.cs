using CityTaxes.Models;
using Microsoft.EntityFrameworkCore;

namespace CityTaxes.Repository
{
    public class CityTaxesContext : DbContext
    {
        public DbSet<TaxRecord> TaxRecords { get; set; }

        public CityTaxesContext(DbContextOptions<CityTaxesContext> options) : base(options) { }
    }
}