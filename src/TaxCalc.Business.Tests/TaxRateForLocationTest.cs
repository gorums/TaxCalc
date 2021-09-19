using Microsoft.VisualStudio.TestTools.UnitTesting;
using Moq;
using System;
using System.Threading;
using System.Threading.Tasks;
using TaxCalc.Domain;
using TaxCalc.Domain.Models;

namespace TaxCalc.Business.Tests
{
    [TestClass]
    public class TaxRateForLocationTest
    {
        [TestMethod]
        [ExpectedException(typeof(ArgumentException), "The parameter zip is required")]
        public async Task TestTaxRateForLocationWitoutZip()
        {
            // Arrange
            var mockTaxCalcProviderFactory = new Mock<ITaxCalcProviderFactory>();

            // Act 
            var taxCalcBusiness = new TaxCalcBusiness(mockTaxCalcProviderFactory.Object);
            var rate = await taxCalcBusiness.GetTaxRateForLocationAsync(string.Empty, null);
        }

        [TestMethod]
        public async Task TestTaxRateForLocation()
        {
            // Arrange
            var mockTaxCalcProviderFactory = new Mock<ITaxCalcProviderFactory>();
            var mockTaxCalcProvider = new Mock<ITaxCalcProvider>();

            mockTaxCalcProviderFactory
                .Setup(t => t.GetTaxCalcProviderImpl())
                .Returns(() => mockTaxCalcProvider.Object);

            mockTaxCalcProvider
                .Setup(p => p.GetTaxRateForLocationAsync(It.IsAny<string>(), It.IsAny<OptionalAddress>(), It.IsAny<CancellationToken>()))
                .Returns<string, OptionalAddress, CancellationToken>((zip, optionalAddress, c) =>
                {
                    if ("90404".Equals(zip))
                    {
                        return Task.FromResult(new RateResult()
                        {
                            Rate = new Rate
                            {
                                StateRate = "0.06"
                            }
                        });
                    }

                    return null;
                });

            // Act 
            var taxCalcBusiness = new TaxCalcBusiness(mockTaxCalcProviderFactory.Object);
            var rate = await taxCalcBusiness.GetTaxRateForLocationAsync("90404", null);

            // Assert 
            Assert.IsTrue(rate != null);
            Assert.IsTrue("0.06".Equals(rate.StateRate));
        }

        [TestMethod]
        public async Task TestTaxRateForLocationWrongZip()
        {
            // Arrange
            var mockTaxCalcProviderFactory = new Mock<ITaxCalcProviderFactory>();
            var mockTaxCalcProvider = new Mock<ITaxCalcProvider>();

            mockTaxCalcProviderFactory
                .Setup(t => t.GetTaxCalcProviderImpl())
                .Returns(() => mockTaxCalcProvider.Object);

            mockTaxCalcProvider
                .Setup(p => p.GetTaxRateForLocationAsync(It.IsAny<string>(), It.IsAny<OptionalAddress>(), It.IsAny<CancellationToken>()))
                .Returns<string, OptionalAddress, CancellationToken>((zip, optionalAddress, c) =>
                {
                    if ("11111".Equals(zip))
                    {
                        // Empty Rate
                        return Task.FromResult(new RateResult());
                    }

                    return Task.FromResult(new RateResult()
                    {
                        Rate = new Rate
                        {
                            StateRate = "0.06"
                        }
                    });
                });

            // Act 
            var taxCalcBusiness = new TaxCalcBusiness(mockTaxCalcProviderFactory.Object);
            var rate = await taxCalcBusiness.GetTaxRateForLocationAsync("11111", null);

            // Assert 
            Assert.IsTrue(rate == null);
        }
    }
}
