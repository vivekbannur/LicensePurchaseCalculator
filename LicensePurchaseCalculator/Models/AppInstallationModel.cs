using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LicensePurchaseCalculator.Models
{
    public class AppInstallationModel
    {
        public int ComputerID{ get; set; }
        public int UserID { get; set; }
        public int ApplicationID { get; set; }
        public string ComputerType { get; set; } = string.Empty;
        public string? Comment { get; set; }
    }
}
