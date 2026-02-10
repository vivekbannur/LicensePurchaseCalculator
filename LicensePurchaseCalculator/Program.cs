using LicensePurchaseCalculator;
using LicensePurchaseCalculator.Implementations.Providers;
using LicensePurchaseCalculator.Interfaces.Providers;
using Microsoft.Extensions.DependencyInjection;

public class Program
{
    public static void Main(string[] args)
    {
        var host = CreateHostBuilder(args).Build();
            var app = host.Services.GetRequiredService<App>();
            app.Run(args);
    }

    public static IHostBuilder CreateHostBuilder(string[] args) =>
        Host.CreateDefaultBuilder(args)
            .ConfigureServices((ctx, services) =>
            {
                services.AddLogging(x => x.AddConsole());
                services.AddSingleton<IDataReaderProvider, DataReaderProvider>();
                services.AddSingleton<ILicenseCalculationProvider, LicenseCalculationProvider>();
                services.AddSingleton<App>();
            });
}