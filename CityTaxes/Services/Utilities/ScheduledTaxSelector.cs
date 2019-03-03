using System;
using System.Collections.Generic;
using System.Linq;
using CityTaxes.Models;

namespace CityTaxes.Services.Utilities
{
    public class ScheduledTaxSelector : IScheduledTaxSelector
    {
        public TaxRecord GetScheduledTaxForDate(DateTime date, IEnumerable<TaxRecord> taxRecords)
        {
            var recordsForTheDateRange = taxRecords.Where(r => date >= r.StartDate && date <= r.EndDate).ToList();

            TaxRecord scheduledTax = null;

            if (recordsForTheDateRange.Any())
            {
                scheduledTax = recordsForTheDateRange.Min();
            }

            return scheduledTax;
        }
    }
}