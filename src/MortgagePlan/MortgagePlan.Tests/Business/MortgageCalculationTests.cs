using Moq;
using MortgagePlan.Business;
using MortgagePlan.Business.Interfaces;
using MortgagePlan.Data;
using NUnit.Framework;
using System.Collections.Generic;

namespace MortgagePlan.Tests.Business
{
    [TestFixture]
    public class MortgageCalculationTests
    {
        private MortgageCalculation _mortgageCalculation;
        private Mock<IProspectFileReader> _prospectFileReaderMock;

        [SetUp]
        public void Setup()
        {
            _prospectFileReaderMock = new Mock<IProspectFileReader>();
            _mortgageCalculation = new MortgageCalculation(_prospectFileReaderMock.Object);
        }

        [Test]
        public void GetProspects_ShouldCallReadFile()
        {
            // Arrange
            _prospectFileReaderMock.Setup(x => x.ReadFile()).Returns(new List<Prospect>());

            // Act
            _mortgageCalculation.GetProspects();

            // Assert
            _prospectFileReaderMock.Verify(x => x.ReadFile(), Times.Once);
        }

        [TestCase(5, 2, 1000, 43.87)]
        [TestCase(10, 1, 1000, 87.92)]
        public void GetProspects_ValidInputs_ReturnsExpectedResult(decimal c, int n, decimal L, decimal expectedP)
        {
            var testprospect = new List<Prospect>() { 
                new Prospect(){
                    LoanAmount = L,
                    InterestRate = c,
                    Years = n
                }
            };
            // Arrange
            _prospectFileReaderMock.Setup(x => x.ReadFile()).Returns(testprospect);

            // Act
            var response = _mortgageCalculation.GetProspects().Data.FirstOrDefault();

            // Assert
            Assert.That(response.MonthlyPayment, Is.EqualTo(expectedP).Within(0.01m));
        }
        
        [Test]
        public void GetProspects_ShouldRaiseFileNotFoundErrorIfFileNotFound()
        {
            // Arrange
            _prospectFileReaderMock.Setup(x => x.ReadFile()).Throws<FileNotFoundException>();

            // Act
            var response = _mortgageCalculation.GetProspects();

            // Assert
            Assert.IsFalse(response.IsSuccess);
            Assert.AreEqual("Prospect file not found", response.Message);
        }
        
        [Test]
        public void GetProspects_ShouldRaiseSomethingWentWrongErrorIfOtherErrors()
        {
            // Arrange
            _prospectFileReaderMock.Setup(x => x.ReadFile()).Throws<Exception>();

            // Act
            var response = _mortgageCalculation.GetProspects();

            // Assert
            Assert.IsFalse(response.IsSuccess);
            Assert.AreEqual("Something went wrong", response.Message);
        }

    }
}
