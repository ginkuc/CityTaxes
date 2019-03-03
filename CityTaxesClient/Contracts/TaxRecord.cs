using System;

namespace CityTaxesClient.Contracts
{
    public class TaxRecord
    {
        public int Id { get; set; }

        public string Municipality { get; set; }

        public DateTime StartDate { get; set; }

        public DateTime EndDate { get; set; }

        public decimal Value { get; set; }

        public TaxPeriod Period { get; set; }
    }
}