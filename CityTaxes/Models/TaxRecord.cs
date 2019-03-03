using System;
using System.ComponentModel.DataAnnotations;

namespace CityTaxes.Models
{
    public class TaxRecord : IComparable<TaxRecord>
    {
        public int Id { get; set; }

        [Required]
        public string Municipality { get; set; }

        [Required]
        [DataType(DataType.Date)]
        [DisplayFormat(ApplyFormatInEditMode = true, DataFormatString = "{0:yyyy.MM.dd}")]
        public DateTime StartDate { get; set; }

        [Required]
        [DataType(DataType.Date)]
        [DisplayFormat(ApplyFormatInEditMode = true, DataFormatString = "{0:yyyy.MM.dd}")]
        public DateTime EndDate { get; set; }

        [Required]
        public decimal Value { get; set; }

        [Required]
        public TaxPeriod Period { get; set; }

        public int CompareTo(TaxRecord other)
        {
            if (Period > other.Period)
                return 1;
            if (Period < other.Period)
                return -1;
            return 0;
        }
    }
}