using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using LicensePurchaseCalculator.Models;

namespace LicensePurchaseCalculator.Interfaces.Providers
{
    public interface IDataReaderProvider
    {
        IEnumerable<AppInstallationModel> ReadInstallations(string filePath);
    }
}
