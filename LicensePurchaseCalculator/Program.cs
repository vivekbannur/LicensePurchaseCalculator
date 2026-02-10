using LicensePurchaseCalculator;
using LicensePurchaseCalculator.Implementations.Providers;
using LicensePurchaseCalculator.Interfaces.Providers;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;

public class Program
{
    public static void Main(string[] args)
    {
        try
        {
            var host = CreateHostBuilder(args).Build();
            var app = host.Services.GetRequiredService<App>();
            app.Run(args);
        }
        catch (FileNotFoundException ex)
        {
            Console.WriteLine($"File not found: {ex.Message}");
        }
        catch (Exception ex)
        {
            Console.WriteLine($"Unexpected error: {ex.Message}");
        }
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