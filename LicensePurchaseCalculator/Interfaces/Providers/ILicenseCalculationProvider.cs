using LicensePurchaseCalculator.Models;

namespace LicensePurchaseCalculator.Interfaces.Providers
{
    public interface ILicenseCalculationProvider
    {
        int CalculateMinimumLicenses(IEnumerable<AppInstallationModel> stream, int applicationId);
    }
}
