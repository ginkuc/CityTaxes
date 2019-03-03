using System;
using System.Collections.Generic;
using System.Linq;
using CityTaxes.Models;

namespace CityTaxes.Services.Utilities
{
    public class TaxRecordModificationValidator : ITaxRecordModificationValidator
    {
        public void ValidateTaxRecordEntry(TaxRecord recordEntry, IEnumerable<TaxRecord> existingTaxRecords)
        {
            var existingRecordForPeriod = existingTaxRecords.FirstOrDefault(r =>
                r.Period == recordEntry.Period && r.StartDate == recordEntry.StartDate && r.EndDate == recordEntry.EndDate);

            if (existingRecordForPeriod != null)
            {
                throw new InvalidOperationException($"A tax record for the specified period {recordEntry.Period} " +
                                                    $"and date range ({recordEntry.StartDate.ToShortDateString()} - {recordEntry.EndDate.ToShortDateString()}) already exists.");
            }
        }
    }
}