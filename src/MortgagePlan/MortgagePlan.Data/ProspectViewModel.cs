using CsvHelper.Configuration;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MortgagePlan.Data
{
    public class ProspectViewModel
    {
        [Required(ErrorMessage = "Customer Name is required")]
        [Display(Name ="Customer Name")]
        public string CustomerName { get; set; }

        [Required(ErrorMessage = "Loan Amount is required")]
        [Range(0.001, double.MaxValue, ErrorMessage = "Loan Amount must be a positive number")]
        [Display(Name = "Loan Amount")]
        public decimal LoanAmount { get; set; }

        [Required(ErrorMessage = "Years is required")]
        [Range(1, int.MaxValue, ErrorMessage = "Years must be a positive number")]
        [Display(Name = "Loan Duration")]
        public int Years { get; set; } = 0;

        [Required(ErrorMessage = "Interest Rate is required")]
        [Range(0.001, double.MaxValue, ErrorMessage = "Interest Rate must be a positive number")]
        [Display(Name = "Interest Rate (Annual)")]
        public decimal InterestRate { get; set; }

    }
}
