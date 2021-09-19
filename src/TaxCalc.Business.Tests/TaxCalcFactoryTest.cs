using Microsoft.Extensions.Options;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Moq;
using System;
using System.Threading.Tasks;
using TaxCalc.Business.Configurations;

namespace TaxCalc.Business.Tests
{
    [TestClass]
    public class TaxCalcFactoryTest
    {
        [TestMethod]
        [ExpectedException(typeof(ArgumentException), "The provider Wrong Provider does not exist")]
        public async Task TestFactoryWrongProvider()
        {
            // Arrange
            var mockOptions = new Mock<IOptions<TaxProviderOptions>>();

            mockOptions
                .Setup(t => t.Value)
                .Returns(() => new TaxProviderOptions
                {
                    Name = "Wrong Provider", // "The provider Wrong Provider does not exist"
                    Url = "https://api.taxjar.com/v2",
                    Token = "1111",
                    Retry = 3,
                    Wait = 5000
                });


            // Act 
            var taxCalcProviderFactory = new TaxCalcProviderFactory(mockOptions.Object);
            var taxCalcBusiness = new TaxCalcBusiness(taxCalcProviderFactory);
            var rate = await taxCalcBusiness.GetTaxRateForLocationAsync("90404", null);
        }

        [TestMethod]
        [ExpectedException(typeof(ArgumentException), "The Retry value only can be between 1 and 10")]
        public async Task TestFactoryInvalidRetry()
        {
            // Arrange
            var mockOptions = new Mock<IOptions<TaxProviderOptions>>();

            mockOptions
                .Setup(t => t.Value)
                .Returns(() => new TaxProviderOptions
                {
                    Name = "TaxJar",
                    Url = "https://api.taxjar.com/v2",
                    Token = "1111",
                    Retry = 20, // "The Retry value only can be between 1 and 10"
                    Wait = 5000
                });


            // Act 
            var taxCalcProviderFactory = new TaxCalcProviderFactory(mockOptions.Object);
            var taxCalcBusiness = new TaxCalcBusiness(taxCalcProviderFactory);
            var rate = await taxCalcBusiness.GetTaxRateForLocationAsync("90404", null);
        }

        [TestMethod]
        [ExpectedException(typeof(ArgumentException), "The URl is invalid")]
        public async Task TestFactoryInvalidURL()
        {
            // Arrange
            var mockOptions = new Mock<IOptions<TaxProviderOptions>>();

            mockOptions
                .Setup(t => t.Value)
                .Returns(() => new TaxProviderOptions
                {
                    Name = "TaxJar",
                    Url = "invalid url", // "The URl is invalid"
                    Token = "1111",
                    Retry = 3,
                    Wait = 5000
                });


            // Act 
            var taxCalcProviderFactory = new TaxCalcProviderFactory(mockOptions.Object);
            var taxCalcBusiness = new TaxCalcBusiness(taxCalcProviderFactory);
            var rate = await taxCalcBusiness.GetTaxRateForLocationAsync("90404", null);
        }

        [TestMethod]
        [ExpectedException(typeof(ArgumentException), "The Token can not be empty")]
        public async Task TestFactoryEmptyToken()
        {
            // Arrange
            var mockOptions = new Mock<IOptions<TaxProviderOptions>>();

            mockOptions
                .Setup(t => t.Value)
                .Returns(() => new TaxProviderOptions
                {
                    Name = "TaxJar",
                    Token = string.Empty, // "The Token can not be empty"
                    Url = "https://api.taxjar.com/v2",
                    Retry = 3,
                    Wait = 5000
                });


            // Act 
            var taxCalcProviderFactory = new TaxCalcProviderFactory(mockOptions.Object);
            var taxCalcBusiness = new TaxCalcBusiness(taxCalcProviderFactory);
            var rate = await taxCalcBusiness.GetTaxRateForLocationAsync("90404", null);
        }
    }
}
