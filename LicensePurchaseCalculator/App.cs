using System;
using LicensePurchaseCalculator.Interfaces.Providers;
using Microsoft.Extensions.Logging;

namespace LicensePurchaseCalculator
{
    public class App
    {
        private readonly IDataReaderProvider _reader;
        private readonly ILicenseCalculationProvider _calculator;
        private readonly ILogger<App> _logger;

        public App(IDataReaderProvider reader, ILicenseCalculationProvider calculator, ILogger<App> logger)
        {
            _reader = reader;
            _calculator = calculator;
            _logger = logger;
        }

        public int Run(string[] args)
        {
            if (args.Length < 1)
            {
                _logger.LogError("Missing required argument: <path-to-file.csv>");
                _logger.LogInformation("Usage: dotnet run -- <path-to-file.csv> [applicationId]");
                return 2;
            }
            _logger.LogInformation("License calculation started.");
            
            _logger.LogInformation("License calculation completed successfully.");
            return 0;
        }
    }
}
