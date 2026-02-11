using LicensePurchaseCalculator.Interfaces.Providers;
using LicensePurchaseCalculator.Models;
using LicensePurchaseCalculator.Utilities;
using LicensePurchaseCalculator.Constants;

namespace LicensePurchaseCalculator.Implementations.Providers
{
    public class LicenseCalculationProvider : ILicenseCalculationProvider
    {
        /// Calculates the minimum number of licenses required per application.
        /// Processes installation records in a streaming manner to support very large files.
        /// Performs filtering, deduplication, and per-user aggregation.
        public int CalculateMinimumLicenses(IEnumerable<AppInstallationModel> stream, int applicationId)
        {
            int counter = 0;
            var filteredData = FilterByApp(stream, applicationId, counter);
            var uniqueData = RemoveDuplicates(filteredData);
            var perUser = AggregatePerUser(uniqueData);
            var numberOfLicenses = SumLicenses(perUser);
            return numberOfLicenses;
        }
        internal IEnumerable<AppInstallationModel> FilterByApp(IEnumerable<AppInstallationModel> stream, int appId,int counter)
        {
            foreach (var row in stream)
            {
                counter++;
                if (counter % 1000000 == 0)
                    Console.WriteLine($"Processed {counter} rows...");
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

                if (row.ComputerType.Equals(ApplicationConstants.Desktop, StringComparison.OrdinalIgnoreCase))
                    device.Desktops++;
                else if (row.ComputerType.Equals(ApplicationConstants.Laptop, StringComparison.OrdinalIgnoreCase))
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