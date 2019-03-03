using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using CityTaxes.Models;

namespace CityTaxes.Services
{
    public interface ICityTaxService
    {
        Task<List<TaxRecord>> GetAllTaxRecords();
        Task<TaxRecord> GetTaxRecordById(int id);
        Task<decimal?> GetScheduledTax(string municipality, DateTime date);
        Task UpdateTaxRecord(TaxRecord taxRecord);
        Task<TaxRecord> CreateTaxRecord(TaxRecord taxRecord);
        Task DeleteTaxRecord(int id);
    }
}