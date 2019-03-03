using CityTaxesClient.Contracts;
using System;
using System.Collections.Generic;
using System.Net.Http;
using System.Threading.Tasks;

namespace CityTaxesClient
{
    class Program
    {
        private const string CityTaxesUrl = "http://localhost:5000/api/taxrecords";
        private static readonly HttpClient CityTaxesHttpClient = new HttpClient();

        private const string TestMunicipality = "Vilnius";

        public static async Task Main()
        {
            Console.Title = "Console Client For CityTaxes";

            Console.WriteLine("Seeding initial data.");
            await SeedInitialData();
            Console.WriteLine("Done seeding initial data.");

            Console.WriteLine("Fetching scheduled taxes.");
            await GetScheduledTaxes();
            Console.WriteLine("Done fetching scheduled taxes.");

            Console.ReadLine();
        }

        private static async Task GetScheduledTaxes()
        {
            var inputs = new List<Tuple<string, DateTime>>
            {
                new Tuple<string, DateTime>(TestMunicipality, new DateTime(2016, 1, 1)),
                new Tuple<string, DateTime>(TestMunicipality, new DateTime(2016, 5, 2)),
                new Tuple<string, DateTime>(TestMunicipality, new DateTime(2016, 7, 10)),
                new Tuple<string, DateTime>(TestMunicipality, new DateTime(2016, 3, 16))
            };

            foreach (var inputTuple in inputs)
            {
                var response = await CityTaxesHttpClient.GetStringAsync($"{CityTaxesUrl}/{inputTuple.Item1}/{inputTuple.Item2.ToString("yyyy.MM.dd")}");
                Console.WriteLine($"Response for {inputTuple.Item1} municipality tax for {inputTuple.Item2} is: {response}.");
            }
        }

        private static async Task SeedInitialData()
        {
            foreach (var seedRecord in GetSeedRecords())
            {
                var response = await CityTaxesHttpClient.PostAsJsonAsync(CityTaxesUrl, seedRecord);
                Console.WriteLine(await response.Content.ReadAsStringAsync());
            }
        }

        private static List<TaxRecord> GetSeedRecords()
        {
            return new List<TaxRecord>
            {
                new TaxRecord
                {
                    StartDate = new DateTime(2016, 1, 1),
                    EndDate = new DateTime(2016, 12, 31),
                    Municipality = TestMunicipality,
                    Period = TaxPeriod.Yearly,
                    Value = 0.2m
                },
                new TaxRecord
                {
                    StartDate = new DateTime(2016, 5, 1),
                    EndDate = new DateTime(2016, 5, 31),
                    Municipality = TestMunicipality,
                    Period = TaxPeriod.Monthly,
                    Value = 0.4m
                },
                new TaxRecord
                {
                    StartDate = new DateTime(2016, 1, 1),
                    EndDate = new DateTime(2016, 1, 1),
                    Municipality = TestMunicipality,
                    Period = TaxPeriod.Daily,
                    Value = 0.1m
                },
                new TaxRecord
                {
                    StartDate = new DateTime(2016, 12, 25),
                    EndDate = new DateTime(2016, 12, 25),
                    Municipality = TestMunicipality,
                    Period = TaxPeriod.Daily,
                    Value = 0.1m
                }
            };
        }
    }
}
