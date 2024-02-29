using CsvHelper.Configuration;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MortgagePlan.Data
{
    public class Prospect : ClassMap<Prospect>
    {
        public string CustomerName { get; set; }
        public decimal LoanAmount { get; set; }
        public int Years { get; set; }
        public decimal InterestRate { get; set; }
        public decimal MonthlyPayment { get; set; }
        public Prospect()
        {
            Map(m => m.CustomerName).Index(0);
            Map(m => m.LoanAmount).Index(1);
            Map(m => m.InterestRate).Index(2);
            Map(m => m.Years).Index(3);
        }
        public override string ToString()
        {
            return $"{CustomerName} wants to borrow {LoanAmount} € for a period of {Years} years and pay {MonthlyPayment} € each month";
        }
        
        
    }
}
