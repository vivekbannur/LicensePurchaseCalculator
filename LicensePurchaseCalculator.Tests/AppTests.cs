using LicensePurchaseCalculator.Interfaces.Providers;
using LicensePurchaseCalculator.Models;
using Microsoft.Extensions.Logging;
using Moq;

namespace LicensePurchaseCalculator.Tests
{
    public class AppTests
    {
        [Fact]
        public void Run_NoArguments_Returns2()
        {
            var readerMock = new Mock<IDataReaderProvider>();
            var calcMock = new Mock<ILicenseCalculationProvider>();
            var loggerMock = new Mock<ILogger<App>>();

            var app = new App(readerMock.Object, calcMock.Object, loggerMock.Object);
            var result = app.Run(Array.Empty<string>());
            Assert.Equal(2, result);
        }

        [Fact]
        public void Run_ValidArguments_Returns0()
        {
            var readerMock = new Mock<IDataReaderProvider>();
            var calcMock = new Mock<ILicenseCalculationProvider>();
            var loggerMock = new Mock<ILogger<App>>();

            var fakeData = new List<AppInstallationModel>();

            readerMock
                .Setup(r => r.ReadInstallations("test.csv"))
                .Returns(fakeData);

            calcMock
                .Setup(c => c.CalculateMinimumLicenses(fakeData, 374))
                .Returns(5);

            var app = new App(readerMock.Object, calcMock.Object, loggerMock.Object);

            var result = app.Run(new[] { "test.csv", "374" });

            Assert.Equal(0, result);
        }

        [Fact]
        public void Run_NoAppIdProvided_DefaultsTo374()
        {
            var readerMock = new Mock<IDataReaderProvider>();
            var calcMock = new Mock<ILicenseCalculationProvider>();
            var loggerMock = new Mock<ILogger<App>>();

            var fakeData = new List<AppInstallationModel>();

            readerMock
                .Setup(r => r.ReadInstallations("test.csv"))
                .Returns(fakeData);

            calcMock
                .Setup(c => c.CalculateMinimumLicenses(fakeData, 374))
                .Returns(1);

            var app = new App(readerMock.Object, calcMock.Object, loggerMock.Object);

            var result = app.Run(new[] { "test.csv" });

            Assert.Equal(0, result);
        }
    }
}
