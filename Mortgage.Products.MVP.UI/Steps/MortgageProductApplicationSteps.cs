using FluentAssertions;
using Microsoft.Extensions.Configuration;
using Mortgage.Products.MVP.API.Helpers;
using Mortgage.Products.MVP.API.Models;
using Mortgage.Products.MVP.UI.BusinessObjects;
using Mortgage.Products.MVP.UI.Helpers;
using Mortgage.Products.MVP.UI.Models;
using Mortgage.Products.MVP.UI.SeleniumHelpers;
using System.Collections.Generic;
using TechTalk.SpecFlow;
using Xunit.Abstractions;

namespace Mortgage.Products.MVP.UI.Steps
{
    [Binding]
    public sealed class MortgageProductApplicationSteps
    {
        private readonly ScenarioContext _scenarioContext;
        private readonly MortgageProductsObjects _mortgageProductsPage;
        private readonly ITestOutputHelper _output;

        public MortgageProductApplicationSteps(ScenarioContext scenarioContext, ITestOutputHelper output)
        {
            _scenarioContext = scenarioContext;
            _mortgageProductsPage = new MortgageProductsObjects(_scenarioContext);

            //TODO add UI output logging to Step / Helpers
            _output = output;
        }

        [Given(@"the mortgage product information is retrieved via the API")]
        public void GivenTheMortgageProductInformationIsRetrievedViaTheAPI()
        {
            //TODO ScenarioContext Helper Class to manage reads / adds
            _scenarioContext["apiResponse"] = RequestHelper.MakeApiGetRequest(_output, "products");
        }

        [Given(@"the application homepage is loaded")]
        [When(@"the application homepage is loaded")]
        public void GivenTheApplicationHomepageIsLoaded()
        {
            //TODO refactor to Config helper class
            var config = new ConfigurationBuilder().AddJsonFile("appconfig.json").Build();

            DriverUtility.SetupBrowser("chrome",10);
            DriverUtility.NavigateToUrl(config["ApplicationUrl"]);      
        }

        [Then(@"the Mortgage Products dispalyed matches the API response")]
        public void ThenTheMortgageProductsDispalyedMatchesTheAPIResponse()
        {
            //TODO ScenarioContext Helper Class to manage reads / adds
            var apiResponse = _scenarioContext["apiResponse"] as ApiResponse<object>;

            IList<MortgageProduct> expectedMortgageProducts = DataHelper.ProcessApiDataToMortgageProducts(apiResponse);
            IList<MortgageProduct> actualMortgageProducts = _mortgageProductsPage.GetDisplayedMortageProducts();

            actualMortgageProducts.Should().BeEquivalentTo(expectedMortgageProducts);
        }


    }
}
