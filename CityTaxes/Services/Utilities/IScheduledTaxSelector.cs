using System;
using System.Collections.Generic;
using CityTaxes.Models;

namespace CityTaxes.Services.Utilities
{
    public interface IScheduledTaxSelector
    {
        TaxRecord GetScheduledTaxForDate(DateTime date, IEnumerable<TaxRecord> taxRecords);
    }
}