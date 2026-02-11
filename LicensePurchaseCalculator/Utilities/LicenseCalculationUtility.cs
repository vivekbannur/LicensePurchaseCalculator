namespace LicensePurchaseCalculator.Utilities
{
    public static class LicenseCalculationUtility
    {
        public static int CalculateForUser(int desktops, int laptops)
        {
            var extraLaptops = Math.Max(0, laptops - desktops);
            var extraCopies = (extraLaptops + 1) / 2;
            return desktops + extraCopies;
        }
    }
}
