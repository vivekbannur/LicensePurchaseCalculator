using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using LicensePurchaseCalculator.Models;

namespace LicensePurchaseCalculator.Interfaces.Providers
{
    public interface ILicenseCalculationProvider
    {
        int CalculateMinimumLicenses(IEnumerable<AppInstallationModel> stream, int applicationId);
    }
}
