using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using LicensePurchaseCalculator.Interfaces.Providers;
using LicensePurchaseCalculator.Models;
using LicensePurchaseCalculator.Utilities;
using Microsoft.Extensions.Logging;

namespace LicensePurchaseCalculator.Implementations.Providers
{
    public class LicenseCalculationProvider : ILicenseCalculationProvider
    {
        //private readonly ILogger<LicenseCalculationProvider> _logger;
        public LicenseCalculationProvider()//ILogger<LicenseCalculationProvider> logger)
        {
            //_logger = logger;
        }
        /// Calculates the minimum number of licenses required per application.
        /// Processes installation records in a streaming manner to support very large files.
        /// Performs filtering, deduplication, and per-user aggregation.
        public int CalculateMinimumLicenses(IEnumerable<AppInstallationModel> stream, int applicationId)
        {
            var filteredData = FilterByApp(stream, applicationId);
            var uniqueData = RemoveDuplicates(filteredData);
            var perUser = AggregatePerUser(uniqueData);
            var numberOfLicenses = SumLicenses(perUser);
            return numberOfLicenses;
        }
        internal IEnumerable<AppInstallationModel> FilterByApp(IEnumerable<AppInstallationModel> stream, int appId)
        {
            foreach (var row in stream)
            {
                if (row.ApplicationID == appId)
                    yield return row;
            }
        }
        internal IEnumerable<AppInstallationModel> RemoveDuplicates(IEnumerable<AppInstallationModel> stream)
        {
            var seen = new HashSet<string>();

            foreach (var row in stream)
            {
                var key = $"{row.ApplicationID}|{row.UserID}|{row.ComputerID}";
                if (seen.Add(key))
                {
                    yield return row;
                }
            }
        }
        internal Dictionary<int, UserDeviceUsage> AggregatePerUser(IEnumerable<AppInstallationModel> stream)
        {
            var perUser = new Dictionary<int, UserDeviceUsage>();

            foreach (var row in stream)
            {
                if (!perUser.ContainsKey(row.UserID))
                    perUser[row.UserID] = new UserDeviceUsage();

                var device = perUser[row.UserID];

                if (row.ComputerType.Equals("DESKTOP", StringComparison.OrdinalIgnoreCase))
                    device.Desktops++;
                else if (row.ComputerType.Equals("LAPTOP", StringComparison.OrdinalIgnoreCase))
                    device.Laptops++;
            }
            return perUser;
        }
        internal int SumLicenses(Dictionary<int, UserDeviceUsage> perUser)
        {
            var numberOfLicenses = 0;
            foreach (var usage in perUser.Values)
                numberOfLicenses += LicenseCalculationUtility.CalculateForUser(usage.Desktops, usage.Laptops);
            return numberOfLicenses;
        }
    }
}