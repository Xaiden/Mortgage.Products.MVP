using FluentAssertions;
using Microsoft.Extensions.Configuration;
using Mortgage_Products_MVP.Helpers;
using Mortgage_Products_MVP.Models;
using Mortgage_Products_UI.BusinessObjects;
using Mortgage_Products_UI.Helpers;
using Mortgage_Products_UI.Models;
using Mortgage_Products_UI.SeleniumHelpers;
using System.Collections.Generic;
using TechTalk.SpecFlow;
using Xunit.Abstractions;

namespace Mortgage_Products_UI.Steps
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
            _output = output;
        }

        [Given(@"the mortgage product information is retrieved via the API")]
        public void GivenTheMortgageProductInformationIsRetrievedViaTheAPI()
        {
            _scenarioContext["apiResponse"] = RequestHelper.MakeApiGetRequest(_output, "products");
        }

        [Given(@"the application homepage is loaded")]
        [When(@"the application homepage is loaded")]
        public void GivenTheApplicationHomepageIsLoaded()
        {
            var config = new ConfigurationBuilder().AddJsonFile("appconfig.json").Build();
            //ApplicationUrl

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
