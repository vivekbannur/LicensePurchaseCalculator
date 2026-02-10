using LicensePurchaseCalculator.Implementations.Providers;
using LicensePurchaseCalculator.Models;

namespace LicensePurchaseCalculator.Tests
{
    public class LicenseCalculationProviderTests
    {
        private readonly LicenseCalculationProvider _sut = new LicenseCalculationProvider();

        [Fact]
        public void Example1_UserHasLaptopAndDesktop_Requires1Copy()
        {
            var data = new[]
            {
                new AppInstallationModel { ComputerID = 1, UserID = 1, ApplicationID = 374, ComputerType = "LAPTOP",  Comment="A" },
                new AppInstallationModel { ComputerID = 2, UserID = 1, ApplicationID = 374, ComputerType = "DESKTOP", Comment="A" },
            };
            var result = _sut.CalculateMinimumLicenses(data, 374);
            Assert.Equal(1, result);
        }

        [Fact]
        public void Example2_TwoUsers_Requires3Copies()
        {
            var data = new[]
            {
                new AppInstallationModel { ComputerID = 1, UserID = 1, ApplicationID = 374, ComputerType = "LAPTOP",  Comment="A" },
                new AppInstallationModel { ComputerID = 2, UserID = 1, ApplicationID = 374, ComputerType = "DESKTOP", Comment="A" },

                new AppInstallationModel { ComputerID = 3, UserID = 2, ApplicationID = 374, ComputerType = "DESKTOP", Comment="A" },
                new AppInstallationModel { ComputerID = 4, UserID = 2, ApplicationID = 374, ComputerType = "DESKTOP", Comment="A" },
            };
            var result = _sut.CalculateMinimumLicenses(data, 374);
            Assert.Equal(3, result);
        }

        [Fact]
        public void Example3_DuplicateRows_Requires2Copies()
        {
            var data = new[]
            {
                new AppInstallationModel { ComputerID = 1, UserID = 1, ApplicationID = 374, ComputerType = "LAPTOP",  Comment="SystemA" },

                new AppInstallationModel { ComputerID = 2, UserID = 2, ApplicationID = 374, ComputerType = "DESKTOP", Comment="SystemA" },
                new AppInstallationModel { ComputerID = 2, UserID = 2, ApplicationID = 374, ComputerType = "desktop", Comment="SystemB" }, // duplicate per example
            };
            var result = _sut.CalculateMinimumLicenses(data, 374);
            Assert.Equal(2, result);
        }

        [Fact]
        public void MixedApplicationIds_IgnoresOtherApps_Requires1Copy()
        {
            var data = new[]
            {
                new AppInstallationModel { ComputerID = 1, UserID = 1, ApplicationID = 999, ComputerType = "LAPTOP", Comment="SystemA" },

                new AppInstallationModel { ComputerID = 2, UserID = 1, ApplicationID = 374, ComputerType = "LAPTOP", Comment="SystemA" },
                new AppInstallationModel { ComputerID = 3, UserID = 1, ApplicationID = 374, ComputerType = "DESKTOP", Comment="SystemA" },
            };
            var result = _sut.CalculateMinimumLicenses(data, 374);
            Assert.Equal(1, result); // only rows for 374 count
        }

        [Fact]
        public void OnlyDesktops_SameUser_TwoDesktops_Requires2Copies()
        {
            var data = new[]
            {
                new AppInstallationModel { ComputerID = 1, UserID = 1, ApplicationID = 374, ComputerType = "DESKTOP", Comment="SystemA" },
                new AppInstallationModel { ComputerID = 2, UserID = 1, ApplicationID = 374, ComputerType = "DESKTOP", Comment="SystemA" },
            };
            var result = _sut.CalculateMinimumLicenses(data, 374);
            Assert.Equal(2, result);
        }

        [Fact]
        public void OnlyLaptops_ThreeLaptops_Requires2Copies()
        {
            var data = new[]
            {
                new AppInstallationModel { ComputerID = 1, UserID = 1, ApplicationID = 374, ComputerType = "LAPTOP", Comment="SystemA" },
                new AppInstallationModel { ComputerID = 2, UserID = 1, ApplicationID = 374, ComputerType = "LAPTOP", Comment="SystemA" },
                new AppInstallationModel { ComputerID = 3, UserID = 1, ApplicationID = 374, ComputerType = "LAPTOP", Comment="SystemA" },
            };
            var result = _sut.CalculateMinimumLicenses(data, 374);
            Assert.Equal(2, result);
        }
    }
}