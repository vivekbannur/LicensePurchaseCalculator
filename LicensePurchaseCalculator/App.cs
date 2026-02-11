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
            var path = args[0];
            var appId = (args.Length >= 2 && int.TryParse(args[1], out var parsed)) ? parsed : 374;

            _logger.LogInformation("Reading: {Path} | ApplicationID: {AppId}", path, appId);
            // Stream CSV records one-by-one (no full file load into memory)
            var records = _reader.ReadInstallations(path);
            var result = _calculator.CalculateMinimumLicenses(records, appId);
            _logger.LogInformation("License calculation completed successfully.");
            _logger.LogInformation("Application: {AppId}", appId);
            _logger.LogInformation("Minimum licenses required: {}", result);
            return 0;
        }
    }
}
