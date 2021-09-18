using Microsoft.VisualStudio.TestTools.UnitTesting;
using Moq;
using System.Threading;
using System.Threading.Tasks;
using TaxCalc.Domain;
using TaxCalc.Domain.Models;

namespace TaxCalc.Business.Tests
{
    [TestClass]
    public class CalculateTaxForAnOrderTest
    {
        [TestMethod]
        public void TestCalculateTaxForAnOrderA()
        {
            // Arrange
            var mockTaxCalcProviderFactory = new Mock<ITaxCalcProviderFactory>();
            var mockTaxCalcProvider = new Mock<ITaxCalcProvider>();

            mockTaxCalcProviderFactory
                .Setup(t => t.GetTaxCalcProvider(It.IsAny<string>()))
                .Returns<string>((providerName) => mockTaxCalcProvider.Object);

            
        }

        [TestMethod]
        public void TestCalculateTaxForAnOrderAWrong()
        {
            // Arrange
            var mockTaxCalcProviderFactory = new Mock<ITaxCalcProviderFactory>();
            var mockTaxCalcProvider = new Mock<ITaxCalcProvider>();

            mockTaxCalcProviderFactory
                .Setup(t => t.GetTaxCalcProvider(It.IsAny<string>()))
                .Returns<string>((providerName) => mockTaxCalcProvider.Object);

           
        }
    }
}
