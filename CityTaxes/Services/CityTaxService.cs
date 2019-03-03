using CityTaxes.Models;
using CityTaxes.Repository;
using CityTaxes.Services.Utilities;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace CityTaxes.Services
{
    public class CityTaxService : ICityTaxService
    {
        private readonly CityTaxesContext _cityTaxesContext;
        private readonly ITaxRecordModificationValidator _taxRecordModificationValidator;
        private readonly IScheduledTaxSelector _scheduledTaxSelector;

        public CityTaxService(CityTaxesContext cityTaxesContext,
            ITaxRecordModificationValidator taxRecordModificationValidator,
            IScheduledTaxSelector scheduledTaxSelector)
        {
            _cityTaxesContext = cityTaxesContext;
            _taxRecordModificationValidator = taxRecordModificationValidator;
            _scheduledTaxSelector = scheduledTaxSelector;
        }

        public async Task<List<TaxRecord>> GetAllTaxRecords()
        {
            return await _cityTaxesContext.TaxRecords.ToListAsync();
        }

        public async Task<TaxRecord> GetTaxRecordById(int id)
        {
            return await _cityTaxesContext.TaxRecords.FindAsync(id);
        }

        public async Task<decimal?> GetScheduledTax(string municipality, DateTime date)
        {
            var municipalityTaxes = await _cityTaxesContext.TaxRecords.Where(r => r.Municipality == municipality).ToListAsync();

            var scheduledTax = _scheduledTaxSelector.GetScheduledTaxForDate(date, municipalityTaxes);

            decimal? scheduledTaxValue = null;
            if (scheduledTax != null)
            {
                scheduledTaxValue = scheduledTax.Value;
            }

            return scheduledTaxValue;
        }

        public async Task UpdateTaxRecord(TaxRecord taxRecord)
        {
            try
            {
                ValidateTaxRecord(taxRecord);

                var trackedTaxRecord = await _cityTaxesContext.TaxRecords.FindAsync(taxRecord.Id);
                UpdateTaxRecordValues(trackedTaxRecord, taxRecord);

                _cityTaxesContext.Update(trackedTaxRecord);

                await _cityTaxesContext.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!TaxRecordExists(taxRecord.Id))
                {
                    throw new InvalidOperationException($"Tax record (Id = {taxRecord.Id}) doesn't exist.");
                }

                throw new InvalidOperationException("Unable to update tax record. Please try again later.");
            }
        }

        private static void UpdateTaxRecordValues(TaxRecord trackedTaxRecord, TaxRecord taxRecord)
        {
            trackedTaxRecord.Period = taxRecord.Period;
            trackedTaxRecord.EndDate = taxRecord.EndDate;
            trackedTaxRecord.StartDate = taxRecord.StartDate;
            trackedTaxRecord.Municipality = taxRecord.Municipality;
            trackedTaxRecord.Value = taxRecord.Value;
        }

        public async Task<TaxRecord> CreateTaxRecord(TaxRecord taxRecord)
        {
            ValidateTaxRecord(taxRecord);

            try
            {
                _cityTaxesContext.TaxRecords.Add(taxRecord);
                await _cityTaxesContext.SaveChangesAsync();
            }
            catch (InvalidOperationException)
            {
                throw new InvalidOperationException($"Tax record with id = {taxRecord.Id} already exists.");
            }

            return taxRecord;
        }

        public async Task DeleteTaxRecord(int id)
        {
            var taxRecord = await _cityTaxesContext.TaxRecords.FindAsync(id);
            if (taxRecord != null)
            {
                _cityTaxesContext.TaxRecords.Remove(taxRecord);
                await _cityTaxesContext.SaveChangesAsync();
            }
        }

        private void ValidateTaxRecord(TaxRecord taxRecord)
        {
            var currentTaxesForMunicipality = _cityTaxesContext.TaxRecords.Where(r => r.Municipality == taxRecord.Municipality);
            _taxRecordModificationValidator.ValidateTaxRecordEntry(taxRecord, currentTaxesForMunicipality);
        }

        private bool TaxRecordExists(int id)
        {
            return _cityTaxesContext.TaxRecords.Any(e => e.Id == id);
        }
    }
}