using System;
using System.Collections.Generic;
using System.Formats.Asn1;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using LicensePurchaseCalculator.Interfaces.Providers;
using LicensePurchaseCalculator.Models;
using CsvHelper;
using CsvHelper.Configuration;

namespace LicensePurchaseCalculator.Implementations.Providers
{
    public class DataReaderProvider : IDataReaderProvider
    {
        /// Streams installation records from CSV without loading entire file into memory.
        /// Suitable for very large files (1GB+).
        public IEnumerable<AppInstallationModel> ReadInstallations(string filePath)
        {
            using var fs = File.OpenRead(filePath);
            using var sr = new StreamReader(fs);

            var config = new CsvConfiguration(CultureInfo.InvariantCulture)
            {
                HasHeaderRecord = true,
                MissingFieldFound = null,
                BadDataFound = null,
                HeaderValidated = null,
                PrepareHeaderForMatch = args => args.Header.Trim()
            };

            using var csv = new CsvReader(sr, config);
            csv.Read();
            csv.ReadHeader();

            while (csv.Read())
            {
                var model = new AppInstallationModel
                {
                    ComputerID = int.Parse(csv.GetField("ComputerID")!),
                    UserID = int.Parse(csv.GetField("UserID")!),
                    ApplicationID = int.Parse(csv.GetField("ApplicationID")!),
                    ComputerType = csv.GetField("ComputerType")?.Trim() ?? "",
                    Comment = csv.GetField("Comment")
                };

                yield return model;
            }
        }
    }
}
