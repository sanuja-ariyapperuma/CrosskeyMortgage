using MortgagePlan.Business.Interfaces;
using MortgagePlan.Data;
using System;
using System.Collections.Generic;
using System.IO;

namespace MortgagePlan.Business
{
    
    
    public class MortgageCalculation : IMortgageCalculation
    {
        private readonly IProspectFileReader _prospectFileReader;
        public MortgageCalculation(IProspectFileReader prospectFileReader)
        {
            _prospectFileReader = prospectFileReader;
        }
        public CustomResponse<IEnumerable<Prospect>> GetProspects()
        {
            try
            {
                var sourceProspects = _prospectFileReader.ReadFile();

                foreach (var prospect in sourceProspects)
                {
                    CalculateMonthlyPayment(prospect);
                }

                return new CustomResponse<IEnumerable<Prospect>>()
                {
                    IsSuccess = true,
                    Data = sourceProspects
                };
            }
            catch (FileNotFoundException) 
            {
                return new CustomResponse<IEnumerable<Prospect>>()
                {
                    IsSuccess = false,
                    Message = "Prospect file not found"
                };
            }
            catch (Exception ex)
            {

                return new CustomResponse<IEnumerable<Prospect>>()
                {
                    IsSuccess = false,
                    Message = "Something went wrong"
                };
            }
        }
       
        private void CalculateMonthlyPayment(Prospect prospect)
        {
            var monthlyInterest = prospect.InterestRate / 100 / 12;
            var commonPotion = Power((1 + monthlyInterest), prospect.Years * 12); // Represetns (1 + c)^n

            var numerator = prospect.LoanAmount * monthlyInterest * commonPotion;
            var denominator = commonPotion - 1;

            var monthlyPayment = numerator / denominator;

            prospect.MonthlyPayment = Math.Round(monthlyPayment, 2);

        }

        private decimal Power(decimal baseValue, int exponent)
        {
            decimal result = 1;

            for (int i = 0; i < exponent; i++)
                result *= baseValue;

            return result;
        }

        
    }
}
