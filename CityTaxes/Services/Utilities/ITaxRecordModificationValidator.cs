using System.Collections.Generic;
using CityTaxes.Models;

namespace CityTaxes.Services.Utilities
{
    public interface ITaxRecordModificationValidator
    {
        void ValidateTaxRecordEntry(TaxRecord recordEntry, IEnumerable<TaxRecord> existingTaxRecords);
    }
}