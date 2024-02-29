using MortgagePlan.Business.Interfaces;
using MortgagePlan.Data;
using System;
using System.Web.Mvc;
using System.Web.UI;

namespace MortgagePlan.Controllers
{
    public class HomeController : Controller
    {
        private readonly IMortgageCalculation _mortgageCalculation;
        private readonly IProspectFileReader _prospectFileReader;

        public HomeController(IMortgageCalculation mortgageCalculation, IProspectFileReader prospectFileReader)
        {
            _mortgageCalculation = mortgageCalculation;
            _prospectFileReader = prospectFileReader;
        }
        public ActionResult Index()
        {
            var prospects = _mortgageCalculation.GetProspects();
            return View(prospects);
        }
        [HttpPost]
        public ActionResult Submit(ProspectViewModel prospect)
        {
            if (ModelState.IsValid)
            {
                var prospectData = new Prospect()
                {
                    CustomerName = prospect.CustomerName,
                    LoanAmount = prospect.LoanAmount,
                    InterestRate = prospect.InterestRate,
                    Years = prospect.Years
                };
                _prospectFileReader.WriteFile(prospectData);

                ViewBag.Message = "Form submitted successfully!";
                return RedirectToAction("Index");
            }
            var prospects = _mortgageCalculation.GetProspects();
            return View("Index",prospects);
        }

    }
}