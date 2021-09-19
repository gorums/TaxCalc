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
    public class CalculateTaxForAnOrderTest
    {
        [TestMethod]
        [ExpectedException(typeof(ArgumentException), "The parameter to_country is required")]
        public async Task TestCalculateTaxForAnOrderValidateCountry()
        {
            // Arrange
            var mockTaxCalcProviderFactory = new Mock<ITaxCalcProviderFactory>();
            var mockTaxCalcProvider = new Mock<ITaxCalcProvider>();

            mockTaxCalcProviderFactory
                .Setup(t => t.GetTaxCalcProviderImpl())
                .Returns(() => mockTaxCalcProvider.Object);

            // Act 
            var taxCalcBusiness = new TaxCalcBusiness(mockTaxCalcProviderFactory.Object);
            var tax = await taxCalcBusiness.CalculateTaxForAnOrderAsync(new Order());
        }

        [TestMethod]
        [ExpectedException(typeof(ArgumentException), "The parameter shipping has to be greater than 0")]
        public async Task TestCalculateTaxForAnOrderValidateShipping()
        {
            // Arrange
            var mockTaxCalcProviderFactory = new Mock<ITaxCalcProviderFactory>();
            var mockTaxCalcProvider = new Mock<ITaxCalcProvider>();

            mockTaxCalcProviderFactory
                .Setup(t => t.GetTaxCalcProviderImpl())
                .Returns(() => mockTaxCalcProvider.Object);

            // Act 
            var taxCalcBusiness = new TaxCalcBusiness(mockTaxCalcProviderFactory.Object);
            var tax = await taxCalcBusiness.CalculateTaxForAnOrderAsync(new Order
            {
                ToCountry ="CA"
            });
        }


        [TestMethod]
        [ExpectedException(typeof(ArgumentException), "The parameter to_zip is required")]
        public async Task TestCalculateTaxForAnOrderValidateToZip()
        {
            // Arrange
            var mockTaxCalcProviderFactory = new Mock<ITaxCalcProviderFactory>();
            var mockTaxCalcProvider = new Mock<ITaxCalcProvider>();

            mockTaxCalcProviderFactory
                .Setup(t => t.GetTaxCalcProviderImpl())
                .Returns(() => mockTaxCalcProvider.Object);

            // Act 
            var taxCalcBusiness = new TaxCalcBusiness(mockTaxCalcProviderFactory.Object);
            var tax = await taxCalcBusiness.CalculateTaxForAnOrderAsync(new Order
            {
                ToCountry = "US",
                Shipping = 1.2
            });
        }

        [TestMethod]
        [ExpectedException(typeof(ArgumentException), "The parameter to_state is required")]
        public async Task TestCalculateTaxForAnOrderValidateToState()
        {
            // Arrange
            var mockTaxCalcProviderFactory = new Mock<ITaxCalcProviderFactory>();
            var mockTaxCalcProvider = new Mock<ITaxCalcProvider>();

            mockTaxCalcProviderFactory
                .Setup(t => t.GetTaxCalcProviderImpl())
                .Returns(() => mockTaxCalcProvider.Object);

            // Act 
            var taxCalcBusiness = new TaxCalcBusiness(mockTaxCalcProviderFactory.Object);
            var tax = await taxCalcBusiness.CalculateTaxForAnOrderAsync(new Order
            {
                ToCountry = "US",
                ToZip = "11111",
                Shipping = 1.2
            });
        }

        [TestMethod]
        public async Task TestCalculateTaxForAnOrder()
        {
            // Arrange
            var mockTaxCalcProviderFactory = new Mock<ITaxCalcProviderFactory>();
            var mockTaxCalcProvider = new Mock<ITaxCalcProvider>();

            mockTaxCalcProviderFactory
                .Setup(t => t.GetTaxCalcProviderImpl())
                .Returns(() => mockTaxCalcProvider.Object);

            mockTaxCalcProvider
                .Setup(p => p.CalculateTaxForAnOrderAsync(It.IsAny<Order>(),  It.IsAny<CancellationToken>()))
                .Returns<Order, CancellationToken>((order, c) =>
                {
                    if ("US".Equals(order.ToCountry))
                    {
                        return Task.FromResult(new TaxResult()
                        {
                            Tax = new Tax
                            {
                                Rate = 0.06
                            }
                        });
                    }

                    return null;
                });

            // Act 
            var taxCalcBusiness = new TaxCalcBusiness(mockTaxCalcProviderFactory.Object);
            var tax = await taxCalcBusiness.CalculateTaxForAnOrderAsync(new Order
            {
                ToCountry = "US",
                ToZip = "11111",
                ToState = "FL",
                Shipping = 1.2
            });

            // Assert 
            Assert.IsTrue(tax != null);
            Assert.IsTrue(0.06 == tax.Rate);
        }
    }
}
