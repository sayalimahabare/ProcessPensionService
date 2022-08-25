using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ProcessPensionDetails.Models
{
    public class processDetails
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public DateTime DOB { get; set; }
        public string PAN { get; set; }
        public double SalaryEarned { get; set; }
        public int Allowances { get; set; }
        public string SelfOrFamilyPension { get; set; }
        public string BankName { get; set; }
        public double AccountNumber { get; set; }
        public string PublicOrPrivate { get; set; }
        public string AdharNumber { get; set; }
        public double pensionAmount { get; set; }
        public double serviceCharge { get; set; }
    }
}
