using LicensePurchaseCalculator.Models;

namespace LicensePurchaseCalculator.Interfaces.Providers
{
    public interface IDataReaderProvider
    {
        IEnumerable<AppInstallationModel> ReadInstallations(string filePath);
    }
}
